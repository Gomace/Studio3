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
    
    [Header("Stats")]
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _phyiscal, _magical, _defense, _resistance, _speed, _total;
    #endregion Elements
    
    private void OnEnable() => _detMenu.onDetailsLoad += LoadDetails;
    private void OnDisable() => _detMenu.onDetailsLoad -= LoadDetails;
    
    private void LoadDetails(CreatureBase cBase)
    {
        if (!cBase)
            return;
        
        // Elements
        _name.text = cBase.Name;
        _splash.sprite = cBase.Splash;
        _type1.sprite = cBase.Type1.Icon;
        _type2.sprite = cBase.Type2.Icon;
        _role.sprite = cBase.Role.Icon;
        
        // Stats
        _health.text = cBase.MaxHealth.ToString();
        _phyiscal.text = cBase.Physical.ToString();
        _magical.text = cBase.Magical.ToString();
        _defense.text = cBase.Defense.ToString();
        _resistance.text = cBase.Resistance.ToString();
        _speed.text = cBase.Speed.ToString();
        _total.text = (cBase.Physical + cBase.Magical + cBase.Defense + cBase.Resistance + cBase.Speed).ToString();
    }
}
