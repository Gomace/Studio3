using System;
using TMPro;
using UnityEngine;

public class CharacterHud : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText, _levelText;
    [SerializeField] private HealthBar _healthBar;

    private Camera _mainCam;

    private void Start() => _mainCam = Camera.main;
    
    public void SetData(Creature creature)
    {
        _nameText.text = creature.Base.Name;
        _levelText.text = creature.Level.ToString();
        _healthBar.SetHealth((float) creature.Health / creature.MaxHealth);
    }

    private void LateUpdate()
    {
        if (!_mainCam)
            return;
        
        //transform.look <- fix this
        transform.LookAt(_mainCam.transform);
        transform.Rotate(0f, 180f, 0f);
    }
}
