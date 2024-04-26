using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Elements
    [Header("These should already be referenced.")] // Card display elements
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _card, _type1, _type2, _role;
    [SerializeField] private TMP_Text _lvl;
    [SerializeField] private GameObject _hover;
    #endregion Elements
    
    private CreatureInfo _creature;
    
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
    public void OnPointerClick(PointerEventData eventData) // Quick equip
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            EquipCreature();
    }
    
    public void LoadInfo() // Put details on card
    {
        if (_creature == null)
        {
            gameObject.SetActive(false);
            return;
        }
        if (_creature.Base == null)
        {
            gameObject.SetActive(false);
            return;
        }
        
        _name.text = _creature.Base.Name;
        _card.sprite = _creature.Base.Card;
        
        _type1.sprite = _creature.Base.Type1.Icon;
        if (_creature.Base.Type2)
            _type2.sprite = _creature.Base.Type2.Icon;
        _type2.enabled = _creature.Base.Type2;
        
        _role.sprite = _creature.Base.Role.Icon;
        _lvl.text = $"Lvl. {_creature.Level}";
        
        gameObject.SetActive(true);
    }
    
    public void EquipCreature() => CardMenu.AddCreatureToRoster(this); // Adds Creature to open slot in Roster
    public void DetailsInfo() => CardMenu.DetailsScreen(_creature); // What Creature to show in DetailsMenu
    public void UpdateSelected() => CardMenu.CurSelected(_creature);
}
