using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RosterCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CardMenu _cardMenu;
    #region Elements
    [Header("These should already be referenced.")] // Slot display elements
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon, _type1, _type2, _role;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private RectTransform _hover;
    #endregion Elements
    
    // Creature this slot has equipped
    private CreatureBase _cBase;

    public CreatureBase CBase
    {
        get => _cBase;
        set
        {
            _cBase = value;
            LoadInfo();
        } 
    }

    // Add self to CollectionMenu loading pool
    private void OnEnable() => _cardMenu.onCardsLoad += LoadInfo;
    private void OnDisable() => _cardMenu.onCardsLoad -= LoadInfo;
    
    // Mouse-over-card stuff
    public void OnPointerEnter(PointerEventData eventData) => _hover.gameObject.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => _hover.gameObject.SetActive(false);
    public void OnPointerClick(PointerEventData eventData) // Quick Unequip
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            UnequipCreature();
    }
    
    private void LoadInfo() // Reveal details when card is equipped in slot
    {
        if (!_cBase)
        {
            foreach (RectTransform element in (RectTransform)transform) // Turn off all Slot elements
                element.gameObject.SetActive(false);
            return;
        }
        
        foreach (RectTransform element in (RectTransform)transform) // Turn all slot elements on to display stuff
            element.gameObject.SetActive(true);
        _hover.gameObject.SetActive(false); // Hover still not on without hovering
        
        _name.text = _cBase.Name;
        _icon.sprite = _cBase.Icon;
        _type1.sprite = _cBase.Type1.Icon;
        _type2.sprite = _cBase.Type2.Icon;
        _role.sprite = _cBase.Role.Icon;
    }

    public void UnequipCreature() // Removes creature from roster.
    {
        
        _cBase = null;
        LoadInfo();
    }
}
