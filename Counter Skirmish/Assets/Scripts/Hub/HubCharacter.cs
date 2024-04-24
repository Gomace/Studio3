using System.Linq;
using UnityEngine;

// Something something Creature follows you [RequireComponent(typeof(FollowerCreature))]
public class HubCharacter : MonoBehaviour
{
    private const string _jsonPath = "/RosterData.json";

    public CreatureInfo[] Creatures { get; private set; } = new CreatureInfo[6];

    private void Awake()
    {
        LoadRoster(); // Check if player has a save
        if (Creatures.Any(creature => creature != null)) // If save had creatures, don't give starter
            return;

        Instantiate(Resources.Load<GameObject>("UI/StarterCreature")).GetComponent<StarterCreature>().Player = this; // Player has no save, start new game
    }
    
    public void AddCreatureToRoster(CreatureInfo creature) // Add Creature to slot
    {
        if (Creatures.Any(slot => slot == creature)) // Check if already equipped
            return;

        for (int i = 0; i < Creatures.Length; ++i)
        {
            if (Creatures[i] != null) // Find empty slot
                continue;
            
            Creatures[i] = creature;
            SaveRoster();
            break;
        }
    }
    public void RemoveCreatureFromRoster(CreatureInfo creature)
    {
        if (Creatures.All(slot => slot != creature)) // Check if not equipped
            return;

        int remaining = Creatures.Count(slot => slot != null);
        if (remaining - 1 < 1) // Minimum one creature at all times
            return;

        for (int i = 0; i < Creatures.Length; ++i)
        {
            if (Creatures[i] != creature) // Find creature in roster
                continue;

            Creatures[i] = null;
            SaveRoster();
            break;
        }
    }

    public void SaveRoster()
    {
        RosterData data = new RosterData();
        
        data.ApplyCreatureInfo(Creatures);
        
        SavingSystem.SaveToJson(data, _jsonPath);
    }
    private void LoadRoster() // Load CreatureInfo, not Creature
    {
        RosterData data = SavingSystem.LoadFromJson<RosterData>(_jsonPath);

        if (data == null)
            return;

        for (int i = 0; i < data.Names.Length; ++i)
        {
            Creatures[i] = new CreatureInfo(Resources.Load<CreatureBase>($"ScrObjs/Creatures/{data.Names[i]}"), data.Levels[i], data.Exps[i])
            {
                PassiveBase = Resources.Load<PassiveBase>($"ScrObjs/Passives/{data.Passives[i]}"),
                AbilityBases = new AbilityBase[4]
            };
            for (int l = 0; l < data.Abilities[i].Names.Length; ++l)
                Creatures[i].AbilityBases[l] = Resources.Load<AbilityBase>($"ScrObjs/Abilities/{data.Abilities[i].Names[l]}");
        }
    }
}