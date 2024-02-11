using UnityEngine;
using Object = UnityEngine.Object;

public class InstanceUnit : MonoBehaviour
{
    #region Events
    public delegate void OnLoadHUD(Creature @creature);
    public event OnLoadHUD onLoadHUD;
    
    public delegate void OnHealthChanged(float @normHealth);
    public event OnHealthChanged onHealthChanged;
    public delegate void OnResourceChanged(float @normRes);
    public event OnResourceChanged onResourceChanged;
    #endregion Events
    
    [SerializeField] private bool isPlayerUnit;
    
    [Header("These should always be the same.")]
    [SerializeField] private CreatureBase _base; // Should not be drag & drop reference
    [SerializeField] private int _level; // - || -
    [SerializeField] private Object _model;
    
    private Creature _creature;

    private void OnEnable() => InstanceSystem.onLoadInstance += SetupUnit;
    private void OnDisable() => InstanceSystem.onLoadInstance -= SetupUnit;

    private void SetupUnit()
    {
        _creature = new Creature(_base, _level); // add model somehow. Figure it out vvv icon there
        onLoadHUD?.Invoke(_creature);
        
        onHealthChanged?.Invoke((float) _creature.Health / _creature.MaxHealth);
        onResourceChanged?.Invoke((float) _creature.Resource / _creature.MaxResource);
        
        PlayEnterAnim();
    }
    
    public void UpdateHealth() => onHealthChanged?.Invoke((float) _creature.Health / _creature.MaxHealth);
    public void UpdateResource() => onResourceChanged?.Invoke((float) _creature.Resource / _creature.MaxResource);
    
    public void PlayEnterAnim()
    {
    }
    public void PlayAttackAnim()
    {
    }
    public void PlayHitAnim() => Debug.Log(_creature.Base.Name + " hit.");
    public void PlayFaintAnim() => Debug.Log(_creature.Base.Name + " fainted.");
}