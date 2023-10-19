using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/Create new Creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] private string name;
    
    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject model;

    [SerializeField] private CreatureType type1, type2;
    
    // BASE STATS
    [SerializeField] private int maxHealth, attack, magic, defense, resistance, speed;

    [SerializeField] private List<LearnableAbility> learnableAbilities;

    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;
    public GameObject Model => model;
    public int MaxHealth => maxHealth;
    public int Attack => attack;
    public int Magic => magic;
    public int Defense => defense;
    public int Resistance => resistance;
    public int Speed => speed;
    
    public List<LearnableAbility> LearnableAbilities => learnableAbilities;
}

[System.Serializable]
public class LearnableAbility
{
    [SerializeField] private AbilityBase abilityBase;
    [SerializeField] private int level;
    
    public AbilityBase Base => abilityBase;
    public int Level => level;
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