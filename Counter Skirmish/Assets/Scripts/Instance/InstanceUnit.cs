using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreatureRoster))]
public class InstanceUnit : MonoBehaviour
{
    #region Events
    public delegate void OnLoadHUD(Creature @creature);
    public event OnLoadHUD onLoadHUD;
    
    // Health
    public delegate void OnHealthChanged(float @normHealth);
    public event OnHealthChanged onHealthChanged;
    public delegate void OnResourceChanged(float @normRes);
    public event OnResourceChanged onResourceChanged;
    
    public delegate void OnActivateCooldown(int @slotNum);
    public event OnActivateCooldown onActivateCooldown;
    #endregion Events
    
    [SerializeField] private Transform _character;
    private List<Creature> _usedCreatures = new();
    private List<GameObject> _usedModels = new();

    private Creature _creature;

    public Dictionary<string, GameObject> Indicators = new();
    public Creature Creature => _creature;

    public void SetupUnit(Creature creature)
    {
        ChangeCharacter(creature);
        _creature = creature;

        onLoadHUD?.Invoke(_creature);
    }
    
    public void UpdateHealth() => onHealthChanged?.Invoke((float) _creature.Health / _creature.MaxHealth);
    public void UpdateResource() => onResourceChanged?.Invoke((float) _creature.Resource / _creature.MaxResource);

    public void ActivateCooldown(int slotNum) => onActivateCooldown?.Invoke(slotNum);

    public void PlayEnterAnim() => Debug.Log(_creature.Base.Name + " entered.");
    public void PlayAttackAnim() => Debug.Log(_creature.Base.Name + " is attacking.");
    public void PlayHitAnim() => Debug.Log(_creature.Base.Name + " was hit.");
    public void PlayFaintAnim() => Debug.Log(_creature.Base.Name + " fainted.");

    private void ChangeCharacter(Creature creature)
    {
        foreach (GameObject model in _usedModels)
                model.SetActive(false);

        if (_usedCreatures.Contains(creature)) // Check if creature's model already exists
        {
            int i = _usedCreatures.IndexOf(creature);
            
            _usedModels[i].transform.position = _character.position;
            _usedModels[i].transform.rotation = _character.rotation;
            _usedModels[i].SetActive(true); // If creature already exists, turn on model

            return;
        }
        
        _usedCreatures.Add(creature); // If creature not exist, add to list
        _usedModels.Add(Instantiate(creature.Base.Model, _character.position, _character.rotation, _character)); // Add new creature model to list
        foreach (Ability ability in creature.Abilities)
        {
            if (ability == null)
                return;
            
            if (!Indicators.ContainsKey(ability.Base.Indicator.name))
                Indicators.Add(ability.Base.Indicator.name, Instantiate(ability.Base.Indicator, transform));
        }
    }

    /*private IEnumerator AbilityCooldown()
    {
        
    }*/
}