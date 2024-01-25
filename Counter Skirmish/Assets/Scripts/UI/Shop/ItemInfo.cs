using UnityEngine;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private ItemBase _iBase;
    [SerializeField] private TMP_Text _name, _description, _price;
    [SerializeField] private Sprite _icon;

    [SerializeField] private Color _redPrice = new Color(245, 31, 31, 255),
                                    _yelPrice = new Color(255, 218, 0, 255);
    
    private int gold = 100; //temporary
    
    public ItemBase Base => _iBase;

    private void OnEnable() => ItemShop.onShopLoad += LoadInfo;
    private void OnDisable() => ItemShop.onShopLoad -= LoadInfo;

    private void LoadInfo()
    {
        //_icon = _iBase.Icon;
        _name.text = _iBase.Name;
        _description.text = _iBase.Description;
        _price.text = _iBase.Price.ToString();
        
        _price.color = gold >= _iBase.Price ? _yelPrice : _redPrice; // find gold on player
    }
}