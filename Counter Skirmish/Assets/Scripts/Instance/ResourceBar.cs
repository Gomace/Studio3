using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    [SerializeField] private InstanceUnit _unit;
    
    [Header("These should all be filled.")]
    [SerializeField] private Image _mainBar;
    [SerializeField] private Image _incBar, _lossBar;
    [SerializeField] private float _barSpeed = 1f;

    private Coroutine _changingBar;
    
    private void OnEnable() => _unit.onResourceChanged += SetBar;
    private void OnDisable() => _unit.onResourceChanged -= SetBar;

    private void SetBar(float healthNormalized)
    {
        if (_changingBar != null)
            StopCoroutine(_changingBar);

        _changingBar = StartCoroutine(SetHealthSmooth(healthNormalized));
    }

    private IEnumerator SetHealthSmooth(float newH)
    {
        float cur = _mainBar.fillAmount,
              chaAmt;
        
        if (newH < cur) // Damaged
        {
            chaAmt = cur - newH;
            _mainBar.fillAmount -= chaAmt;
            _incBar.fillAmount -= chaAmt;
            // Dead?

            while (cur - newH > Mathf.Epsilon)
            {
                cur -= chaAmt * Time.deltaTime * _barSpeed;
                _lossBar.fillAmount = cur;
                yield return null;
            }
        }
        else // Healed
        {
            chaAmt = newH - cur;
            while (newH - cur > Mathf.Epsilon)
            {
                cur += chaAmt * Time.deltaTime;
                _incBar.fillAmount = cur;
                yield return null;
            }
        }
    }
}