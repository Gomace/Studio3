using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")] // Maybe add description pop up things
    [SerializeField] private Image[] _abilityUIs = new Image[4];
    [SerializeField] private Image _passive;
    
    private readonly Image[] _radials = new Image[4];
    private readonly TMP_Text[] _nums = new TMP_Text[4];
    private readonly Color _tempColor = new (0.7f, 0.7f, 0.7f, 1f);

    private Ability[] _abilities;

    private void Awake()
    {
        for (int i = 0; i < _abilityUIs.Length; ++i)
        {
            _radials[i] = _abilityUIs[i].transform.GetChild(0).GetComponent<Image>();
            _nums[i] = _radials[i].transform.GetChild(0).GetComponent<TMP_Text>();
        }
    }

    private void OnEnable()
    {
        _unit.onLoadHUD += SetAbilityUI;
        _unit.onActivateCooldown += AbilityCD;
    }
    private void OnDisable()
    {
        _unit.onLoadHUD -= SetAbilityUI;
        _unit.onActivateCooldown -= AbilityCD;
    }

    private void SetAbilityUI(Creature creature)
    {
        Debug.Log($"You have acquired {creature.Base.Name}");
        _abilities = creature.Abilities;

        for (int i = 0; i < _abilities.Length; ++i)
        {
            if (_abilities[i] == null)
                break;
            Debug.Log($"Is this null? {_abilities[i].Base.Icon != null}");
            if (_abilities[i].Base.Icon != null)
                _abilityUIs[i].sprite = _abilities[i].Base.Icon;
        }

        /*if (creature.Passive == null) // TODO idk it not work
            return;
        if (creature.Passive.Base.Icon != null)
            _passive.sprite = creature.Passive.Base.Icon;*/ 
    }

    private void AbilityCD(Ability ability)
    {
        for (int i = 0; i < _abilities.Length; ++i)
        {
            if (_abilities[i] == ability)
            {
                _abilityUIs[i].color = _tempColor;
                _radials[i].gameObject.SetActive(true);
                StartCoroutine(CooldownTimer(ability, i));
                return;
            }
        }
    }
    private IEnumerator CooldownTimer(Ability ability, int abiSlot)
    { 
        while (ability.Cooldown > 0 && _abilities[abiSlot] == ability)
        {
            ability.Cooldown -= Time.deltaTime;
            _radials[abiSlot].fillAmount = ability.Cooldown / ability.Base.Cooldown;
            
            _nums[abiSlot].text = ability.Cooldown > 1 ? ability.Cooldown.ToString("N0") : ability.Cooldown.ToString("F1", CultureInfo.InvariantCulture);
            
            yield return null;
        }
        
        _abilityUIs[abiSlot].color = Color.white;
        _radials[abiSlot].gameObject.SetActive(false);
        
        while (ability.Cooldown > 0)
        {
            ability.Cooldown -= Time.deltaTime;
            yield return null;
        }
    }
}