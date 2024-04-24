using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverviewLoader : MonoBehaviour
{
    [SerializeField] private DetailsMenu _detMenu;
    
    #region Elements
    [Header("These should already be referenced.")] // Overview display elements
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _splash, _type1, _type2, _role;
    [SerializeField] private TMP_Text _extraName, _title, _description, _lvl;
    
    [Header("Stats")]
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _resource, _physical, _magical, _defense, _resistance, _speed, _total;
    #endregion Elements
    
    // Stats
    private int _sHealth, _sResource, _sPhysical, _sMagical, _sDefense, _sResistance, _sSpeed, _sTotal;
    
    private void OnEnable() => _detMenu.onDetailsLoad += LoadDetails;
    private void OnDisable() => _detMenu.onDetailsLoad -= LoadDetails;
    
    private void LoadDetails(CreatureInfo creature)
    {
        if (creature == null)
            return;
        if (creature.Base == null)
            return;
            
        // Elements
        _name.text = _extraName.text = creature.Base.Name;
        _title.text = creature.Base.Title;
        _description.text = creature.Base.Description;
        
        _splash.sprite = creature.Base.Splash;
        
        _type1.sprite = creature.Base.Type1.Icon;
        if (creature.Base.Type2)
            _type2.sprite = creature.Base.Type2.Icon;
        _type2.enabled = creature.Base.Type2;
        
        _role.sprite = creature.Base.Role.Icon;
        _lvl.text = $"Lvl. {creature.Level}";
        
        CalculateStats(creature);
        
        // Stats
        _health.text = _sHealth.ToString();
        _resource.text = _sResource.ToString();
        
        _physical.text = _sPhysical.ToString();
        _magical.text = _sMagical.ToString();
        _defense.text = _sDefense.ToString();
        _resistance.text = _sResistance.ToString();
        _speed.text = _sSpeed.ToString();
        
        _total.text = _sTotal.ToString();
    }

    private void CalculateStats(CreatureInfo creature)
    {
        _sHealth = Mathf.FloorToInt((creature.Base.MaxHealth * creature.Level) / 100f) + 10;
        _sResource = Mathf.FloorToInt((creature.Base.MaxResource * 0.2f) + ((creature.Base.MaxResource * 0.8f) * creature.Level) / 100f) + 10;

        _sPhysical = Mathf.FloorToInt((creature.Base.Physical * creature.Level) / 100f) + 5;
        _sMagical = Mathf.FloorToInt((creature.Base.Magical * creature.Level) / 100f) + 5;
        _sDefense = Mathf.FloorToInt((creature.Base.Defense * creature.Level) / 100f) + 5;
        _sResistance = Mathf.FloorToInt((creature.Base.Resistance * creature.Level) / 100f) + 5;
        _sSpeed = Mathf.FloorToInt((creature.Base.Speed * creature.Level) / 100f) + 5;

        _sTotal = _sPhysical + _sMagical + _sDefense + _sResistance + _sSpeed;
    }
}
