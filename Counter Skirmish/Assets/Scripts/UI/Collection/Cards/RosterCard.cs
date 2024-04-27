using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RosterCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Elements
    [Header("These should already be referenced.")] // Slot display elements
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon, _type1, _type2, _role;
    [SerializeField] private TMP_Text _lvl;
    [SerializeField] private GameObject _rental, _hover;
    #endregion Elements
    
    private CreatureInfo _creature; // Creature this slot has equipped

    public CardMenu CardMenu { get; set; }
    public CreatureInfo Creature
    {
        get => _creature;
        set
        {
            _creature = value;
            LoadInfo();
        } 
    }
    
    // Mouse-over-card stuff
    public void OnPointerEnter(PointerEventData eventData) => _hover.SetActive(_creature != null);
    public void OnPointerExit(PointerEventData eventData) => _hover.SetActive(false);
    public void OnPointerClick(PointerEventData eventData) // Quick Unequip
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            UnequipCreature();
    }
    
    public void LoadInfo() // Reveal details when card is equipped in slot
    {
        if (_creature == null)
        {
            foreach (RectTransform element in (RectTransform)transform) // Turn off all Slot elements
                element.gameObject.SetActive(false);
            return;
        }
        
        if (_creature.Base == null)
        {
            foreach (RectTransform element in (RectTransform)transform) // Turn off all Slot elements
                element.gameObject.SetActive(false);
            return;
        }

        _name.text = _creature.Base.Name;
        _icon.sprite = _creature.Base.Icon;
        
        _type1.sprite = _creature.Base.Type1.Icon;
        if (_creature.Base.Type2)
            _type2.sprite = _creature.Base.Type2.Icon;
        _type2.enabled = _creature.Base.Type2;
        
        _role.sprite = _creature.Base.Role.Icon;
        _lvl.text = $"Lvl. {_creature.Level}";
        
        
        foreach (RectTransform element in (RectTransform)transform) // Turn all slot elements on to display stuff
            element.gameObject.SetActive(true);
        
        _rental.SetActive(_creature.Rental);
        _hover.SetActive(false); // Hover still not on without hovering
    }

    public void UnequipCreature() // Removes creature from Roster
    {
        if (_creature == null)
            return;
        if (_creature.Base == null)
            return;
        
        _hover.SetActive(false);
        CardMenu.RemoveCreatureFromRoster(_creature);
    }
    public void DetailsInfo() // What Creature to show in DetailsMenu
    {
        if (_creature == null)
            return;
        if (_creature.Base == null)
            return;
        
        CardMenu.DetailsScreen(_creature);
    }
    public void UpdateSelected()
    {
        if (_creature == null)
            return;
        if (_creature.Base == null)
            return;
        
        CardMenu.CurSelected(_creature);
    }
}
