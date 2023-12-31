using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public delegate void OnShopLoad();
    public static event OnShopLoad onShopLoad;
    
    [SerializeField] private RectTransform _categories, _shops;
    [SerializeField] private Color _clickColor = new Color(55, 55, 55, 255),
                                    _normalColor = new Color(128, 128, 128, 255);

    private void OnEnable() => LoadShop();

    public void LoadShop() => onShopLoad?.Invoke();

    public void SelectCategory(GameObject category)
    {
        foreach (RectTransform btn in _categories)
            btn.GetComponent<Image>().color = _normalColor;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = _clickColor;

        foreach (RectTransform shop in _shops)
            shop.gameObject.SetActive(false);
        category.SetActive(true);
        
        LoadShop();
    }

    public void PurchaseItem()
    {
        //EventSystem.current.currentSelectedGameObject.GetComponent<ItemInfo>().Base; // the item you bought
        LoadShop();
    }
}
