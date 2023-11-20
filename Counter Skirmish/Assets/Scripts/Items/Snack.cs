using UnityEngine;

[CreateAssetMenu(fileName = "Snack", menuName = "CouSki/Item/Snack")]
public class Snack : ItemBase
{
    public ItemBase Base { get; set; }
    public int Price { get; set; }

    public Snack(ItemBase iBase)
    {
        Base = iBase;
        Price = iBase.Price;
    }
}
