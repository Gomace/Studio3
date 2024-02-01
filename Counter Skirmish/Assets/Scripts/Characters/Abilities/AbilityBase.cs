using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "CouSki/Abilities/Ability", order = -11)]
public class AbilityBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    [TextArea]
    [SerializeField] private string _description;
    
    [SerializeField] private Typing _type;

    [SerializeField] private int _power;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _resource, _range;
    
    [Header("Ability Functionality Details")]
    [SerializeField] private Targeting _targeting;
    [SerializeField] private AbiClass _abiClass;
    [SerializeField] private CalcNumFrom _calcNumFrom;
    [SerializeField] private CalcMetric _metric;
    [SerializeField] private DmgStyle _style;

    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    
    public Typing Type => _type;
    
    public Targeting Targeting => _targeting;
    public AbiClass AbiClass => _abiClass;
    public CalcNumFrom CalcNumFrom => _calcNumFrom;
    public CalcMetric Metric => _metric;
    public DmgStyle Style => _style;

    public int Power => _power;
    public float Cooldown => _cooldown;
    public int Resource => _resource;
    public int Range => _range;
}