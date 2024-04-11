using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private CreatureBase _cBase; // Creature this card is about
    [SerializeField] private CardMenu _cardMenu; // Card menu chief

    #region Elements
    [Header("These should already be referenced.")] // Card display elements
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _card, _type1, _type2, _role;
    [SerializeField] private RectTransform _hover;
    #endregion Elements

    private void OnEnable() => _cardMenu.onCardsLoad += LoadInfo;
    private void OnDisable() => _cardMenu.onCardsLoad -= LoadInfo;

    // Mouse-over-card stuff
    public void OnPointerEnter(PointerEventData eventData) => _hover.gameObject.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => _hover.gameObject.SetActive(false);
    public void OnPointerClick(PointerEventData eventData) // Quick equip
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            EquipCreature();
    }
    
    private void LoadInfo() // Put details on card
    {
        if (!_cBase)
        {
            Debug.Log("Missing CreatureBase in " + gameObject.name);
            return;
        }
            
        _name.text = _cBase.Name;
        _card.sprite = _cBase.Card;
        _type1.sprite = _cBase.Type1.Icon;
        _type2.sprite = _cBase.Type2.Icon;
        _role.sprite = _cBase.Role.Icon;
    }

    public void DetailsInfo() => _cardMenu.DetailsScreen(_cBase); // What Creature to show in DetailsMenu
    public void EquipCreature() => _cardMenu.AddCreatureToRoster(_cBase);// Adds Creature to open slot in Roster.
}
