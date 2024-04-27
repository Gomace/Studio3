
using System;
using UnityEngine;

[Serializable]
public class CreatureInfo
{
    [SerializeField] private CreatureBase _base;
    [SerializeField] private int _level = 1, _exp = 0;
    
    [SerializeField] private bool _rental = false;

    [SerializeField] private AbilityBase[] _abilityBases = new AbilityBase[4];
    [SerializeField] private PassiveBase _passiveBase;

    public CreatureBase Base => _base;
    public int Level => _level;
    public int Exp => _exp;
    public bool Rental => _rental;

    public AbilityBase[] AbilityBases
    {
        get => _abilityBases;
        set => _abilityBases = value;
    }
    public PassiveBase PassiveBase
    {
        get => _passiveBase;
        set => _passiveBase = value;
    }

    public CreatureInfo(CreatureBase cBase, int level, int exp, bool rental = false)
    {
        _base = cBase;
        _level = level;
        _exp = exp;
        
        _rental = rental;
    }
}