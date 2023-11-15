using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionMenu : MonoBehaviour
{
    public delegate void OnCollectionLoad();
    public static event OnCollectionLoad onCollectionLoad;
    
    [SerializeField] private RectTransform _categories, _filters;
    [SerializeField] private Color _clickColor = new Color(55, 55, 55, 255),
                                    _normalColor = new Color(128, 128, 128, 255);

    private void OnEnable() => LoadCollection();

    public void LoadCollection() => onCollectionLoad?.Invoke();

    public void SelectCategory(GameObject category)
    {
        foreach (RectTransform btn in _categories)
            btn.GetComponent<Image>().color = _normalColor;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = _clickColor;

        foreach (RectTransform filter in _filters)
            filter.gameObject.SetActive(false);
        category.SetActive(true);
        
        LoadCollection();
    }

    public void PurchaseItem()
    {
        //EventSystem.current.currentSelectedGameObject.GetComponent<ItemInfo>().Base; // the item you bought
        LoadCollection();
    }
}