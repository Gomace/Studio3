using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CreatureRoster))]
public class InstanceUnit : MonoBehaviour
{
    #region Events
    public delegate void OnLoadHUD(Creature @creature); // Loading HUD
    public event OnLoadHUD onLoadHUD;
    public delegate void OnSpawn(); // Creature spawns, revives or swaps in
    public event OnSpawn onSpawn;
    public delegate void OnDead(); // Creature dies
    public event OnDead onDead;
    
    // Health & Resource
    public delegate void OnHealthChanged(float @normHealth);
    public event OnHealthChanged onHealthChanged;
    public delegate void OnResourceChanged(float @normRes);
    public event OnResourceChanged onResourceChanged;
    
    // Cooldown
    public delegate void OnActivateCooldown(Ability @ability);
    public event OnActivateCooldown onActivateCooldown;
    #endregion Events
    
    [SerializeField] private Transform _character;
    private List<Creature> _usedCreatures = new();
    private List<GameObject> _usedModels = new();

    public Dictionary<string, GameObject> Indicators = new();

    public CreatureRoster CreRoster { get; private set; }
    public Creature Creature { get; private set; }

    public void Awake() => CreRoster = GetComponent<CreatureRoster>();

    public void SetupUnit(Creature creature)
    {
        ChangeCharacter(creature);
        Creature = creature;
        gameObject.layer = 14; // Change layer to Unit
        
        onLoadHUD?.Invoke(Creature);
        onSpawn?.Invoke();
    }
    
    public void UpdateHealth() => onHealthChanged?.Invoke((float) Creature.Health / Creature.MaxHealth);
    public void UpdateResource() => onResourceChanged?.Invoke((float) Creature.Resource / Creature.MaxResource);

    public void ActivateCooldown(Ability ability)
    {
        StartCoroutine(CooldownTimer(ability));
        onActivateCooldown?.Invoke(ability);
    }

    public void CreatureDead(Creature creature) // TODO use this Creature to give exp :) (future)
    {
        gameObject.layer = 16; // Change layer to Dead
        onDead?.Invoke(); // TODO CreatureRoster NextCreature all dead stop moving for NPC
        CreRoster.NextCreature();
    }
    
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

    private IEnumerator CooldownTimer(Ability ability)
    {
        while (ability.Cooldown > 0)
        {
            ability.Cooldown -= Time.deltaTime;
            yield return null;
        }
    }
}