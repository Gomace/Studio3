using System;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class InstanceUnit : MonoBehaviour
{
    public delegate void OnLoadHUD(Creature @creature);
    public event OnLoadHUD onLoadHUD;
    
    [Header("Only players get icons.")]
    [SerializeField] private bool isPlayerUnit;
    
    [Header("These should always be the same.")]
    [SerializeField] private CreatureBase _base; // Should not be drag & drop reference
    [SerializeField] private int _level; // - || -
    [SerializeField] private Object _model;
    
    private Creature _creature;

    public CreatureBase Base => _base;
    public Creature Creature => _creature;

    private void OnEnable() => InstanceSystem.onLoadInstance += SetupUnit;
    private void OnDisable() => InstanceSystem.onLoadInstance -= SetupUnit;

    private void SetupUnit()
    {
        _creature = new Creature(_base, _level); // add model somehow. Figure it out vvv icon there
        onLoadHUD?.Invoke(Creature);
        
        _creature.PlayEnterAnim();
    }
}