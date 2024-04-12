using System.IO;
using System.Linq;
using UnityEngine;

// Something something Creature follows you [RequireComponent(typeof(FollowerCreature))]
public class HubCharacter : MonoBehaviour
{
    [Header("This should already be referenced.")]
    [SerializeField] private GameObject _starterUI;
    private string _path;
    
    public CreatureInfo[] Creatures { get; set; } = new CreatureInfo[6];

    private void Awake()
    {
        _path = Application.persistentDataPath + "/SaveData/Json/RosterData.json";
        
        LoadRoster(); // Check if player has a save
        if (Creatures.Any(creature => creature != null)) // If save had creatures, don't give starter
            return;
        
        _starterUI.GetComponent<StarterCreature>().Player = this;// Player has no save, start new game
        _starterUI.SetActive(true);
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
            SaveRoster();
            break;
        }
    }

    public void SaveRoster() => SavingSystem.SaveToJson(new RosterData(Creatures), _path);
    private void LoadRoster() // Load CreatureInfo, not Creature
    {
        if (!File.Exists(_path))
            return;
        
        RosterData data = SavingSystem.LoadFromJson<RosterData>(_path);

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
