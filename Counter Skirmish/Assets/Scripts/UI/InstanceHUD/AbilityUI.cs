using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")] // Maybe add description pop up things
    [SerializeField] private Image[] _abilityUIs;
    [SerializeField] private Image _passive;

    private Ability[] _abilities;

    private void OnEnable() => _unit.onLoadHUD += SetAbilityUI;
    private void OnDisable() => _unit.onLoadHUD -= SetAbilityUI;

    private void SetAbilityUI(Creature creature)
    {
        _abilities = creature.Abilities;

        for (int i = 0; i < _abilities.Length; ++i)
            _abilityUIs[i].sprite = _abilities[i].Base.Icon;

        _passive.sprite = creature.Passive.Base.Icon;
    }
}