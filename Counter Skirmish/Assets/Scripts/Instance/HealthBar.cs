using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")]
    [SerializeField] private Image _mainBar;
    [SerializeField] private Image _incBar, _lossBar;
    
    private float _barSpeed = 2f, _curInc, _curLoss;
    private Coroutine _damageBar;

    private void OnEnable() => _unit.onHealthChanged += SetBar;
    private void OnDisable() => _unit.onHealthChanged -= SetBar;

    private void SetBar(float newH)
    {
        float oldH = _mainBar.fillAmount, // Save previous _mainBar value
            chaAmt = Mathf.Abs(oldH - newH); // Get Health difference

        if (newH < oldH)
        {
            _mainBar.fillAmount = newH; // Change _mainBar to new Health
            _incBar.fillAmount = _mainBar.fillAmount + _curInc; // _incBar has same incoming Health
            _curLoss += chaAmt; // Add lost Health to _lossBar
            // Dead?
            
            if (_damageBar != null)
                StopCoroutine(_damageBar);
    
            _damageBar = StartCoroutine(DamageBar());
        }
        else
            HealBar(chaAmt);
    }

    private void HealBar(float chaAmt)
    {
        _incBar.fillAmount += chaAmt;
        _incBar.fillAmount = _mainBar.fillAmount + _curInc; // _incBar has same incoming Health
    }
    
    private IEnumerator DamageBar()
    {
        while (_curLoss > Mathf.Epsilon) // While _lossBar can shrink, keep going
        {
            _lossBar.fillAmount -= _curLoss * (Time.deltaTime * _barSpeed); // _lossBar shrinks to _mainBar size
            yield return null;
        }
    }
}