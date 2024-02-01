using UnityEngine;

[CreateAssetMenu(fileName = "AbiClass", menuName = "CouSki/Abilities/AbilityClass")]
public class AbiClass : ScriptableObject
{
    [SerializeField] private string _name;
    
    [TextArea]
    [SerializeField] private string _description;

    public string Name => _name;
    public string Description => _description;
}