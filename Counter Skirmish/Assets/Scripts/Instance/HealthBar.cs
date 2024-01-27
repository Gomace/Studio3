using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _health;

    public void SetHealth(float healthNormalized) => _health.fillAmount = healthNormalized;

    public IEnumerator SetHealthSmooth(float newHealth)
    {
        float curHealth = _health.fillAmount;
        float changeAmt = curHealth - newHealth;

        while (curHealth - newHealth > Mathf.Epsilon)
        {
            curHealth -= changeAmt * Time.deltaTime;
            _health.fillAmount = curHealth;
            yield return null;
        }
        _health.fillAmount = newHealth;
    }
}