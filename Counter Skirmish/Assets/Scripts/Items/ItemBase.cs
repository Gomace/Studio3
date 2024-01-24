using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] private string _name;

    [TextArea]
    [SerializeField] private string _description;

    [SerializeField] private Sprite _icon;
    
    [SerializeField] private Quality _quality;
    [SerializeField] private int _price;
    
    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    public Quality Quality => _quality;
    public int Price => _price;
}

public enum Quality
{
    None,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythic
}