using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RosterSlot : MonoBehaviour
{
    [Header("These should already be filled.")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _healthBar, _resBar, _icon, _type1, _type2, _role;
    [SerializeField] private TMP_Text _level, _healthNum, _resNum;

    private Creature _creature; // Send this to CreatureRoster

    public Creature Creature
    {
        get => _creature;
        set => SetRosterUI(value);
    }
    
    private void SetRosterUI(Creature creature)
    {
        _creature = creature;
        
        _name.text = creature.Base.Name;
        _icon.sprite = creature.Base.Icon;
        _type1.sprite = creature.Base.Type1.Icon;
        _type2.sprite = creature.Base.Type2.Icon;
        _role.sprite = creature.Base.Role.Icon;
        
        SetLevel();
        SetBars();
    }

    public void SetLevel()
    {
        if (_creature != null)
            _level.text = _creature.Level.ToString();
    }

    public void SetBars()
    {
        if (_creature != null)
        {
            _healthBar.fillAmount = ((float)_creature.Health / _creature.MaxHealth);
            _healthNum.text = $"{_creature.Health.ToString()}/{_creature.MaxHealth.ToString()}";
            
            _resBar.fillAmount = ((float)_creature.Resource / _creature.MaxResource);
            _resNum.text = $"{_creature.Resource.ToString()}/{_creature.MaxResource.ToString()}";
        }
    }
}
