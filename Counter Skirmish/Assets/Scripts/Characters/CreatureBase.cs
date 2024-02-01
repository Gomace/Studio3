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

    [SerializeField] private Typing _type1, _type2;
    [SerializeField] private Role _role;

    // BASE STATS
    [SerializeField] private int _maxHealth, _physical, _magical, _defense, _resistance, _speed;

    [SerializeField] private List<LearnableAbility> _learnableAbilities;

    public string Name => _name;
    public string Description => _description;
    
    public Sprite Icon => _icon;
    public Sprite Card => _card;
    public Sprite Splash => _splash;
    public GameObject Model => _model;

    public Typing Type1 => _type1;
    public Typing Type2 => _type2;

    public Role Role => _role;
    
    public int MaxHealth => _maxHealth;
    public int Physical => _physical;
    public int Magical => _magical;
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
    Arcane,
    Dark,
    Earth,
    Energy,
    Entropy,
    Ethereal,
    Fire,
    Forest,
    Host,
    Light,
    Matter,
    Predator,
    Radioactive,
    Swamp,
    Tech,
    Void,
    Water,
    Wind
}

public class TypeChart
{
    public static float[][] chart =
    {
        //                   ARC    DRK   ERT   NRG   NTP   ETH   FIR   FOR   HOS   LIT   MTR   PRD   RAD   SWP   TEC   VOI   WAT   WIN
        /*ARC*/new float[] { 1f,    1f,   1f,  0.5f, 1.5f, 1.5f,  1f,   1f,   1f,  1.5f,  1f,   1f,   1f,  0.5f, 1.5f, 0.5f,  1f,   1f },
        /*DRK*/new float[] { 1.5f, 0.5f,  1f,   1f,   1f,  0.5f, 0.5f,  1f,   1f,  1.5f,  1f,  0.5f, 0.5f, 0.5f, 0.5f,  1f,   1f,  1.5f },
        /*ERT*/new float[] { 1f,    1f,  0.5f,  1f,   1f,  0.5f, 1.5f,  1f,   1f,   1f,   1f,  1.5f,  1f,  0.5f, 1.5f, 0.5f, 0.5f,  1f },
        /*NRG*/new float[] { 1f,   1.5f,  1f,   1f,  0.5f,  1f,   1f,   1f,   1f,  0.5f,  1f,   1f,  0.5f,  1f,   1f,  0.5f, 1.5f,  1f },
        /*NTP*/new float[] { 1.5f, 0.5f, 1.5f,  1f,   1f,   1f,  0.5f, 1.5f, 1.5f,  1f,  1.5f,  1f,   1f,   1f,  1.5f,  1f,  0.5f, 0.5f },
        /*ETH*/new float[] { 1f,   1.5f, 0.5f,  1f,   1f,   1f,  0.5f,  1f,  1.5f, 0.5f, 0.5f, 0.5f, 1.5f,  1f,   1f,  0.5f,  1f,   1f },
        /*FIR*/new float[] { 1f,   1.5f, 0.5f, 0.5f,  1f,  1.5f, 0.5f, 1.5f, 1.5f, 0.5f,  1f,   1f,   1f,   1f,  1.5f,  1f,  0.5f,  1f },
        /*FOR*/new float[] { 1f,    1f,   1f,   1f,   1f,   1f,  0.5f,  1f,   1f,  1.5f,  1f,   1f,  0.5f,  1f,  1.5f,  1f,  1.5f, 0.5f },
        /*HOS*/new float[] { 1f,    1f,  1.5f,  1f,  0.5f, 0.5f, 0.5f, 1.5f,  1f,   1f,  1.5f, 1.5f, 0.5f,  1f,  0.5f,  1f,   1f,   1f },
        /*LIT*/new float[] { 1.5f, 1.5f, 0.5f,  1f,   1f,  1.5f, 0.5f, 0.5f,  1f,  0.5f, 0.5f,  1f,  0.5f, 1.5f,  1f,  1.5f,  1f,  0.5f },
        /*MTR*/new float[] { 0.5f, 0.5f,  1f,  0.5f, 0.5f, 0.5f,  1f,   1f,   1f,  0.5f,  1f,  1.5f,  1f,   1f,  1.5f, 0.5f,  1f,  0.5f },
        /*PRD*/new float[] { 1f,   1.5f, 0.5f,  1f,   1f,  0.5f, 0.5f,  1f,  1.5f, 0.5f,  1f,  1.5f,  1f,   1f,  0.5f, 0.5f,  1f,  0.5f },
        /*RAD*/new float[] { 0.5f,  1f,  0.5f, 0.5f, 1.5f, 1.5f, 1.5f,  1f,   1f,  0.5f, 1.5f, 1.5f, 0.5f,  1f,  1.5f, 0.5f,  1f,   1f },
        /*SWP*/new float[] { 1f,    1f,  1.5f,  1f,   1f,   1f,   1f,   1f,  0.5f, 0.5f,  1f,   1f,  1.5f,  1f,  1.5f,  1f,  0.5f, 1.5f },
        /*TEC*/new float[] { 1f,   1.5f, 1.5f, 1.5f, 0.5f, 0.5f,  1f,  1.5f,  1f,   1f,  1.5f, 1.5f,  1f,  0.5f,  1f,  0.5f, 0.5f,  1f },
        /*VOI*/new float[] { 1.5f,  1f,  1.5f, 1.5f,  1f,   1f,  1.5f,  1f,  1.5f, 0.5f, 1.5f, 1.5f, 0.5f, 1.5f, 1.5f, 0.5f, 1.5f, 0.5f },
        /*WAT*/new float[] { 1f,   0.5f, 1.5f,  1f,   1f,   1f,  1.5f, 0.5f, 1.5f,  1f,   1f,   1f,   1f,  0.5f, 1.5f, 0.5f, 0.5f,  1f},
        /*WIN*/new float[] { 1f,    1f,  0.5f, 0.5f,  1f,  1.5f, 1.5f,  1f,   1f,  0.5f, 0.5f,  1f,   1f,  1.5f, 0.5f, 0.5f,  1f,  0.5f},
    };

    public static float GetEffectiveness(CreatureType attackType, CreatureType defenseType)
    {
        if (attackType == CreatureType.None || defenseType == CreatureType.None)
            return 1;

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}