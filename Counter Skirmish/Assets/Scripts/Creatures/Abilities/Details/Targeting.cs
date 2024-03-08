using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TargMethod", menuName = "CouSki/Abilities/TargetingMethod")]
public class Targeting : ScriptableObject
{
    [SerializeField] private string _name;

    [TextArea]
    [SerializeField] private string _description;

    [SerializeField] private GameObject _indicator;
    [SerializeField] private bool _showRange = false;
    
    public string Name => _name;
    public string Description => _description;

    public GameObject Indicator => _indicator;
    public bool ShowRange => _showRange;
}