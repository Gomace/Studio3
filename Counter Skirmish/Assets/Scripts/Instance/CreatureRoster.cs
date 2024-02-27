using UnityEngine;

public class CreatureRoster : MonoBehaviour
{
    #region Events
    public delegate void OnRosterLoaded(Creature[] @creatures);
    public event OnRosterLoaded onRosterLoaded;
    public delegate void OnSendCreature(Creature @creature);
    public event OnSendCreature onSendCreature;
    #endregion Events
    
    [SerializeField] private Creature[] _creatures;
    private Creature _curCreature; // TODO get creatures from Hub
    
    public Creature CurCreature
    {
        get => _curCreature;
        set
        {
            if (_curCreature == value) // Ignore change if creature already current
                return;
            
            _curCreature = value;
            onSendCreature?.Invoke(value); // Tells everyone what new creature is
        }
    }

    private void Start()
    {
        foreach (Creature creature in _creatures)
            creature.Initialize(gameObject);
        
        onRosterLoaded?.Invoke(_creatures);
        NextCreature();
    }

    private void NextCreature() // Send next non-dead creature
    {
        foreach (Creature creature in _creatures)
        {
            if (creature.Health > 0)
                CurCreature = creature; // if CurCreature is changed, it will tell everyone
        }
    }
}
