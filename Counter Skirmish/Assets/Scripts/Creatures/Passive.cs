using System;
using UnityEngine;

[Serializable]
public class Passive
{
    [SerializeField] private PassiveBase _base;

    public PassiveBase Base
    {
        get => _base;
        set => _base = value;
    }
    
    public Passive(PassiveBase pBase)
    {
        _base = pBase;
    }
}
