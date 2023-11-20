using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CreatureBase _cBase;
    
    [Header("These should already be referenced.")]
    [SerializeField] private TMP_Text _name;
    
    [SerializeField] private Image _card, _type1, _type2, _role;

    [SerializeField] private Color _normalC = new Color(255, 255, 255, 255),
                                    _hoverC = new Color(150, 150, 150, 255);

    [SerializeField] private CardHoverBase _hover;

    public CreatureBase Base => _cBase;

    private void OnEnable() => CollectionMenu.onCollectionLoad += LoadInfo;
    private void OnDisable() => CollectionMenu.onCollectionLoad -= LoadInfo;

    private void LoadInfo() // connect details to overview
    {
        if (!_cBase)
            return; // Debug.Log("Missing CreatureBase in " + gameObject.name);
            
        _name.text = _cBase.Name;
        _card.sprite = _cBase.Card;
        _type1.sprite = _cBase.Type1.Icon;
        _type2.sprite = _cBase.Type2.Icon;
        _role.sprite = _cBase.Role.Icon;
        // find gold on player
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _card.color = _hoverC;
        //_hover.CardHover(true, (RectTransform)transform);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _card.color = _normalC;
        //_hover.CardHover(false, (RectTransform)transform);
    }
}
