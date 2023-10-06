using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/Create new Creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] private string name;
    
    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private CreatureType type1, type2;
    
    // BASE STATS
    [SerializeField] private int maxHp, attack, magic, defense, resistance, speed;

    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public int MaxHp { get { return maxHp; } }
    public int Attack { get { return attack; } }
    public int Magic { get { return magic; } }
    public int Defense { get { return defense; } }
    public int Resistance { get { return resistance; } }
    public int Speed { get { return speed; } }
}

public enum CreatureType
{
    None,
    Fire,
    Source,
    Forest,
    Earth,
    Predator,
    Energy,
    Light,
    Water,
    Swamp,
    Dark,
    Matter,
    Tech,
    Wind,
    Host,
    Void,
    Entropy
}