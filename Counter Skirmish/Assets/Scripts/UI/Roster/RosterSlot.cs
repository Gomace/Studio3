using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RosterSlot : MonoBehaviour
{
    [Header("These should already be filled.")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _healthBar, _resBar, _icon, _type1, _type2, _role;
    [SerializeField] private TMP_Text _level, _healthNum, _resNum;
    [SerializeField] private GameObject _dead;

    private Creature _creature;
    
    public Creature Creature
    {
        get => _creature; // This is sent to CreatureRoster
        set => SetRosterUI(value);
    }
    
    public GameObject Dead => _dead;
    
    private void SetRosterUI(Creature creature)
    {
        _creature = creature;

        if (creature == null)
            return;

        if (creature.Base == null)
            return;
        
        _name.text = creature.Base.Name;
        _icon.sprite = creature.Base.Icon;
        
        _type1.sprite = creature.Base.Type1.Icon;
        if (creature.Base.Type2)
            _type2.sprite = creature.Base.Type2.Icon;
        _type2.enabled = creature.Base.Type2;
        
        _role.sprite = creature.Base.Role.Icon;
        _dead.SetActive(false);
        
        SetLevel();
        SetHealth((float)_creature.Health / _creature.MaxHealth);
        SetResource((float)_creature.Resource / _creature.MaxResource);
    }

    public void SetLevel()
    {
        if (_creature != null)
            _level.text = _creature.Level.ToString();
    }

    public void SetHealth(float newH)
    {
        if (_creature == null)
            return;
        
        _healthBar.fillAmount = newH;
        _healthNum.text = $"{_creature.Health.ToString()}/{_creature.MaxHealth.ToString()}";
    }
    public void SetResource(float newR)
    {
        if (_creature == null)
            return;
        
        _resBar.fillAmount = newR;
        _resNum.text = $"{_creature.Resource.ToString()}/{_creature.MaxResource.ToString()}";
    }
}
