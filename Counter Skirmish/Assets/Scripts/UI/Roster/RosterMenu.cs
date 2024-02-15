using UnityEngine.EventSystems;
using UnityEngine;

public class RosterMenu : MonoBehaviour
{
    [SerializeField] private CreatureRoster _playerRoster;
    
    [Header("These should already be filled.")]
    [SerializeField] private RosterSlot[] _slots;

    private void OnEnable() => _playerRoster.onRosterLoaded += SetupRoster;
    private void OnDisable() => _playerRoster.onRosterLoaded -= SetupRoster;

    private void SetupRoster(Creature[] creatures) // Put creatures from roster in slots, and disable unfilled ones
    {
        for (int i = 0; i < creatures.Length; ++i)
            _slots[i].Creature = creatures[i];
        
        foreach (RosterSlot slot in _slots)
            slot.gameObject.SetActive(slot.Creature != null);
    }
    // Test if instances of the class are shared through reference, or if the new assignments get copies.

    private void ReloadBars(Creature creature) // Update bars on creature in rosterUI
    {
        foreach (RosterSlot slot in _slots)
        {
            if (slot.Creature == creature)
                slot.SetBars();
        }
    }

    private void LevelUp(Creature creature) // Update level on creature in rosterUI
    {
        foreach (RosterSlot slot in _slots)
        {
            if (slot.Creature == creature)
                slot.SetLevel();
        }
    }

    public void SwapCreature() => _playerRoster.CurCreature = EventSystem.current.currentSelectedGameObject.GetComponent<RosterSlot>().Creature;
}
