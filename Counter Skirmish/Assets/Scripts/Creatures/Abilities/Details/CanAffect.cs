using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(fileName = "CanAffect", menuName = "CouSki/Abilities/CanAffect")]
public class CanAffect : ScriptableObject
{
    [SerializeField] private string _name;

    [TextArea]
    [SerializeField] private string _description;

    [SerializeField] private string[] _affected;
    
    public string Name => _name;
    public string Description => _description;

    public string[] Affected => _affected;
}