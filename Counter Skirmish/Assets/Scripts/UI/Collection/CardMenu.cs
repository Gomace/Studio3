using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMenu : MonoBehaviour
{
    public delegate void OnCardsLoad();
    public event OnCardsLoad onCardsLoad; // All cards add themselves to this

    [SerializeField] private GameObject _detailsMenu;
    //[SerializeField] private RectTransform _filters, _cards;
    //[SerializeField] private GameObject _popUp;
    [SerializeField] private RosterEquipper[] _slots = new RosterEquipper[6]; // Roster slots

    private CreatureBase _curCreature;

    public List<string> Keywords { get; set; }

    private void OnEnable() => LoadCards(); // Load cards on window open

    public void LoadCards() => onCardsLoad?.Invoke(); // Load all cards
    
    public void AddCreatureToRoster(CreatureBase creature) // Add Creature to slot
    {
        foreach (RosterEquipper slot in _slots) // Check creature is not already equipped
        {
            if (slot.CBase == creature)
                return;
        }

        foreach (RosterEquipper slot in _slots) // Add creature to potentially empty slot
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

    /*public void PopUpReveal(bool reveal, RectTransform card) // Hover PopUp
    {
        _curCreature = card.GetComponent<CardInfo>().CBase;
    }*/
}