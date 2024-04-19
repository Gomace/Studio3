using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureUI : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon, _type1, _type2, _role;
    [SerializeField] private TMP_Text _lvl;

    private Creature _creature;
    
    private void OnEnable()
    {
        _unit.onLoadHUD += SetCreatureUI;
        _unit.onLvlUp += SetLvl;
    }
    private void OnDisable()
    {
        _unit.onLoadHUD -= SetCreatureUI;
        _unit.onLvlUp -= SetLvl;
    }

    private void SetCreatureUI(Creature creature)
    {
        _creature = creature;
        
        _name.text = creature.Base.Name;
        _icon.sprite = creature.Base.Icon;
        
        _type1.sprite = creature.Base.Type1.Icon;
        if (creature.Base.Type2)
            _type2.sprite = creature.Base.Type2.Icon;
        _type2.enabled = creature.Base.Type2;
        
        _role.sprite = creature.Base.Role.Icon;
        
        SetLvl();
    }

    private void SetLvl()
    {
        if (_creature != null)
            _lvl.text = _creature.Level.ToString();
    }
}