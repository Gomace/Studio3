using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "CouSki/Creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] private string _name;
    
    [TextArea]
    [SerializeField] private string _description;

    [SerializeField] private Sprite _icon, _card, _splash;
    [SerializeField] private GameObject _model;

    [SerializeField] private TypeBase _type1, _type2;
    [SerializeField] private RoleBase _role;

    // BASE STATS
    [SerializeField] private int _maxHealth, _attack, _magic, _defense, _resistance, _speed;

    [SerializeField] private List<LearnableAbility> _learnableAbilities;

    public string Name => _name;
    public string Description => _description;
    
    public Sprite Icon => _icon;
    public Sprite Card => _card;
    public Sprite Splash => _splash;
    public GameObject Model => _model;

    public TypeBase Type1 => _type1;
    public TypeBase Type2 => _type2;

    public RoleBase Role => _role;
    
    public int MaxHealth => _maxHealth;
    public int Attack => _attack;
    public int Magic => _magic;
    public int Defense => _defense;
    public int Resistance => _resistance;
    public int Speed => _speed;
    
    public List<LearnableAbility> LearnableAbilities => _learnableAbilities;
}

[System.Serializable]
public class LearnableAbility
{
    [SerializeField] private AbilityBase _abilityBase;
    [SerializeField] private int _level;
    
    public AbilityBase Base => _abilityBase;
    public int Level => _level;
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