using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InstanceUnit))]
[RequireComponent(typeof(NavMeshAgent))]
public class NPCBehaviour : MonoBehaviour
{
    private InstanceUnit _unit;
    private Transform _character;

    // Movement
    private Vector3 _movePos, _spawnPoint, _abiPoint, _myPos, _tarPos;
    private NavMeshAgent _navMA;
    private const float _turnSpeed = 50f, _variance = 5f, _reactionSpeed = 1/3.2f /*2.5f*/, _failRate = 1f,
                        _spawnRange = 20f, _aggroRange = 10f, _combatRange = 15f, _returningRange = 7f;
    private float _reactionTime = 0f;

    // Raycast
    private const float _maxUseDistance = 6f;
    private Ray _ray;
    private RaycastHit _hit;
    private readonly LayerMask _groundLayer = (1 << 6);

    
    public NPCState State { get; set; } = NPCState.Idle;
    public Transform Target { get; set; }
    
    private void Awake()
    {
        _character = transform.Find("Character");
            
        _unit = GetComponent<InstanceUnit>();
        _navMA = GetComponent<NavMeshAgent>();
        _navMA.updateRotation = false;
        
        _spawnPoint = transform.position;
        Target = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable()
    {
        _unit.onSpawn += CombatState;
        _unit.onDead += DeadState;
    }

    private void OnDisable()
    {
        _unit.onSpawn -= CombatState;
        _unit.onDead -= DeadState;
    }

    private void FixedUpdate()
    {
        _myPos = transform.position;
        _tarPos = Target.position;

        Vector3 dir = _tarPos - _myPos; // Line to target
        Debug.DrawRay(_myPos, dir, Color.black);

        Vector3 perpDir = new Vector3(dir.normalized.z, 0, -dir.normalized.x) * _variance; // perp-line extended
        Debug.DrawRay(_myPos - (perpDir * 0.5f), perpDir, Color.white);
        
        Vector3 perpVar = new Vector3(perpDir.normalized.z, 0, -perpDir.normalized.x) * Random.Range(0.01f, 1f); // perp-line variance
        Debug.DrawRay(_myPos - (perpDir * 0.5f) - (perpVar * 0.5f), perpVar, Color.gray);
        
        if (Reacting())
            return;
        
        switch (State)
        {
            case NPCState.Idle:
                IdleState(); // walk randomly around _spawnPoint
                break;
            case NPCState.Combat:
                CombatState();
                break;
            case NPCState.Returning:
                ReturningState();
                break;
            case NPCState.Dead:
                break;
            default:
                Debug.Log($"You didn't make a case for {State}");
                break;
        }
    }
    
    private void LateUpdate() // Facing walk direction
    {
        if (_navMA.velocity.sqrMagnitude > Mathf.Epsilon)
            _character.rotation = Quaternion.Slerp(_character.rotation, Quaternion.LookRotation(_navMA.velocity.normalized), Time.deltaTime * _turnSpeed);
    }

    #region States
    private void IdleState()
    {
        if ((_myPos - _tarPos).sqrMagnitude < Mathf.Pow(_aggroRange, 2f))
        {
            State = NPCState.Combat;
            return;
        }
        
        _ray = new Ray(new Vector3(Random.Range(-5f, 5f), 2f, Random.Range(-5f, 5f)) + _spawnPoint, Vector3.down);
        Debug.DrawRay(_ray.origin, Vector3.down * _maxUseDistance, Color.yellow);
        
        MoveUnit();
    }
    private void CombatState()
    {
        if ((_myPos - _tarPos).sqrMagnitude > Mathf.Pow(_combatRange, 2f) || (_myPos - _spawnPoint).sqrMagnitude > Mathf.Pow(_spawnRange, 2f))
        {
            State = NPCState.Returning;
            return;
        }
        
        RandomizeMovePos();
        CastAbility(Random.Range(0, _unit.Creature.Abilities.Length));
        
        _ray = new Ray(new Vector3(0f, 2f, 0f) + _movePos, Vector3.down);
        Debug.DrawRay(new Vector3(0f, 2f, 0f) + _movePos, Vector3.down * _maxUseDistance, Color.red);
        
        MoveUnit();
    }
    private void ReturningState()
    {
        if ((_myPos - _tarPos).sqrMagnitude < Mathf.Pow(_returningRange, 2f) && (_myPos - _spawnPoint).sqrMagnitude < Mathf.Pow(_spawnRange, 2))
        {
            State = NPCState.Combat;
            return;
        }
        if ((_myPos - _spawnPoint).sqrMagnitude < 2f * 2f)
        {
            State = NPCState.Idle;
            return;
        }
        
        _ray = new Ray(new Vector3(0f, 2f, 0f) + _spawnPoint, Vector3.down);
        Debug.DrawRay(new Vector3(0f, 2f, 0f) + _spawnPoint, Vector3.down * _maxUseDistance, Color.cyan);
        
        MoveUnit();
    }
    private void DeadState()
    {
        State = NPCState.Dead;
        
        _ray = new Ray(_myPos + new Vector3(0f, 2f, 0f), Vector3.down);
        MoveUnit();
    }
    #endregion States
    
    #region Movement
    private void MoveUnit() => _navMA.SetDestination(Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer) ? _hit.point : _myPos);

    private void RandomizeMovePos()
    {
        int coin1 = Random.Range(0, 2),
            coin2 = Random.Range(0, 2);
        
                // Angles
        Vector3 tarDir = _tarPos - _myPos, // Line to target
                perpDir = new Vector3(tarDir.normalized.z, 0, -tarDir.normalized.x) * _variance, // perp-line extended
                perpVar = new Vector3(perpDir.normalized.z, 0, -perpDir.normalized.x) * Random.Range(0.01f, 1f), // perp-line variance
                
                // Sides
                pdRan = (coin1 == 0 ? (-perpDir * 0.5f) : (-perpDir * 0.5f) + perpDir),
                pvRan = (coin2 == 0 ? (-perpVar * 0.5f) : (-perpVar * 0.5f) + perpVar);
        
        _movePos = _myPos + pdRan + pvRan;
    }
    #endregion Movement
    
    #region Abilities
    private bool AbilityInRange(Ability ability)
    {
        float dis = ability.Base.IndHitBox.z * ability.Base.Deviation;

        _abiPoint = _tarPos + new Vector3(Random.Range(-_failRate, _failRate), 0f, Random.Range(-_failRate, _failRate));
        
        return (_myPos - _abiPoint).sqrMagnitude < Mathf.Pow(dis, 2f);
    }
    
    private Vector3 AbilityDestination()
    {
        _ray = new Ray(_abiPoint + new Vector3(0f, 2f, 0f), Vector3.down);
        
        return Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer)
            ? _hit.point : _myPos;
    }

    private void CastAbility(int slotNum)
    {
        while (_unit.Creature.Abilities[slotNum] == null) // Check if ability is equipped
            slotNum = Random.Range(0, _unit.Creature.Abilities.Length); // Go agane until find ability

        Ability curAbi = _unit.Creature.Abilities[slotNum];

        if (curAbi.Cooldown > 0)
            return;

        if (AbilityInRange(curAbi))
            _unit.Creature.PerformAbility(slotNum, AbilityDestination());
        else
            _movePos += _tarPos - _myPos; // Move closer to target
    }
    #endregion Abilities
    
    private bool Reacting()
    {
        if (_reactionTime < _reactionSpeed)
        {
            _reactionTime += Time.deltaTime;
            return true;
        }
        _reactionTime = 0;
        return false;
    }
}

public enum NPCState
{
    Idle,
    Combat,
    Returning,
    Dead
}