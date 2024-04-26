using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class AbilitiesLoader : MonoBehaviour
{
    [SerializeField] private DetailsMenu _detMenu;
    
    #region Elements
    [Header("These should already be referenced.")] // Overview display elements
    [SerializeField] private GameObject _entryPrefab;
    [SerializeField] private Transform _entryContainer;
    [SerializeField] private TMP_Text _name, _description;
    [SerializeField] private EquippedAbility[] _equipped = new EquippedAbility[4];
    #endregion Elements

    private CreatureInfo _creature;
    
    private AbilityEntry[] _entries;

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
        
        if (creature == null) // Check if creature exists
            return;
        if (creature.Base == null) // Check if Base exists
            return;

        int used = 0;

        foreach (AbilityBase aBase in creature.AbilityBases) // Check if Abilities are equipped
        {
            if (aBase == null)
                continue;

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

        int length = creature.Base.LearnableAbilities.Where(learnable => learnable != null).Count(learnable => learnable.Base != null), // Length of non-empty LearnableAbilities
            available = 0;

        _entries = new AbilityEntry[length];
        for (int i = 0; i < length; ++i) // Create empty prefabs for all non-empty LearnableAbilities
        {
            _entries[i] = Instantiate(_entryPrefab.GetComponent<AbilityEntry>(), _entryContainer);
            _entries[i].AbiLoader = this;
        }
        
        foreach (LearnableAbility learnable in creature.Base.LearnableAbilities) // Put available abilities in slots.
        {
            if (learnable == null)
                continue;
            if (learnable.Base == null)
                continue;
            if (learnable.Level > creature.Level)
                continue;

            _entries[available++].ABase = learnable.Base;
        }
    }

    public void EquipAbility(AbilityBase abiBase)
    {
        for (int i = 0; i < _equipped.Length; ++i)
        {
            if (_equipped[i].ABase != null)
                continue;

            _equipped[i].ABase = abiBase;
            _creature.AbilityBases[i] = abiBase;
            
            _detMenu.UpdateAbilities();
            return;
        }
    }
    public void UnequipAbility(EquippedAbility selectedSlot)
    {
        if (1 >= _equipped.Count(equipped => equipped.ABase != null)) // Must have minimum 1 ability
            return;
        
        for (int i = 0; i < _equipped.Length; ++i)
        {
            if (_equipped[i] != selectedSlot)
                continue;
            
            selectedSlot.ABase = null;
            _creature.AbilityBases[i] = null;
            
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