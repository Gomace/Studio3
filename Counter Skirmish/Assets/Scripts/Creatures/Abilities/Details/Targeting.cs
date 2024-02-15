using UnityEngine;

[CreateAssetMenu(fileName = "TargMethod", menuName = "CouSki/Abilities/TargetingMethod")]
public class Targeting : ScriptableObject
{
    [SerializeField] private string _name;

    [TextArea]
    [SerializeField] private string _description;

    public string Name => _name;
    public string Description => _description;
}