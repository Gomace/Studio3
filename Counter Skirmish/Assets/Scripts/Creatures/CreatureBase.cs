using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "CouSki/Creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] private string _name;
    
    [TextArea]
    [SerializeField] private string _description;

    [SerializeField] private Sprite _icon, _card, _splash;
    [SerializeField] private GameObject _model;

    [SerializeField] private Typing _type1, _type2;
    [SerializeField] private Role _role;

    // Base Stats
    [SerializeField] private int _maxHealth, _maxResource, _physical, _magical, _defense, _resistance, _speed;
    
    // Extra Modifiers
    [SerializeField] private float _critChance = 1f, _critDamage = 1f;

    [SerializeField] private LearnableAbility[] _learnableAbilities;
    [SerializeField] private PossiblePassives[] _possiblePassives;

    public string Name => _name;
    public string Description => _description;
    
    public Sprite Icon => _icon;
    public Sprite Card => _card;
    public Sprite Splash => _splash;
    public GameObject Model => _model;

    public Typing Type1 => _type1;
    public Typing Type2 => _type2;

    public Role Role => _role;
    
    // Base Stats
    public int MaxHealth => _maxHealth;
    public int MaxResource => _maxResource;
    public int Physical => _physical;
    public int Magical => _magical;
    public int Defense => _defense;
    public int Resistance => _resistance;
    public int Speed => _speed;
    
    // Extra Modifiers
    public float CritChance => _critChance;
    public float CritDamage => _critDamage;
    
    public LearnableAbility[] LearnableAbilities => _learnableAbilities;
    public PossiblePassives[] PossiblePassives => _possiblePassives;

    public float GetEffectiveness(Typing hitType)
    {
        float multiplier = 1;

        if (Type1 != null)
            multiplier *= Type1.TypeChart(hitType);
        if (Type2 != null)
            multiplier *= Type2.TypeChart(hitType);
        
        return multiplier;
    }
}

[System.Serializable]
public class LearnableAbility
{
    [SerializeField] private AbilityBase _abilityBase;
    [SerializeField] private int _level;
    
    public AbilityBase Base => _abilityBase;
    public int Level => _level;
}

[System.Serializable]
public class PossiblePassives
{
    [SerializeField] private PassiveBase _passiveBase;
    
    public PassiveBase Base => _passiveBase;
}

public enum Stat
{
    Physical,
    Magical,
    Defense,
    Resistance,
    Speed
}