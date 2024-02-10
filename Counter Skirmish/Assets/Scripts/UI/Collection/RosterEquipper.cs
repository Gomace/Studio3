using Mono.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RosterEquipper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CollectionMenu _collMenu;
    #region Elements
    [Header("These should already be referenced.")] // Slot display elements
    [SerializeField] private TMP_Text _name;

    [SerializeField] private Image _healthBar, _icon, _type1, _type2, _role;
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
    private void OnEnable() => _collMenu.onCollectionLoad += LoadInfo;
    private void OnDisable() => _collMenu.onCollectionLoad -= LoadInfo;
    
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
        if (!CBase)
        {
            foreach (RectTransform element in (RectTransform)transform) // Turn off all Slot elements
                element.gameObject.SetActive(false);
            return;
        }
        
        foreach (RectTransform element in (RectTransform)transform) // Turn all slot elements on to display stuff
            element.gameObject.SetActive(true);
        _hover.gameObject.SetActive(false); // Hover still not on without hovering
        
        _name.text = CBase.Name;
        _icon.sprite = CBase.Icon;
        _type1.sprite = CBase.Type1.Icon;
        _type2.sprite = CBase.Type2.Icon;
        _role.sprite = CBase.Role.Icon;
    }

    public void UnequipCreature() // Removes creature from roster.
    {
        CBase = null;
        
        LoadInfo();
    }
}
