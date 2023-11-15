using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private Sprite icon;
    
    [SerializeField] private Quality quality;
    [SerializeField] private int price;
    
    public string Name => name;
    public string Description => description;
    public Sprite Icon => icon;
    public Quality Quality => quality;
    public int Price => price;
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