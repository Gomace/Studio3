using UnityEngine;

[CreateAssetMenu(fileName = "Net", menuName = "Item/Net")]
public class Net : ItemBase
{
    public ItemBase Base { get; set; }
    public int Price { get; set; }

    public Net(ItemBase iBase)
    {
        Base = iBase;
        Price = iBase.Price;
    }
}