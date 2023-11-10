using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField] private RectTransform _categories, _shops;
    [SerializeField] private Color _clickColor, _normalColor;
    
    [SerializeField] private ItemBase[] _wares;
    
    public void SelectCategory(GameObject category)
    {
        foreach (RectTransform btn in _categories)
            btn.GetComponent<Image>().color = _normalColor;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = _clickColor;
        
        foreach (RectTransform shop in _shops)
            shop.gameObject.SetActive(false);
        category.SetActive(true);
    }
    
    public void PurchaseItem()
    {
        
    }
}
