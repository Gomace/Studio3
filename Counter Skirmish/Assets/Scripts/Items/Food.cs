using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Item/Food")]
public class Food : ItemBase
{
    public ItemBase Base { get; set; }
    public int Price { get; set; }

    public Food(ItemBase iBase)
    {
        Base = iBase;
        Price = iBase.Price;
    }
}
