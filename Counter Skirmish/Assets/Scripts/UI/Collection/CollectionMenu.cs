using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionMenu : MonoBehaviour
{
    public delegate void OnCollectionLoad();
    public static event OnCollectionLoad onCollectionLoad; // All cards add themselves to this

    [SerializeField] private GameObject _detailsMenu;
    //[SerializeField] private RectTransform _filters, _cards;
    //[SerializeField] private GameObject _popUp;
    [SerializeField] private RosterEquipper[] _slots = new RosterEquipper[6]; // Roster slots
    private List<string> _keywords;
    
    private CreatureBase _currentCreature;

    public List<string> Keywords => _keywords;

    private void OnEnable() => LoadCollection();

    public void LoadCollection() => onCollectionLoad?.Invoke(); // Load all cards
    
    public void SelectFilter(string keyword) // Find filter from click
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Toggle>().isOn)
            _keywords.Add(keyword);
        else
            _keywords.Remove(keyword);

        //LoadCollection();
    }

    /*public void PopUpReveal(bool reveal, RectTransform card) // Hover PopUp
    {
        _currentCreature = card.GetComponent<CardInfo>().CBase;
    }*/
    public void AddCreatureToRoster(CreatureBase creature) // Add Creature to slot
    {
        foreach (RosterEquipper slot in _slots)
        {
            if (slot.CBase == creature)
                return;
        }

        foreach (RosterEquipper slot in _slots)
        {
            if (slot.CBase == null)
            {
                slot.CBase = creature;
                break;
            }
        }
        
        LoadCollection();
    }
    
    public void DetailsScreen(CreatureBase creature) // Get details from card
    {
        _detailsMenu.SetActive(true);
        _detailsMenu.GetComponent<DetailsMenu>().CBase = creature;
        gameObject.SetActive(false);
    }
}