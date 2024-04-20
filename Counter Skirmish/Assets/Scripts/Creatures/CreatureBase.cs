using System;
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

    [SerializeField] private int _expYield;
    [SerializeField] private GrowthGroup _growthRate;
    
    [SerializeField] private int _catchRate = 255;

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

    public int ExpYield => _expYield;
    public GrowthGroup GrowthRate => _growthRate;
    public int CatchRate => _catchRate;
    
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
    
    public int GetExpForLevel(int lvl)
    {
        if (lvl == 1)
            return 0;
        
        switch (_growthRate)
        {
            case GrowthGroup.Erratic:
                switch (lvl)
                {
                    case < 50:
                        return ((int)Math.Pow(lvl, 3) * (100 - lvl)) / 50;
                    case >= 50 and < 68:
                        return ((int)Math.Pow(lvl, 3) * (150 - lvl)) / 100;
                    case >= 68 and < 98:
                        return ((int)Math.Pow(lvl, 3) * ((1911 - 10 * lvl) / 3)) / 500;
                    case >= 98 and < 100:
                        return ((int)Math.Pow(lvl, 3) * (160 - lvl)) / 100;
                    default:
                        return 600000;
                }
            case GrowthGroup.Fast:
                return 4 * (int)Math.Pow(lvl, 3) / 5;
            case GrowthGroup.MediumFast:
                return (int)Math.Pow(lvl, 3);
            case GrowthGroup.MediumSlow:
                return 6 / 5 * (int)Math.Pow(lvl, 3) - 15 * (int)Math.Pow(lvl, 2) + 100 * lvl - 140;
            case GrowthGroup.Slow:
                return 5 * (int)Math.Pow(lvl, 3) / 4;
            case GrowthGroup.Fluctuating:
                switch (lvl)
                {
                    case < 15:
                        return ((int)Math.Pow(lvl, 3) * (((lvl + 1) / 3) + 24)) / 50;
                    case >= 15 and < 36:
                        return ((int)Math.Pow(lvl, 3) * (lvl + 14)) / 50;
                    case >= 36 and < 100:
                        return ((int)Math.Pow(lvl, 3) * ((lvl / 2) + 32)) / 50;
                    default:
                        return 1640000;
                }
            default:
                Debug.Log($"You didn't make a case for {_growthRate}");
                return (int)Math.Pow(lvl, 3);
        }
    }
}

[Serializable]
public class LearnableAbility
{
    [SerializeField] private AbilityBase _abilityBase;
    [SerializeField] private int _level;
    
    public AbilityBase Base => _abilityBase;
    public int Level => _level;
}

[Serializable]
public class PossiblePassives
{
    [SerializeField] private PassiveBase _passiveBase;
    
    public PassiveBase Base => _passiveBase;
}

public enum GrowthGroup
{   
    Erratic,
    Fast,
    MediumFast,
    MediumSlow,
    Slow,
    Fluctuating
}

public enum Stat
{
    Physical,
    Magical,
    Defense,
    Resistance,
    Speed
}