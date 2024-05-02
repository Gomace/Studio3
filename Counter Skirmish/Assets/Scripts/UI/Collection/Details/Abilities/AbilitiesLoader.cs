using System.Collections.Generic;
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

    private Stack<GameObject> _entryStack = new();
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
        ResetAbilityScreen();
        
        if (creature == null)
            return;
        if (creature.Base == null)
            return;

        _creature = creature;
        
        SetupEquipped(creature);
        SetupEntries(creature);
    }

    public void EquipAbility(AbilityEntry entry)
    {
        if (entry == null)
            return;
        if (entry.ABase == null)
            return;
        if (_equipped == null)
            return;
        if (_creature?.AbilityBases == null)
            return;
        if (_creature?.LearnedAbilities == null)
            return;
        
        for (int i = 0; i < _equipped.Length; ++i)
        {
            if (_equipped[i].ABase != null) // Check for empty equip slot
                continue;
            if (_creature.AbilityBases[i] != null)
            {
                _equipped[i].ABase = _creature.AbilityBases[i];
                continue;
            }

            _equipped[i].ABase = entry.ABase;
            _creature.AbilityBases[i] = entry.ABase; // Add to equipped

            for (int l = 0; l < _creature.LearnedAbilities.Length; ++l)
            {
                if (_creature.LearnedAbilities[l] != entry.ABase)
                    continue;
                
                Debug.Log($"Equipping ability {entry.ABase.Name}");
                _creature.LearnedAbilities[l] = null; // Remove ability from Learned
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
        if (selectedSlot.ABase == null)
            return;
        if (_equipped == null)
            return;
        if (_entries == null)
            return;
        if (_creature?.AbilityBases == null)
            return;
        if (_creature?.LearnedAbilities == null)
            return;
        
        if (1 >= _equipped.Where(equipped => equipped != null).Count(equipped => equipped.ABase != null)) // Must have minimum 1 ability
            return;
        if (1 >= _creature.AbilityBases.Count(ability => ability != null))
        {
            for (int i = 0; i < _creature.AbilityBases.Length; ++i) // Make _equipped match AbilityBases
                if (i < _equipped.Length)
                    _equipped[i].ABase = _creature.AbilityBases[i];
            return;
        }
        
        for (int i = 0; i < _equipped.Length; ++i)
        {
            if (_equipped[i] != selectedSlot) // Find the clicked on slot
                continue;
            if (_creature.AbilityBases[i] != selectedSlot.ABase)
            {
                for (int l = 0; l < _creature.AbilityBases.Length; ++l)
                    if (l < _equipped.Length)
                        _equipped[l].ABase = _creature.AbilityBases[l];
                continue;
            }
            
            AbilityEntry emptyEntry = _entries.FirstOrDefault(entry => !entry.gameObject.activeSelf); // Find empty entry
            if (emptyEntry == null)
                return;
            
            emptyEntry.ABase = selectedSlot.ABase; // Add ability to entry
            emptyEntry.gameObject.SetActive(true);

            for (int l = 0; l < _creature.LearnedAbilities.Length; ++l) // Find empty slot in Learned
            {
                if (_creature.LearnedAbilities[l] != null)
                    continue;

                Debug.Log($"Unequipping ability {selectedSlot.ABase.Name}");
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

    private void ResetAbilityScreen()
    {
        if (_equipped != null)
            foreach (EquippedAbility equipped in _equipped) // Empty the equipped
            {
                if (equipped == null)
                    continue;
                
                equipped.ABase = null;
            }
        
        if (_entries != null)
            foreach (AbilityEntry entry in _entries) // Empty the entries and add to _entryStack
            {
                if (entry == null)
                    continue;
                
                entry.ABase = null;
                
                entry.gameObject.SetActive(false);
                _entryStack.Push(entry.gameObject);
            }
    }

    private void SetupEquipped(CreatureInfo creature)
    {
        if (_equipped == null)
            return;
        
        int used = 0;
        
        foreach (AbilityBase aBase in creature.AbilityBases) // Check if Abilities are equipped in Creature
        {
            if (aBase == null)
                continue;

            Debug.Log($"Equipping {used}");
            if (used < _equipped.Length)
                _equipped[used++].ABase = aBase; // Put abilities in equipped-slots
        }

        if (creature.AbilityBases.Length >= 4) // Make sure length of array is 4 - for bugs
            return;
        
        int equip = 0;

        creature.AbilityBases = new AbilityBase[4];
        foreach (EquippedAbility equipped in _equipped)
        {
            if (equipped.ABase == null)
                continue;

            creature.AbilityBases[equip++] = equipped.ABase;
        }
    }

    private void SetupEntries(CreatureInfo creature)
    {
        if (creature.LearnedAbilities?.Length > 0) // If have collection of learned
        {
            int length = creature.LearnedAbilities.Count(ability => ability != null), // Length of non-empty LearnedAbilities
                available = 0;

            Debug.Log("Using existing");
            _entries = new AbilityEntry[length + 3];
            for (int i = 0; i < _entries.Length; ++i) // Create empty prefabs for all non-empty LearnedAbilities
            {
                if (!_entryStack.TryPop(out GameObject entry))
                {
                    entry = Instantiate(_entryPrefab, _entryContainer);
                    entry.GetComponent<AbilityEntry>().AbiLoader = this;
                }
                entry.SetActive(true);
                _entries[i] = entry.GetComponent<AbilityEntry>();
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
            int length = creature.Base.LearnableAbilities.Where(learnable => learnable != null).Where(learnable => learnable.Base != null).Count(learnable => learnable.Level <= creature.Level), // Length of non-empty LearnableAbilities
                available = 0;

            Debug.Log("Creating new");
            _entries = new AbilityEntry[length + 3];
            for (int i = 0; i < _entries.Length; ++i) // Create empty prefabs for all non-empty LearnableAbilities
            {
                if (!_entryStack.TryPop(out GameObject entry))
                {
                    entry = Instantiate(_entryPrefab, _entryContainer);
                    entry.GetComponent<AbilityEntry>().AbiLoader = this;
                }
                entry.SetActive(true);
                _entries[i] = entry.GetComponent<AbilityEntry>();
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
                if (!_entryStack.TryPop(out GameObject entry))
                {
                    entry = Instantiate(_entryPrefab, _entryContainer);
                    entry.GetComponent<AbilityEntry>().AbiLoader = this;
                }
                entry.SetActive(false);
                _entries[i] = entry.GetComponent<AbilityEntry>();
            }
        }
    }
}