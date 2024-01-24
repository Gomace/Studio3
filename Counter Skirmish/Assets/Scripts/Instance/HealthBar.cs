using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _health;

    public void SetHealth(float healthNormalized) => _health.fillAmount = healthNormalized;
}