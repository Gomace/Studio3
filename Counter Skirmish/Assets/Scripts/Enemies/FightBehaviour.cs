using UnityEngine;

[RequireComponent(typeof(InstanceUnit))]
[RequireComponent(typeof(BasicEnemy))] //[RequireComponent(typeof(NPCMovement))] change this at some point
public class FightBehaviour : MonoBehaviour
{
    private InstanceUnit _unit;
    private BasicEnemy _movement; //NPCMovement change this at some point

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
        _movement = GetComponent<BasicEnemy>(); //NPCMovement change this at some point
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
