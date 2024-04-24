using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class EquippedAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Elements
    [Header("This should already be referenced.")]
    [SerializeField] private GameObject _hover;
    #endregion Elements

    private Image _icon;
    
    private AbilityBase _aBase;
    
    public AbilitiesLoader AbiLoader { get; set; }
    public AbilityBase ABase
    {
        get => _aBase;
        set
        {
            _aBase = value;
            _icon.sprite = value == null ? null : value.Icon;
        }
    }
    
    private void Awake() => _icon = GetComponent<Image>();

    // Mouse-over-ability stuff
    public void OnPointerEnter(PointerEventData eventData) => _hover.SetActive(true);
    public void OnPointerExit(PointerEventData eventData) => _hover.SetActive(false);
    public void OnPointerClick(PointerEventData eventData) // Quick unequip
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            UnequipAbility();
    }

    public void UnequipAbility() => AbiLoader.UnequipAbility(this); // Remove ability from actives
    public void UpdateSelected() => AbiLoader.CurSelected(_aBase);
}
