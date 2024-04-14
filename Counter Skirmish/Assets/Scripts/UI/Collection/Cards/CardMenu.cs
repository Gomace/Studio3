using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMenu : MonoBehaviour
{
    #region Events
    public delegate void OnCardsLoad();
    public event OnCardsLoad onCardsLoad; // All cards add themselves to this
    #endregion Events

    [SerializeField] private HubCharacter _player;
    [SerializeField] private GameObject _detailsMenu;
    
    [Header("These should already be filled.")]
    [SerializeField] private RosterCard[] _slots = new RosterCard[6]; // Roster slots

    private CreatureBase _curCreature;

    public List<string> Keywords { get; set; }

    private void OnEnable() => LoadCards(); // Load cards on window open

    public void LoadCards() => onCardsLoad?.Invoke(); // Load all cards
    
    public void AddCreatureToRoster(CreatureInfo creature) // Add Creature to slot
    {
        if (creature.Base == null)
            return;

        _player.AddCreatureToRoster(creature);
        
        LoadRoster();
    }
    public void RemoveCreatureFromRoster(CreatureInfo creature)
    {
        _player.RemoveCreatureFromRoster(creature);
        
        LoadRoster();
    }

    public void DetailsScreen(CreatureInfo creature) // Open that creature's detail screen
    {
        if (creature.Base == null)
            return;
        
        _detailsMenu.SetActive(true);
        _detailsMenu.GetComponent<DetailsMenu>().Creature = creature;
        gameObject.SetActive(false);
    }
    
    private void LoadRoster()
    {
        foreach (RosterCard slot in _slots)
            slot.Creature = null;

        int equipped = 0;

        foreach (CreatureInfo rosterCreature in _player.Creatures)
        {
            if (rosterCreature == null)
                continue;

            if (equipped < _slots.Length)
                _slots[equipped++].Creature = rosterCreature;
            else
                break;
        }
        
        LoadCards();
    }
    
    public void SelectFilter(string keyword) // Add filter
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn)
            Keywords.Add(keyword);
        else
            Keywords.Remove(keyword);

        //LoadCards();
    }
}