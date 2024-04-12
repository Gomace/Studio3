using System;
using UnityEngine;

[Serializable]
public class CreatureInfo
{
    [SerializeField] private CreatureBase _base;
    [SerializeField] private int _level = 1, _exp = 0;

    [SerializeField] private AbilityBase[] _abilityBases = new AbilityBase[4];
    [SerializeField] private PassiveBase _passiveBase;

    public CreatureBase Base { get; private set; }
    public int Level { get; private set; }
    public int Exp { get; private set; }

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

    public CreatureInfo(CreatureBase cBase, int level, int exp)
    {
        Base = cBase;
        Level = level;
        Exp = exp;
    }
}