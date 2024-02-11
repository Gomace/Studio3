using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureUI : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon, _type1, _type2, _role;
    [SerializeField] private TMP_Text _level;

    private void OnEnable() => _unit.onLoadHUD += SetCreatureUI;
    private void OnDisable() => _unit.onLoadHUD -= SetCreatureUI;

    private void SetCreatureUI(Creature creature)
    {
        _name.text = creature.Base.Name;
        _icon.sprite = creature.Base.Icon;
        _type1.sprite = creature.Base.Type1.Icon;
        _type2.sprite = creature.Base.Type2.Icon;
        _role.sprite = creature.Base.Role.Icon;
        _level.text = creature.Level.ToString();
    }
}