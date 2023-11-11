using System;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using UnityEngine.WSA;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private ItemBase _iBase;
    [SerializeField] private TMP_Text _name, _description, _price;

    [SerializeField] private Color _redPrice = new Color(245, 31, 31, 255),
                                    _yelPrice = new Color(255, 218, 0, 255);
    
    public ItemBase Base => _iBase;

    private int gold = 500;
    private Sprite icon;

    private void Awake()
    {
        if (GetComponent<Image>())
            icon = GetComponent<Image>().sprite;
    }

    private void OnEnable() => ItemShop.onShopLoad += LoadInfo;
    private void OnDisable() => ItemShop.onShopLoad -= LoadInfo;

    private void LoadInfo()
    {
        _name.text = _iBase.Name;
        _description.text = _iBase.Description;
        _price.text = _iBase.Price.ToString();
        
        _price.color = gold >= _iBase.Price ? _yelPrice : _redPrice; // find gold on player
    }
}
