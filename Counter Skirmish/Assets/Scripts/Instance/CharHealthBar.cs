using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharHealthBar : MonoBehaviour
{
    [SerializeField] private CharacterHud _charHud;
    [SerializeField] private Image _mainBar, _incBar, _lossBar;

    private Coroutine _changingBar;

    private void OnEnable() => _charHud.onHealthChanged += SetBar;
    private void OnDisable() => _charHud.onHealthChanged -= SetBar;

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
            while (cur - newH > Mathf.Epsilon)
            {
                cur -= chaAmt * Time.deltaTime;
                _mainBar.fillAmount = cur;
                yield return null;
            }
        }
        else // Healed
        {
            chaAmt = newH - cur;
            while (newH - cur > Mathf.Epsilon)
            {
                cur += chaAmt * Time.deltaTime;
                _mainBar.fillAmount = cur;
                yield return null;
            }
        }

        _mainBar.fillAmount = newH;
    }
}