using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "CouSki/Item/Tool")]
public class Tool : ItemBase
{
    public ItemBase Base { get; set; }
    public int Price { get; set; }

    public Tool(ItemBase iBase)
    {
        Base = iBase;
        Price = iBase.Price;
    }
}
