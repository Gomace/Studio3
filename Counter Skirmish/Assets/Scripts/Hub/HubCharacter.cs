using System.Linq;
using UnityEngine;

// Something something Creature follows you [RequireComponent(typeof(FollowerCreature))]
public class HubCharacter : MonoBehaviour
{
    private const string _jsonRosterPath = "/RosterData.json";
    private const string _jsonCollectionPath = "/CollectionData.json";

    public CreatureInfo[] RosterCreatures { get; private set; } = new CreatureInfo[6];
    public CreatureInfo[] CollectionCreatures { get; private set; }

    private void Awake()
    {
        LoadRoster(); // Check if player has a save
        LoadCollection();
        if (RosterCreatures.Any(creature => creature != null)) return; // If save had creatures, don't give starter

        Instantiate(Resources.Load<GameObject>("UI/StarterCreature")).GetComponent<StarterCreature>().Player = this; // Player has no save, start new game
    }
    
    public bool AddCreatureToRoster(CreatureInfo creature) // Add Creature to slot
    {
        if (RosterCreatures.Any(slot => slot == creature)) // Check if already equipped
            return false;

        for (int i = 0; i < RosterCreatures.Length; ++i)
        {
            if (RosterCreatures[i] != null) continue; // Find empty slot
            
            RosterCreatures[i] = creature;
            SaveRoster();
            RemoveCreatureFromCollection(creature);
            return true;
        }

        return false;
    }
    public bool RemoveCreatureFromRoster(CreatureInfo creature, bool rental)
    {
        if (RosterCreatures.All(slot => slot != creature)) // Check if not equipped
            return false;

        if (1 >= RosterCreatures.Where(slot => slot != null).Count(slot => slot.Base != null)) // Minimum one creature at all times
            return false;

        for (int i = 0; i < RosterCreatures.Length; ++i)
        {
            if (RosterCreatures[i] != creature) continue; // Find creature in roster

            RosterCreatures[i] = null;
            SaveRoster();
            if (rental == false)
                AddCreatureToCollection(creature);
            return true;
        }

        return false;
    }

    private void RemoveCreatureFromCollection(CreatureInfo creature)
    {
        if (CollectionCreatures.All(slot => slot != creature)) return; // Check if not stored
        
        for (int i = 0; i < CollectionCreatures.Length; ++i)
        {
            if (CollectionCreatures[i] != creature) continue; // Find Creature in collection

            CollectionCreatures[i] = null;
            SaveCollection();
            return;
        }
    }
    private void AddCreatureToCollection(CreatureInfo creature)
    {
        if (CollectionCreatures.Any(slot => slot == creature)) return; // Check if already stored

        for (int i = 0; i < CollectionCreatures.Length; ++i)
        {
            if (CollectionCreatures[i] != null) continue; // Find empty slot
            
            CollectionCreatures[i] = creature;
            SaveCollection();
            return;
        }
    }

    public void SaveRoster()
    {
        RosterData data = new RosterData();
        
        data.ApplyCreatureInfo(RosterCreatures);
        
        SavingSystem.SaveToJson(data, _jsonRosterPath);
    }
    private void LoadRoster() // Load CreatureInfo, not Creature
    {
        RosterData data = SavingSystem.LoadFromJson<RosterData>(_jsonRosterPath);

        if (data == null) return;

        for (int i = 0; i < data.Names.Length; ++i)
        {
            RosterCreatures[i] = new CreatureInfo(Resources.Load<CreatureBase>($"ScrObjs/Creatures/{data.Names[i]}"), data.Levels[i], data.Exps[i], data.Rental[i])
            {
                PassiveBase = Resources.Load<PassiveBase>($"ScrObjs/Passives/{data.Passives[i]}"),
                AbilityBases = new AbilityBase[4],
                LearnedAbilities = new AbilityBase[data.LearnedAbilities[i].Names.Length + 3]
            };
            for (int l = 0; l < data.Abilities[i].Names.Length; ++l)
                RosterCreatures[i].AbilityBases[l] = Resources.Load<AbilityBase>($"ScrObjs/Abilities/{data.Abilities[i].Names[l]}");
            for (int l = 0; l < data.LearnedAbilities[i].Names.Length; ++l)
                RosterCreatures[i].LearnedAbilities[l] = Resources.Load<AbilityBase>($"ScrObjs/Abilities/{data.LearnedAbilities[i].Names[l]}");
        }
    }

    public void SaveCollection()
    {
        CollectionData data = new CollectionData();
        
        data.ApplyCreatureInfo(CollectionCreatures);
        
        SavingSystem.SaveToJson(data, _jsonCollectionPath);
    }
    private void LoadCollection() // Load CreatureInfo, not Creature
    {
        CollectionData data = SavingSystem.LoadFromJson<CollectionData>(_jsonCollectionPath);

        if (data == null)
        {
            CollectionCreatures = new CreatureInfo[5];
            return;
        }

        CollectionCreatures = new CreatureInfo[data.Names.Length + 5];
        
        for (int i = 0; i < data.Names.Length; ++i)
        {
            CollectionCreatures[i] = new CreatureInfo(Resources.Load<CreatureBase>($"ScrObjs/Creatures/{data.Names[i]}"), data.Levels[i], data.Exps[i])
            {
                PassiveBase = Resources.Load<PassiveBase>($"ScrObjs/Passives/{data.Passives[i]}"),
                AbilityBases = new AbilityBase[4],
                LearnedAbilities = new AbilityBase[data.LearnedAbilities[i].Names.Length + 3]
            };
            for (int l = 0; l < data.Abilities[i].Names.Length; ++l)
                CollectionCreatures[i].AbilityBases[l] = Resources.Load<AbilityBase>($"ScrObjs/Abilities/{data.Abilities[i].Names[l]}");
            for (int l = 0; l < data.LearnedAbilities[i].Names.Length; ++l)
                CollectionCreatures[i].LearnedAbilities[l] = Resources.Load<AbilityBase>($"ScrObjs/Abilities/{data.LearnedAbilities[i].Names[l]}");
        }
    }
}