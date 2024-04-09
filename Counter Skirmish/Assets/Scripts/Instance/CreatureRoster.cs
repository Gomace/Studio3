using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(InstanceUnit))]
public class CreatureRoster : MonoBehaviour
{
    #region Events
    public delegate void OnRosterLoaded(Creature[] @creatures);
    public event OnRosterLoaded onRosterLoaded;
    #endregion Events

    [SerializeField] private Creature[] _creatures;
    private Creature _curCreature; // TODO get creatures from Hub
    private InstanceUnit _unit;
    
    public Creature CurCreature
    {
        get => _curCreature;
        set
        {
            if (value.Health == 0)
                return;
            if (_curCreature == value) // Ignore change if creature already current
                return;
            
            _curCreature = value;
            _unit.SetupUnit(value); // Tells everyone what new creature is
        }
    }

    private void Awake()
    {
        _unit = GetComponent<InstanceUnit>();
        
        if (_creatures == null && CompareTag("Player"))
            LoadRoster();
    }

    private void Start()
    {
        foreach (Creature creature in _creatures)
            creature.Initialize(_unit);
        
        onRosterLoaded?.Invoke(_creatures);
        NextCreature();
    }

    public void NextCreature() // Send next non-dead creature
    {
        foreach (Creature creature in _creatures)
        {
            if (creature.Health > 0)
            {
                CurCreature = creature; // if CurCreature is changed, it will tell Unit
                return;
            }
        }
        // If you get here, you're dead. Reset Instance.
    }

    private void SaveRoster() => SavingSystem.SaveToJson(new RosterData(_creatures), Application.persistentDataPath + "/SaveData/Json/RosterData.json");
    private void LoadRoster()
    {
        RosterData data = SavingSystem.LoadFromJson<RosterData>(Application.persistentDataPath + "/SaveData/Json/RosterData.json");

        _creatures = new Creature[data.Names.Length];

        for (int i = 0; i < _creatures.Length; ++i)
        {
            _creatures[i] = new Creature(Resources.Load<CreatureBase>($"ScrObjs/Creatures/{data.Names[i]}"), data.Levels[i], data.Exps[i])
            {
                
                Passive = new Passive(Resources.Load<PassiveBase>($"ScrObjs/Passives/{data.Passives[i]}"))
            };
            for (int l = 0; l < data.Abilities[i].Length; ++l)
                _creatures[i].Abilities[l] = new Ability(Resources.Load<AbilityBase>($"ScrObjs/Abilities/{data.Abilities[i][l]}"), _creatures[i]);
        }
    }
}