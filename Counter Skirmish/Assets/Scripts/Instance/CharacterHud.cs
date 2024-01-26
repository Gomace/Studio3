using System;
using TMPro;
using UnityEngine;

public class CharacterHud : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText, _levelText;
    [SerializeField] private HealthBar _healthBar;

    private Transform _mainCam;

    private void Start()
    {
        if (!Camera.main)
        {
            Debug.Log("No Main Camera in scene :(");
            return;
        }
        
        _mainCam = Camera.main.transform;
    }


    public void SetData(Creature creature)
    {
        _nameText.text = creature.Base.Name;
        _levelText.text = creature.Level.ToString();
        _healthBar.SetHealth((float) creature.Health / creature.MaxHealth);
    }

    private void LateUpdate() => transform.LookAt(transform.position + _mainCam.rotation * Vector3.forward);
}
