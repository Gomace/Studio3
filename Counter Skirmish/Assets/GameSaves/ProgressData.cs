using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressData
{
    private int _level;
    
    public ProgressData(Creature creature)
    {
        _level = creature.Level;
    }
}