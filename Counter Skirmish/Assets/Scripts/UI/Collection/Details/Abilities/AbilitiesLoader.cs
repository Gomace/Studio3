using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class AbilitiesLoader : MonoBehaviour
{
    [SerializeField] private DetailsMenu _detMenu;
    
    #region Elements
    [Header("These should already be referenced.")]
    [SerializeField] private GameObject _entryPrefab;
    [SerializeField] private Transform _entryContainer;
    [SerializeField] private TMP_Text _name, _description;
    [SerializeField] private EquippedAbility[] _equipped = new EquippedAbility[4];
    #endregion Elements

    private AbilityEntry[] _entries;
    private CreatureInfo _creature;

    private void Start()
    {
        foreach (EquippedAbility equipped in _equipped)
            equipped.AbiLoader = this;
    }

    private void OnEnable() => _detMenu.onDetailsLoad += LoadEntries;
    private void OnDisable() => _detMenu.onDetailsLoad -= LoadEntries;
    
    private void LoadEntries(CreatureInfo creature)
    {
        _creature = creature;
        
        foreach (Transform child in _entryContainer) // Empty the entries
            Destroy(child.gameObject);

        foreach (EquippedAbility equipped in _equipped) // Empty the equipped
            equipped.ABase = null;
        
        if (creature == null) // Check if creature exists
            return;
        if (creature.Base == null) // Check if Base exists
            return;

        // Equipped
        int used = 0;

        foreach (AbilityBase aBase in creature.AbilityBases) // Check if Abilities are equipped in Creature
        {
            if (aBase == null)
                continue;
            Debug.Log($"Equipping {used}");
            _equipped[used++].ABase = aBase; // Put abilities in equipped-slots
        }

        if (creature.AbilityBases.Length < 4) // Make sure length of array is 4 - for bugs
        {
            creature.AbilityBases = new AbilityBase[4];
            for (int i = 0; i < _equipped.Length; ++i)
            {
                if (_equipped[i].ABase == null)
                    continue;
                
                creature.AbilityBases[i] = _equipped[i].ABase;
            }
        }

        // Ability Collection Entries
        if (creature.LearnedAbilities?.Length > 0) // If have collection of learned
        {
            int length = creature.LearnedAbilities.Count(ability => ability != null), // Length of non-empty LearnedAbilities
                available = 0;

            Debug.Log("Using existing");
            _entries = new AbilityEntry[length + 3];
            for (int i = 0; i < _entries.Length; ++i) // Create empty prefabs for all non-empty LearnedAbilities
            {
                _entries[i] = Instantiate(_entryPrefab.GetComponent<AbilityEntry>(), _entryContainer);
                _entries[i].AbiLoader = this;
            }
            for (int i = length; i < _entries.Length; ++i) // Turn off unused slots
                _entries[i].gameObject.SetActive(false);

            foreach (AbilityBase learned in creature.LearnedAbilities) // Put available abilities in slots.
            {
                if (learned == null)
                    continue;

                _entries[available++].ABase = learned;
            }
        }
        else if (4 == creature.AbilityBases.Count(ability => ability != null)) // If no collection of learned
        {
            int length = creature.Base.LearnableAbilities.Where(learnable => learnable != null).Count(learnable => learnable.Base != null), // Length of non-empty LearnableAbilities
                available = 0;

            Debug.Log("Creating new");
            _entries = new AbilityEntry[length + 3];
            for (int i = 0; i < _entries.Length; ++i) // Create empty prefabs for all non-empty LearnableAbilities
            {
                _entries[i] = Instantiate(_entryPrefab.GetComponent<AbilityEntry>(), _entryContainer);
                _entries[i].AbiLoader = this;
            }
            for (int i = length; i < _entries.Length; ++i) // Turn off unused slots
                _entries[i].gameObject.SetActive(false);

            foreach (LearnableAbility learnable in creature.Base.LearnableAbilities) // Put available abilities in slots.
            {
                if (learnable == null)
                    continue;
                if (learnable.Base == null)
                    continue;
                if (learnable.Level > creature.Level)
                    continue;
                
                Debug.Log($"Learnables equpping {learnable.Base.Name}");
                _entries[available++].ABase = learnable.Base;
            }

            foreach (AbilityBase ability in creature.AbilityBases) // Curate using equipped abilities
            {
                foreach (AbilityEntry entry in _entries)
                {
                    if (entry.ABase != ability)
                        continue;
                    Debug.Log($"Removing extra {entry.ABase.Name}");
                    
                    entry.ABase = null;
                    entry.gameObject.SetActive(false);
                    break; // Only remove one ability per equipped
                }
            }

            int collected = 0;
            
            creature.LearnedAbilities = new AbilityBase[_entries.Count(entry => entry.ABase != null) + 3]; // Create a new list with remaining abilities
            foreach (AbilityEntry entry in _entries)
            {
                if (entry.ABase == null)
                    continue;

                Debug.Log($"Adding to collection {entry.ABase.Name}");
                if (collected < creature.LearnedAbilities.Length)
                    creature.LearnedAbilities[collected++] = entry.ABase; // Add available abilities to Learned list
                else
                    break;
            }
        }
        else // No need to generate abilities, because the creature hasn't learned any abilities that aren't equipped.
        {
            Debug.Log("Last resort");
            creature.LearnedAbilities = new AbilityBase[3];
            _entries = new AbilityEntry[3];
            for (int i = 0; i < _entries.Length; ++i) // Create empty prefabs for the slots in case equipped are unequipped.
            {
                _entries[i] = Instantiate(_entryPrefab.GetComponent<AbilityEntry>(), _entryContainer);
                _entries[i].AbiLoader = this;
                _entries[i].gameObject.SetActive(false); // Turn off unused slots
            }
        }
    }

    public void EquipAbility(AbilityEntry entry)
    {
        if (entry == null)
            return;
        if (entry.ABase == null) // Check if this is a real ability
            return;
        
        for (int i = 0; i < _equipped.Length; ++i)
        {
            if (_equipped[i].ABase != null) // Check for empty equip slot
                continue;

            Debug.Log($"Equipping ability {entry.ABase.Name}");
            _equipped[i].ABase = entry.ABase;
            _creature.AbilityBases[i] = entry.ABase; // Add to equipped

            for (int l = 0; l < _creature.LearnedAbilities.Length; ++l)
            {
                if (_creature.LearnedAbilities[l] != entry.ABase)
                    continue;
                
                _creature.LearnedAbilities[l] = null; // Remove ability from Learned when equipped.
                break;
            }

            entry.ABase = null; // Remove ability from entry
            entry.gameObject.SetActive(false);
            
            _detMenu.UpdateAbilities();
            break;
        }
    }
    public void UnequipAbility(EquippedAbility selectedSlot)
    {
        if (selectedSlot == null)
            return;
        if (selectedSlot.ABase == null) // Check if this is a real ability
            return;
        
        if (1 >= _equipped.Count(equipped => equipped.ABase != null)) // Must have minimum 1 ability
            return;
        
        for (int i = 0; i < _equipped.Length; ++i)
        {
            if (_equipped[i] != selectedSlot) // Find the clicked on slot
                continue;

            Debug.Log($"Unequipping ability {selectedSlot.ABase.Name}");
            
            AbilityEntry emptyEntry = _entries.First(entry => !entry.gameObject.activeSelf); // Find empty entry

            emptyEntry.ABase = selectedSlot.ABase;
            emptyEntry.gameObject.SetActive(true);

            for (int l = 0; l < _creature.LearnedAbilities.Length; ++l) // Find empty slot in Learned
            {
                if (_creature.LearnedAbilities[l] != null)
                    continue;

                _creature.LearnedAbilities[l] = selectedSlot.ABase; // Add ability to Learned
                break;
            }

            selectedSlot.ABase = null;
            _creature.AbilityBases[i] = null; // Remove ability from equipped

            _detMenu.UpdateAbilities();
            break;
        }
    }

    public void CurSelected(AbilityBase abiBase)
    {
        _name.text = abiBase.Name;
        _description.text = abiBase.Description;
    }
}