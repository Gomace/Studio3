using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CardMenu _cardMenu; // Card menu chief
    [SerializeField] private CreatureInfo _creature;
    
    #region Elements
    [Header("These should already be referenced.")] // Card display elements
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _card, _type1, _type2, _role;
    [SerializeField] private TMP_Text _lvl;
    [SerializeField] private GameObject _hover;
    #endregion Elements

    private void OnEnable() => _cardMenu.onCardsLoad += LoadInfo;
    private void OnDisable() => _cardMenu.onCardsLoad -= LoadInfo;

    // Mouse-over-card stuff
    public void OnPointerEnter(PointerEventData eventData) => _hover.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => _hover.SetActive(false);
    public void OnPointerClick(PointerEventData eventData) // Quick equip
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            EquipCreature();
    }
    
    private void LoadInfo() // Put details on card
    {
        if (_creature == null)
            return;
        if (_creature.Base == null)
            return;
            
        _name.text = _creature.Base.Name;
        _card.sprite = _creature.Base.Card;
        
        _type1.sprite = _creature.Base.Type1.Icon;
        if (_creature.Base.Type2)
            _type2.sprite = _creature.Base.Type2.Icon;
        _type2.enabled = _creature.Base.Type2;
        
        _role.sprite = _creature.Base.Role.Icon;
        _lvl.text = $"Lvl. {_creature.Level}";
    }
    
    public void EquipCreature() => _cardMenu.AddCreatureToRoster(_creature); // Adds Creature to open slot in Roster
    public void DetailsInfo() => _cardMenu.DetailsScreen(_creature); // What Creature to show in DetailsMenu
}
