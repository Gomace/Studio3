using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InstanceUnit))]
[RequireComponent(typeof(NPCMovement))]
public class FightBehaviour : MonoBehaviour
{
    private InstanceUnit _unit;
    private NPCMovement _movement;

    private Vector3 _targetPos;
    
    private int _curSlot;
    
    private int _CurAbility
    {
        get => _curSlot;
        set
        {
            _curSlot = value;
            CastAbility();
        }
    }
    
    private void Awake()
    {
        _unit = GetComponent<InstanceUnit>();
        _movement = GetComponent<NPCMovement>();
    }

    private void FixedUpdate()
    {
        Debug.Log($"{_unit.Creature.Base.Name}'s state is: {_movement.State}");
        
        if (_movement.State != NPCState.Combat)
            return;
        
        // square the distance we compare with
        //if (offset.sqrMagnitude < closeDistance * closeDistance)
    }

    private void SelectAbility()
    {
        _CurAbility = Random.Range(0, 4);
    }

    private void CastAbility()
    {
        if (_unit.Creature.Abilities[_CurAbility] == null) // Check if ability is equipped
            return;
        
        Ability curAbi = _unit.Creature.Abilities[_CurAbility];

        if (curAbi.Cooldown > 0)
            return;
        
        _unit.Creature.PerformAbility(_CurAbility, _movement.AbilityDestination(MissVariance(_targetPos)));
    }

    private Vector3 MissVariance(Vector3 targetPos)
    {

        return Vector3.zero;
    }
}
