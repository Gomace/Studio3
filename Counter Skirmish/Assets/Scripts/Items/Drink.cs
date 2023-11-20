using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "CouSki/Item/Drink")]
public class Drink : ItemBase
{
    public ItemBase Base { get; set; }
    public int Price { get; set; }

    public Drink(ItemBase iBase)
    {
        Base = iBase;
        Price = iBase.Price;
    }
}
