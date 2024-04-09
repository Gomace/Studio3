using UnityEngine;

[CreateAssetMenu(fileName = "Passive", menuName = "CouSki/Passive")]
public class PassiveBase : ScriptableObject
{
    [SerializeField] private string _name;
    
    [SerializeField] private Sprite _icon;

    [TextArea]
    [SerializeField] private string _description;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public string Description => _description;
}
