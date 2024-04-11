using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class StarterCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private StarterCreature _starterCreature;
    [SerializeField] private CreatureInfo _creature;
    
    #region Elements
    [Header("These should already be referenced.")] // Slot display elements
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _card, _type1, _type2, _role;
    [SerializeField] private TMP_Text _lvl;
    [SerializeField] private GameObject _hover;
    #endregion Elements

    private void Awake() => LoadInfo();

    public void OnPointerEnter(PointerEventData eventData) => _hover.gameObject.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => _hover.gameObject.SetActive(false);
    
    private void LoadInfo()
    {
        _name.text = _creature.Base.Name;
        _card.sprite = _creature.Base.Card;
        _type1.sprite = _creature.Base.Type1.Icon;
        _type2.sprite = _creature.Base.Type2.Icon;
        _role.sprite = _creature.Base.Role.Icon;
        _lvl.text = $"Lvl. {_creature.Level}";
    }
}
