using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "CouSki/Abilities/Ability", order = -11)]
public class AbilityBase : ScriptableObject
{
    [SerializeField] private Sprite _icon;

    //private GameObject[] thing = new GameObject[6]; TODO what is this?
    
    [TextArea]
    [SerializeField] private string _description;
    
    [SerializeField] private Typing _type;

    [SerializeField] private int _power;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _resource;

    // [SerializeField] private AbilityEffects _effects; // TODO remove this later
    
    // Ability Functionality Details
    [SerializeField] private GameObject _indicator;
    [SerializeField] private Vector3 _indhitBox = new (75f, 100f, 500f);
    [SerializeField] private int _hits = 1;
    [SerializeField] private string[] _canAffect;
    [SerializeField] private AbiClass _abiClass;
    [SerializeField] private CalcNumFrom _calcNumFrom;
    [SerializeField] private CalcMetric _metric;
    [SerializeField] private DmgStyle _style;

    // Extra Modifiers
    [SerializeField] private float _critChance = 1f, _critDamage = 1f;
    
    public string Name => name;
    public Sprite Icon => _icon;
    public string Description => _description;
    
    public Typing Type => _type;
    
    public int Power => _power;
    public float Cooldown => _cooldown;
    public int Resource => _resource;

    // public AbilityEffects Effects => _effects; // TODO remove this later
    
    // Ability Functionality Details
    public GameObject Indicator => _indicator;
    public Vector3 IndHitBox => _indhitBox * 0.01f;
    public int Hits => _hits;
    public string[] CanAffect => _canAffect;
    public AbiClass AbiClass => _abiClass;
    public CalcNumFrom CalcNumFrom => _calcNumFrom;
    public CalcMetric Metric => _metric;
    public DmgStyle Style => _style;
    
    // Extra Modifiers
    public float CritChance => _critChance;
    public float CritDamage => _critDamage;
}

[System.Serializable]
public class AbilityEffects
{
    [SerializeField] private StatBoost[] _boosts;

    public StatBoost[] Boosts => _boosts;
}

[System.Serializable]
public class StatBoost
{
    public Stat Stat { get; set; }
    public int Boost { get; set; }
}