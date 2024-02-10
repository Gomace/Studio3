using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHud : MonoBehaviour
{
    public delegate void OnHealthChanged(float @normHealth);
    public event OnHealthChanged onHealthChanged;
    public delegate void OnResourceChanged(float @normRes);
    public event OnResourceChanged onResourceChanged;
    
    [SerializeField] private InstanceUnit _player;
    [SerializeField] private TMP_Text _nameText, _levelText;
    [SerializeField] private Image _type1, _type2;

    private Creature _creature;
    private Transform _mainCam;

    private void Awake()
    {
        if (!Camera.main)
        {
            Debug.Log("No Main Camera in scene :(");
            return;
        }
        
        _mainCam = Camera.main.transform;
    }
    
    private void OnEnable() => _player.onLoadHUD += SetupHUD;
    private void OnDisable() => _player.onLoadHUD -= SetupHUD;

    public void SetupHUD(Creature creature)
    {
        _creature = creature;
        
        _nameText.text = creature.Base.Name;
        _levelText.text = creature.Level.ToString();
        _type1.sprite = creature.Base.Type1.Icon;

        if (creature.Base.Type2)
            _type2.sprite = creature.Base.Type2.Icon;
        else
            _type2.enabled = false;

        onHealthChanged?.Invoke((float) creature.Health / creature.MaxHealth);
        onResourceChanged?.Invoke((float) creature.Resource / creature.MaxResource);
    }

    public void UpdateHealth() => onHealthChanged?.Invoke((float) _creature.Health / _creature.MaxHealth);
    public void UpdateResource() => onResourceChanged?.Invoke((float) _creature.Resource / _creature.MaxResource);
    private void LateUpdate() => transform.LookAt(transform.position + _mainCam.rotation * Vector3.forward);
}