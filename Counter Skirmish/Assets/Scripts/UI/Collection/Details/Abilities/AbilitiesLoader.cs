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

        foreach (EquippedAbility equipped in _equipped)
            equipped.ABase = null;
        
        if (creature == null) // Check if creature exists
            return;
        if (creature.Base == null) // Check if Base exists
            return;

        // Equipped
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

        // Ability Collection Entries
        int length = creature.Base.LearnableAbilities.Where(learnable => learnable != null).Count(learnable => learnable.Base != null), // Length of non-empty LearnableAbilities
            available = 0;

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

            _entries[available++].ABase = learnable.Base;
        }
    }

    public void EquipAbility(AbilityEntry entry)
    {
        if (entry == null)
            return;
        if (entry.ABase == null)
            return;
        
        for (int i = 0; i < _equipped.Length; ++i)
        {
            if (_equipped[i].ABase != null) // Check for empty equip slot
                continue;

            _equipped[i].ABase = entry.ABase;
            _creature.AbilityBases[i] = entry.ABase;

            entry.ABase = null;
            entry.gameObject.SetActive(false);
            
            _detMenu.UpdateAbilities();
            return;
        }
    }
    public void UnequipAbility(EquippedAbility selectedSlot)
    {
        if (selectedSlot == null)
            return;
        if (selectedSlot.ABase == null)
            return;
        
        if (1 >= _equipped.Count(equipped => equipped.ABase != null)) // Must have minimum 1 ability
            return;
        
        for (int i = 0; i < _equipped.Length; ++i)
        {
            if (_equipped[i] != selectedSlot)
                continue;

            AbilityEntry emptyEntry = _entries.First(entry => !entry.gameObject.activeSelf);

            emptyEntry.ABase = selectedSlot.ABase;
            emptyEntry.gameObject.SetActive(true);
            
            selectedSlot.ABase = null;
            _creature.AbilityBases[i] = null;

            _detMenu.UpdateAbilities();
            return;
        }
    }

    public void CurSelected(AbilityBase abiBase)
    {
        _name.text = abiBase.Name;
        _description.text = abiBase.Description;
    }
}