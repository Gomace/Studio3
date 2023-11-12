using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionMenu : MonoBehaviour
{
    #region Events
    public delegate void OnCollectionLoad();
    public static event OnCollectionLoad onCollectionLoad;
    
    #endregion Events
    
    [SerializeField] private RectTransform _categories, _shops;
    [SerializeField] private Color _clickColor, _normalColor;

    private void OnEnable() => LoadShop();

    public void LoadShop() => onCollectionLoad?.Invoke();

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