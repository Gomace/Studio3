using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Transform _testPlayer;

    // Movement
    private Vector3 _movePos, _spawnPoint, _myPos, _tarPos;
    private NavMeshAgent _navMA;
    private readonly float _variance = 5f, _reactionSpeed = 1/3.2f;
    private float _reactionTime = 0f;

    // Raycast
    private const float _maxUseDistance = 6f;
    private Ray _ray;
    private RaycastHit _hit;
    private readonly LayerMask _groundLayer = (1 << 6);
    
    public NPCState State { get; private set; } = NPCState.Idle;
    public Transform Target { get; set; }

    private void Awake()
    {
        _spawnPoint = transform.position;

        Target = _testPlayer;
        
        _navMA = GetComponent<NavMeshAgent>();
        _navMA.updateRotation = false;
    }
    
    private void FixedUpdate()
    {
        _myPos = transform.position;
        _tarPos = Target.position;

        Vector3 dir = _tarPos - _myPos; // Line to target
        Debug.DrawRay(_myPos, dir, Color.black);

        Vector3 perpDir = new Vector3(dir.normalized.z, 0, -dir.normalized.x) * _variance; // perp-line extended
        Debug.DrawRay(_myPos - (perpDir * 0.5f), perpDir, Color.white);
        
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
            default:
                Debug.Log($"You didn't make a case for {State}");
                break;
        }
    }
    
    private void LateUpdate() // Facing walk direction
    {
        if (_navMA.velocity.sqrMagnitude > Mathf.Epsilon)
            _character.rotation = Quaternion.LookRotation(_navMA.velocity.normalized);
    }
    
    private void IdleState()
    {
        if ((_myPos - _tarPos).sqrMagnitude < 10f * 10f)
        {
            State = NPCState.Combat;
            return;
        }
        
        _ray = new Ray(new Vector3(Random.Range(-5f, 5f), 2f, Random.Range(-5f, 5f)) + _spawnPoint, Vector3.down);
        Debug.DrawRay(_ray.origin, Vector3.down * _maxUseDistance, Color.yellow);
        
        MoveUnit(MoveRay());
    }
    private void CombatState()
    {
        if ((_myPos - _tarPos).sqrMagnitude > 12f * 12f || (_myPos - _spawnPoint).sqrMagnitude > 20f * 20f)
        {
            State = NPCState.Returning;
            return;
        }

        RandomizeMovePos();
        
        _ray = new Ray(new Vector3(0f, 2f, 0f) + _movePos, Vector3.down);
        Debug.DrawRay(new Vector3(0f, 2f, 0f) + _movePos, Vector3.down * _maxUseDistance, Color.red);
        
        MoveUnit(MoveRay());
    }
    private void ReturningState()
    {
        if ((_myPos - _tarPos).sqrMagnitude < 7 * 7 && (_myPos - _spawnPoint).sqrMagnitude < 20f * 20f)
        {
            State = NPCState.Combat;
            return;
        }
        if ((_myPos - _spawnPoint).sqrMagnitude < 2f)
        {
            State = NPCState.Idle;
            return;
        }
        
        _ray = new Ray(new Vector3(0f, 2f, 0f) + _spawnPoint, Vector3.down);
        Debug.DrawRay(new Vector3(0f, 2f, 0f) + _spawnPoint, Vector3.down * _maxUseDistance, Color.cyan);
        
        MoveUnit(MoveRay());
    }

    private void MoveUnit(Vector3 mouse) => _navMA.SetDestination(mouse);
    private Vector3 MoveRay()
    {
        return Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer)
            ? _hit.point : _myPos;
    }

    private void RandomizeMovePos()
    {
        Vector3 tarDir = _tarPos - _myPos; // Line to target
        Vector3 perpDir = new Vector3(tarDir.normalized.z, 0, -tarDir.normalized.x) * _variance; // perp-line extended
        Vector3 perpVar = new Vector3(perpDir.normalized.z, 0, -perpDir.normalized.x) * Random.Range(0.01f, 1f); // perp-line variance

        int coin = Random.Range(0, 2);
        _movePos = (coin == 0 ? _myPos - (perpDir * 0.5f) : _myPos - (perpDir * 0.5f) + perpDir);
    }
    
    public Vector3 AbilityDestination(Vector3 variance)
    {
        _ray = new Ray(_myPos + new Vector3(0f, 2f, 0f) + _movePos, Vector3.down);
        
        return Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer)
            ? _hit.point : _myPos;
    }
    
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
    Returning
}