using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform health;

    public void SetHealth(float healthNormalized) => health.localScale = new Vector3(healthNormalized, 1f);
}