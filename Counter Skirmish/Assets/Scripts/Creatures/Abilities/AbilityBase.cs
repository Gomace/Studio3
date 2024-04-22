using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "CouSki/Abilities/Ability", order = -11)]
public class AbilityBase : ScriptableObject
{
    [SerializeField] private string _name;
    
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _model;

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
    [SerializeField] private bool _rescale = false;
    [SerializeField] private float _deviation = 1.75f;
    [SerializeField] private float _force = 750;
    [SerializeField] private int _hits = 1;
    [SerializeField] private Affectable _canAffect;
    [SerializeField] private CalcNumFrom _calcNumFrom;
    [SerializeField] private CalcMetric _metric;
    [SerializeField] private DmgStyle _style;

    // Extra Modifiers
    [SerializeField] private float _critChance = 1f, _critDamage = 1f;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public GameObject Model => _model;
    public string Description => _description;
    
    public Typing Type => _type;
    
    public int Power => _power;
    public float Cooldown => _cooldown;
    public int Resource => _resource;

    // public AbilityEffects Effects => _effects; // TODO remove this later
    
    // Ability Functionality Details
    public GameObject Indicator => _indicator;
    public Vector3 IndHitBox => _indhitBox * 0.01f;
    public bool Rescale => _rescale;
    public float Deviation => _deviation;
    public float Force => _force;
    public int Hits => _hits;
    
    // Extra Modifiers
    public float CritChance => _critChance;
    public float CritDamage => _critDamage;
    
    public string[] CanAffect(GameObject unit)
    {
        switch (_canAffect)
        {
            case Affectable.Enemy:
                switch (unit.tag)
                {
                    case "Player":
                        return new[] { "Enemy" };
                    case "Enemy":
                        return new[] { "Player" };
                }
                break;
            case Affectable.Friendly:
                switch (unit.tag)
                {
                    case "Player":
                        return new[] { "Player" };
                    case "Enemy":
                        return new[] { "Enemy" };
                }
                break;
            case Affectable.Any:
                return new[] { "Player", "Enemy" };
            default:
                Debug.Log($"You didn't make a case for {_canAffect}");
                break;
        }
        Debug.Log($"No Affectable was chosen on ability {name}");
        return new[] { "Enemy" };
    }

    public Creature PowerSource(Creature attacker, Creature defender)
    {
        switch (_calcNumFrom)
        {
            case CalcNumFrom.Self: 
                return attacker;
            case CalcNumFrom.Target: 
                return defender;
            case CalcNumFrom.Random:
                // Spawn circles around caster and target
            case CalcNumFrom.Group:
                // Spawn circles around target and caster
            case CalcNumFrom.PreSet:
                return null; // target doesn't matter, just deal the flat damage.
            default:
                Debug.Log($"You didn't make a case for {_calcNumFrom}");
                return attacker;
        }
    }

    public float Metric(Creature powerSource)
    {
        switch (_metric)
        {
            case CalcMetric.Physical:
                return powerSource.Physical;
            case CalcMetric.Magical:
                return powerSource.Magical;
            case CalcMetric.Defense:
                return powerSource.Defense;
            case CalcMetric.Resistance:
                return powerSource.Resistance;
            case CalcMetric.Speed:
                return powerSource.Speed;
            case CalcMetric.MaxHealth:
                return powerSource.MaxHealth;
            case CalcMetric.CurHealth:
                return powerSource.Health;
            case CalcMetric.MisHealth:
                return powerSource.MaxHealth - powerSource.Health;
            case CalcMetric.PreSet:
                return 1;
            default:
                Debug.Log($"You didn't make a case for {_metric}");
                return powerSource.Physical;
        }
    }

    public float Style(Creature defender, float metric)
    {
        switch (_style)
        {
            case DmgStyle.Physical:
                return metric - defender.Defense;
            case DmgStyle.Magical:
                return metric - defender.Resistance;
            case DmgStyle.Mixed:
                return ((metric / 2) - (defender.Defense / 2)) + ((metric / 2) - (defender.Resistance / 2));
            case DmgStyle.Direct:
                return metric - defender.Health;
            case DmgStyle.Stat:
                return 0;
            default:
                Debug.Log($"No CalcMetric was chosen on ability {name}");
                return 1;
        }
    }

    private enum Affectable
    {
        Enemy,
        Friendly,
        Ally,
        Self,
        Other,
        Any
    }
    
    private enum CalcNumFrom
    {
        Self,
        Target,
        Random,
        Group,
        PreSet
    }

    private enum CalcMetric
    {
        Physical,
        Magical,
        Defense,
        Resistance,
        Speed,
        MaxHealth,
        CurHealth,
        MisHealth,
        PreSet
    }

    private enum DmgStyle
    {
        Physical,
        Magical,
        Direct,
        Mixed,
        Stat
    }
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