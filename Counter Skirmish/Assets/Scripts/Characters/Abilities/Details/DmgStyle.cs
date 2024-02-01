using UnityEngine;

[CreateAssetMenu(fileName = "DmgStyle", menuName = "CouSki/Abilities/DamageStyle")]
public class DmgStyle : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    
    [TextArea]
    [SerializeField] private string _description;

    public string Name => _name;
    public Sprite Icon => _icon;
    public string Description => _description;
}