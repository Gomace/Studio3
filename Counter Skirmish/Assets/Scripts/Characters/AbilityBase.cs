using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class AbilityBase : ScriptableObject
{
    [SerializeField] private string _name;

    [TextArea]
    [SerializeField] private string _description;

    [SerializeField] private TypeBase _type;
    [SerializeField] private int _power, _cooldown, _resource;
    
    public string Name => _name;
    public string Description => _description;
    public TypeBase Type => _type;
    
    public int Power => _power;
    public int Cooldown => _cooldown;
    public int Resource => _resource;
}