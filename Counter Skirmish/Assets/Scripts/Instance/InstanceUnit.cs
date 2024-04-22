using System.Collections.Generic;
using System.Collections;
using System.Linq;
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
    public delegate void OnHealthChanged(float @normHealth); // Health changes
    public event OnHealthChanged onHealthChanged;
    public delegate void OnResourceChanged(float @normRes); // Resource changes
    public event OnResourceChanged onResourceChanged;
    
    // Exp & Level Up
    public delegate void OnExpEarned(bool @lvled); // Update Experience bar
    public event OnExpEarned onExpEarned;
    public delegate void OnLvlUp(); // Lvled up
    public event OnLvlUp onLvlUp;
    
    // Abilities & Cooldown
    public delegate void OnLearnAbility(Creature @creature); // Reloading Abilities if learned
    public event OnLearnAbility onLearnAbility;
    public delegate void OnActivateCooldown(Ability @ability); // Ability going on CD
    public event OnActivateCooldown onActivateCooldown;
    #endregion Events
    
    public Transform Character { get; private set; }
    
    private List<Creature> _usedCreatures = new();
    private List<GameObject> _usedModels = new();

    public Dictionary<string, GameObject> Indicators = new();

    public CreatureRoster CreRoster { get; private set; }
    public Creature Creature { get; private set; }

    public void Awake()
    {
        Character = transform.Find("Character");
        
        CreRoster = GetComponent<CreatureRoster>();
    }

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
    
    public void UpdateExp(bool lvled) => onExpEarned?.Invoke(lvled); // Move exp bar before lvling
    public void LvlUp(bool lvled)
    {
        onLvlUp?.Invoke();
        TryToLearnAbility();
        UpdateExp(lvled);
    }
    
    public void ActivateCooldown(Ability ability)
    {
        StartCoroutine(CooldownTimer(ability));
        onActivateCooldown?.Invoke(ability);
    }

    public void CreatureDead(Creature attacker)
    {
        gameObject.layer = 16; // Change layer to Dead
        if (attacker.Unit.CompareTag("Player"))
            GiveExp(attacker); // Give XP to player attacker
        onDead?.Invoke();
        CreRoster.NextCreature();
    }
    
    private void ChangeCharacter(Creature creature)
    {
        foreach (GameObject model in _usedModels)
                model.SetActive(false);

        if (_usedCreatures.Contains(creature)) // Check if creature's model already exists
        {
            int i = _usedCreatures.IndexOf(creature);
            
            _usedModels[i].transform.position = Character.position;
            _usedModels[i].transform.rotation = Character.rotation;
            _usedModels[i].SetActive(true); // If creature already exists, turn on model

            return;
        }
        
        _usedCreatures.Add(creature); // If creature not exist, add to list
        _usedModels.Add(Instantiate(creature.Base.Model, Character.position, Character.rotation, Character)); // Add new creature model to list
        if (creature.Abilities != null)
        {
            foreach (Ability ability in creature.Abilities)
            {
                if (ability == null)
                    return;

                if (!Indicators.ContainsKey(ability.Base.Indicator.name))
                    Indicators.Add(ability.Base.Indicator.name, Instantiate(ability.Base.Indicator, transform));
            }
        }
    }
    
    private void GiveExp(Creature attacker)
    {
        int expYield = Creature.Base.ExpYield,
            defeatedLvl = Creature.Level;
        int expGain = Mathf.FloorToInt((expYield * defeatedLvl) / 7);
        
        attacker.Exp += expGain; // Update Creature Exp
        attacker.Unit.UpdateExp(attacker.CheckForLvlUp()); // Update Exp UI and send if lvled = true
        // TODO floating purple text number when exp gained
    }

    private void TryToLearnAbility()
    {
        LearnableAbility newAbility = Creature.GetLearnableAbilityAtCurLvl();
        
        if (newAbility == null)
            return;
        
        int abilities = Creature.Abilities.Count(ability => ability != null);

        if (abilities < Creature.Abilities.Length)
        {
            Creature.LearnAbility(newAbility);
            onLearnAbility?.Invoke(Creature);
        }
        else
            return; // TODO "You've learned a new ability! Equip it in the "Abilities" section of your creature's Details summary."
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