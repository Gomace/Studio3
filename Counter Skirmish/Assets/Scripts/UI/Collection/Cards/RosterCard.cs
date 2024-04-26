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
    [SerializeField] private GameObject _hover;
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
    public void OnPointerEnter(PointerEventData eventData) => _hover.SetActive(true);
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
        _hover.SetActive(false); // Hover still not on without hovering
    }

    public void UnequipCreature() => CardMenu.RemoveCreatureFromRoster(_creature); // Removes creature from Roster
    public void DetailsInfo() => CardMenu.DetailsScreen(_creature); // What Creature to show in DetailsMenu
    public void UpdateSelected() => CardMenu.CurSelected(_creature);
}
