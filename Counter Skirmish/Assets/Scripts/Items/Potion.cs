using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Item/Potion")]
public class Potion : ItemBase
{
    public ItemBase Base { get; set; }
    public int Price { get; set; }

    public Potion(ItemBase iBase)
    {
        Base = iBase;
        Price = iBase.Price;
    }
}