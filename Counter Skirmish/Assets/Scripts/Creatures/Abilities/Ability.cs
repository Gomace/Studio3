using System;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    private List<Creature> _affected;
    private string[] _canAffect;

    public AbilityBase Base { get; set; }
    public float Cooldown { get; set; }

    public Ability(AbilityBase cBase)
    {
        Base = cBase;
        Cooldown = cBase.Cooldown;
    }

    public void Cast(GameObject unit, Creature creature, bool modifier) // Up down button toggle Shift
    {
        _canAffect = Base.CanAffect.Affected;
        Base.Targeting.TargetingMethod(unit, creature, modifier, Base.Range);
        
        
    }
}