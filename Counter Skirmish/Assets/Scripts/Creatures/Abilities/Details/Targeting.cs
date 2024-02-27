using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TargMethod", menuName = "CouSki/Abilities/TargetingMethod")]
public class Targeting : ScriptableObject
{
    [SerializeField] private string _name;

    [TextArea]
    [SerializeField] private string _description;

    [SerializeField] private GameObject _indicator;
    [SerializeField] private bool _groundRange = false;
    
    public string Name => _name;
    public string Description => _description;

    public void TargetingMethod(GameObject unit, Creature creature, bool modifier, int range)
    {
        if (modifier)
            unit.GetComponent<PlayerMovement>().TargetingIndicator(_indicator, _groundRange, range);
        
        
    }
}