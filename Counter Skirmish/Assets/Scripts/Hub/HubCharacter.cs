using System.Linq;
using UnityEngine;

// Something something Creature follows you [RequireComponent(typeof(FollowerCreature))]
public class HubCharacter : MonoBehaviour
{
    [SerializeField] private GameObject _starter;

    public CreatureInfo[] Creatures { get; set; } = new CreatureInfo[6];

    private void Awake()
    {
        LoadRoster(); // Check if player has a save
        if (Creatures.Any(creature => creature != null)) // If save had creatures, don't give starter
            return;
        
        GameObject starterUI = Instantiate(_starter); // Player has no save, start new game
        starterUI.GetComponent<StarterCreature>().Player = this;
    }
    
    public void AddCreatureToRoster(CreatureInfo creature) // Add Creature to slot
    {
        if (Creatures.Any(slot => slot == creature))
            return;

        for (int i = 0; i < Creatures.Length; ++i)
        {
            if (Creatures[i] != null)
                continue;
            
            Creatures[i] = creature;
            break;
        }
    }

    private void LoadRoster()
    {
        RosterData data = SavingSystem.LoadFromJson<RosterData>(Application.persistentDataPath + "/SaveData/Json/RosterData.json");

        int length = data.Names.Length;

        for (int i = 0; i < length; ++i)
        {
            Creatures[i] = new CreatureInfo(Resources.Load<CreatureBase>($"ScrObjs/Creatures/{data.Names[i]}"), data.Levels[i], data.Exps[i])
            {
                PassiveBase = Resources.Load<PassiveBase>($"ScrObjs/Passives/{data.Passives[i]}")
            };
            for (int l = 0; l < data.Abilities[i].Length; ++l)
                Creatures[i].AbilityBases[l] = Resources.Load<AbilityBase>($"ScrObjs/Abilities/{data.Abilities[i][l]}");
        }
    }
}
