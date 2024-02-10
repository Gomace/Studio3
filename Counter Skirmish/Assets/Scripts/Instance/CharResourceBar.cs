using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharResourceBar : MonoBehaviour
{
    [SerializeField] private CharacterHud _charHud;
    [SerializeField] private Image _mainBar, _incBar, _lossBar;

    private void OnEnable() => _charHud.onResourceChanged += SetBar;
    private void OnDisable() => _charHud.onResourceChanged -= SetBar;

    private void SetBar(float valueNormalized) => _mainBar.fillAmount = valueNormalized;

    private IEnumerator SetBarSmooth(float newFill)
    {
        float curFill = _mainBar.fillAmount, changeAmt = curFill - newFill;
        SetBar(newFill);

        while (curFill - newFill > Mathf.Epsilon)
        {
            curFill -= changeAmt * Time.deltaTime;
            _mainBar.fillAmount = curFill;
            yield return null;
        }
        _mainBar.fillAmount = newFill;
    }
}