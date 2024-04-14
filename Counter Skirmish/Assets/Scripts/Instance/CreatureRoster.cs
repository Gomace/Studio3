using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InstanceUnit))]
public class CreatureRoster : MonoBehaviour
{
    #region Events
    public delegate void OnRosterLoaded(Creature[] @creatures);
    public event OnRosterLoaded onRosterLoaded;
    public delegate void OnFullDead();
    public event OnFullDead onFullDead;
    #endregion Events

    [SerializeField] private Creature[] _creatures = new Creature[6];
    private Creature _curCreature;
    private InstanceUnit _unit;

    private const string _jsonPath = "/RosterData.json";
    
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
        
        if (_creatures.Where(creature => creature != null).Any(creature => creature.Base != null)) 
            return;
        
        if (CompareTag("Player"))
            LoadRoster();
        else
            Debug.LogError($"{gameObject.name} has no Creatures equipped!");
    }

    private void Start()
    {
        foreach (Creature creature in _creatures)
            creature?.Initialize(_unit);
        
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
        
        onFullDead?.Invoke();
    }

    public void SaveRoster()
    {
        RosterData data = new RosterData();
        
        data.ApplyCreature(_creatures);
        
        SavingSystem.SaveToJson(data, _jsonPath);
    }
    private void LoadRoster() // Load Creature, not CreatureInfo
    {
        RosterData data = SavingSystem.LoadFromJson<RosterData>(_jsonPath);

        if (data == null)
            return;

        _creatures = new Creature[6];
        
        for (int i = 0; i < data.Names.Length; ++i)
        {
            _creatures[i] = new Creature(Resources.Load<CreatureBase>($"ScrObjs/Creatures/{data.Names[i]}"), data.Levels[i], data.Exps[i])
            {
                Passive = new Passive(Resources.Load<PassiveBase>($"ScrObjs/Passives/{data.Passives[i]}")),
                Abilities = new Ability[4]
            };
            for (int l = 0; l < data.Abilities[i].Names.Length; ++l)
                _creatures[i].Abilities[l] = new Ability(Resources.Load<AbilityBase>($"ScrObjs/Abilities/{data.Abilities[i].Names[l]}"), _creatures[i]);
        }
    }
}