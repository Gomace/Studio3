using UnityEngine.EventSystems;
using UnityEngine;

public class RosterMenu : MonoBehaviour
{
    [SerializeField] private CreatureRoster _playerRoster;
    
    [Header("These should already be filled.")]
    [SerializeField] private RosterSlot[] _slots;

    private InstanceUnit _unit;

    private void Awake()
    {
        if (_playerRoster)
            _unit = _playerRoster.gameObject.GetComponent<InstanceUnit>();
    }

    private void OnEnable()
    {
        _playerRoster.onRosterLoaded += SetupRoster;
        _unit.onHealthChanged += ReloadHealth;
        _unit.onResourceChanged += ReloadResource;
        _unit.onDead += CreatureDie;
    }
    private void OnDisable()
    {
        _playerRoster.onRosterLoaded -= SetupRoster;
        _unit.onHealthChanged -= ReloadHealth;
        _unit.onResourceChanged -= ReloadResource;
        _unit.onDead -= CreatureDie;
    }

    private void SetupRoster(Creature[] creatures) // Put creatures from roster in slots, and disable unfilled ones
    {
        for (int i = 0; i < creatures.Length; ++i)
            _slots[i].Creature = creatures[i];
        
        foreach (RosterSlot slot in _slots)
            slot.gameObject.SetActive(slot.Creature != null);
    }

    private void ReloadHealth(float newH)
    {
        foreach (RosterSlot slot in _slots)
        {
            if (slot.Creature == _unit.Creature)
                slot.SetHealth(newH);
        }
    }
    private void ReloadResource(float newR)
    {
        foreach (RosterSlot slot in _slots)
        {
            if (slot.Creature == _unit.Creature)
                slot.SetResource(newR);
        }
    }

    private void LevelUp() // TODO Update level on creature in rosterUI
    {
        foreach (RosterSlot slot in _slots)
        {
            if (slot.Creature == _unit.Creature)
                slot.SetLevel();
        }
    }

    private void CreatureDie()
    {
        foreach (RosterSlot slot in _slots)
        {
            if (slot.Creature == _unit.Creature)
                slot.Dead.SetActive(true);
        }
    }

    public void SwapCreature() => _playerRoster.CurCreature = EventSystem.current.currentSelectedGameObject.GetComponent<RosterSlot>().Creature;
}
