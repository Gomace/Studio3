using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CreatureBase _cBase;
    
    [SerializeField] private TMP_Text _name;
    
    [SerializeField] private Image _card, _type1, _type2, _role;

    [SerializeField] private Color _normal = new Color(255, 255, 255, 255),
                                    _hover = new Color(150, 150, 150, 255) ;

    public CreatureBase Base => _cBase;

    private void OnEnable() => CollectionMenu.onCollectionLoad += LoadInfo;
    private void OnDisable() => CollectionMenu.onCollectionLoad -= LoadInfo;

    private void LoadInfo() // connect details to overview
    {
        if (!_cBase)
            return;
            
        _name.text = _cBase.Name;
        _card.sprite = _cBase.Card;
        _type1.sprite = _cBase.Type1.Icon;
        _type2.sprite = _cBase.Type2.Icon;
        _role.sprite = _cBase.Role.Icon;
        // find gold on player
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _card.color = _hover;
        //_reveal.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _card.color = _normal;
        //_reveal.SetActive(false);
    }
}
