using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")]
    [SerializeField] private Image _mainBar;
    [SerializeField] private Image _incBar, _lossBar;
    [Header("Ignore this on UnitHUD.")]
    [SerializeField] private TMP_Text _num;

    private Creature _creature;
    
    private float _barSpeed = 0.5f;
    private Coroutine _damageBar;

    private void Awake()
    {
        _mainBar.fillAmount = 1f;
        _incBar.fillAmount = 1f;
        _lossBar.fillAmount = 1f;
    }

    private void OnEnable()
    {
        _unit.onLoadHUD += SetHealth;
        _unit.onHealthChanged += SetBar;
    }
    private void OnDisable()
    {
        _unit.onLoadHUD -= SetHealth;
        _unit.onHealthChanged -= SetBar;
    }

    private void SetHealth(Creature creature)
    {
        if (_damageBar != null)
            StopCoroutine(_damageBar);

        _mainBar.fillAmount = _incBar.fillAmount = _lossBar.fillAmount = (float)creature.Health / creature.MaxHealth;
        _creature = creature;
    }
    
    private void SetBar(float newH)
    {
        if (newH < _mainBar.fillAmount) // Damaged
        {
            _incBar.fillAmount -= _mainBar.fillAmount - newH; // _incBar has same incoming Health
            // Dead?
            
            if (_damageBar != null)
                StopCoroutine(_damageBar);
    
            _damageBar = StartCoroutine(DamageBar(newH));
        }
        else // Healed
        {
            if (_lossBar.fillAmount < newH)
            {
                if (_damageBar != null)
                    StopCoroutine(_damageBar);
                _lossBar.fillAmount = newH;
            }
            
            if (_incBar.fillAmount < newH)
                _incBar.fillAmount = newH; // _incBar did its job
        }
        
        _mainBar.fillAmount = newH; // Change _mainBar to new Health
        if (_num && _creature != null)
            _num.text = $"{_creature.Health.ToString()}/{_creature.MaxHealth.ToString()}";
    }
    
    private IEnumerator DamageBar(float newH)
    {
        while (newH < _lossBar.fillAmount) // While _lossBar can shrink, go agane
        {
            _lossBar.fillAmount -= Time.deltaTime * _barSpeed; // _lossBar shrinks to _mainBar size
            yield return null;
        }
    }
}