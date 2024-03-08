using System;
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
            if (_curCreature == value) // Ignore change if creature already current
                return;
            
            _curCreature = value;
            _unit.SetupUnit(value); // Tells everyone what new creature is
        }
    }

    private void Awake() => _unit = GetComponent<InstanceUnit>();

    private void Start()
    {
        foreach (Creature creature in _creatures)
            creature.Initialize(_unit);
        
        onRosterLoaded?.Invoke(_creatures);
        NextCreature();
    }

    private void NextCreature() // Send next non-dead creature
    {
        foreach (Creature creature in _creatures)
        {
            if (creature.Health > 0)
                CurCreature = creature; // if CurCreature is changed, it will tell Unit
        }
    }
}
