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

    /*private void OnEnable() => _unit.onLoadHUD += SetAbilityUI;
    private void OnDisable() => _unit.onLoadHUD -= SetAbilityUI;

    private void SetAbilityUI(Creature creature)
    {
        Debug.Log($"You have acquired {creature.Base.Name}");
        _abilities = creature.Abilities;

        for (int i = 0; i < _abilities.Length; ++i)
        {
            Debug.Log($"{_abilities[i].Base.Icon} is this null? {_abilities[i].Base.Icon != null}");
            if (_abilities[i].Base.Icon != null)
                _abilityUIs[i].sprite = _abilities[i].Base.Icon;
        }

        if (creature.Passive.Base.Icon != null)
            _passive.sprite = creature.Passive.Base.Icon;
    }*/
}