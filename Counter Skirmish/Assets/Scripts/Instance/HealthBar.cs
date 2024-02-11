using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")]
    [SerializeField] private Image _mainBar;
    [SerializeField] private Image _incBar, _lossBar;

    private float _barSpeed = 2f;
    private Coroutine _damageBar;

    private void Awake()
    {
        _mainBar.fillAmount = 1f;
        _incBar.fillAmount = 1f;
        _lossBar.fillAmount = 1f;
    }

    private void OnEnable() => _unit.onHealthChanged += SetBar;
    private void OnDisable() => _unit.onHealthChanged -= SetBar;

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