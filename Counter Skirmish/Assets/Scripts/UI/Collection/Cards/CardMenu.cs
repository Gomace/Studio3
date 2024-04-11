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
    
    public void AddCreatureToRoster(CreatureBase creature) // Add Creature to slot
    {
        foreach (RosterCard slot in _slots) // Check creature is not already equipped
        {
            if (slot.CBase == creature)
                return;
        }

        foreach (RosterCard slot in _slots) // Add creature to potentially empty slot
        {
            if (slot.CBase == null)
            {
                slot.CBase = creature;
                break;
            }
        }
        
        LoadCards();
    }

    public void DetailsScreen(CreatureBase creature) // Open that creature's detail screen
    {
        _detailsMenu.SetActive(true);
        _detailsMenu.GetComponent<DetailsMenu>().CBase = creature;
        gameObject.SetActive(false);
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