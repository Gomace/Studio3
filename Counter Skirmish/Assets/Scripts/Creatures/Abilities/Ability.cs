using System;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    private List<Creature> _affected;
    private string[] _canAffect;
    private GameObject _indicator;
    private bool _showRange;

    public AbilityBase Base { get; set; }
    public float Cooldown { get; set; }

    public Ability(AbilityBase cBase)
    {
        Base = cBase;
        Cooldown = cBase.Cooldown;
        
        // _canAffect = Base.CanAffect.Affected;
        _showRange = Base.Targeting.ShowRange;
    }

    public void Cast(InstanceUnit unit, Creature creature, bool modifier) // Up down button toggle Shift
    {
        _indicator = unit.Indicators[Base.Targeting.Indicator.name];
        // Use Range and width to make hitbox
        
        if (modifier)
        {
            unit.GetComponent<PlayerMovement>().ShowIndicator(_indicator);
            
            return;
        }
        else
            Debug.Log("You did not hold the modifier");
        Debug.Log("Just wanted to check.");
    }
}