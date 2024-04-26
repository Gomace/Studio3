using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardMenu : MonoBehaviour
{
    #region Events
    public delegate void OnCardsLoad();
    public event OnCardsLoad onCardsLoad; // All cards add themselves to this
    #endregion Events

    [SerializeField] private HubCharacter _player;
    [SerializeField] private GameObject _detailsMenu;
    
    [Header("These should already be filled.")]
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private TMP_Text _name, _description;
    [SerializeField] private RosterCard[] _slots = new RosterCard[6]; // Roster slots
    [SerializeField] private CreatureInfo[] _rentals;

    private CardInfo[] _cards;

    public List<string> Keywords { get; set; }

    private void Start() // Load cards on the first load
    {
        foreach (RosterCard slot in _slots)
            slot.CardMenu = this;
        
        LoadRoster();
        LoadCollection();
        LoadCards();
    }
    
    /*private void OnEnable()
    {
        LoadRoster(); // Load roster on window open
        LoadCards();
    }*/

    public void LoadCards() // Load all cards
    {
        foreach (CardInfo card in _cards)
            card.LoadInfo();
        
        foreach (RosterCard slot in _slots)
            slot.LoadInfo();
    }
    
    public void AddCreatureToRoster(CardInfo card) // Add slot Creature to Player
    {
        if (card.Creature == null)
            return;
        if (card.Creature.Base == null)
            return;

        if (_player.AddCreatureToRoster(card.Creature))
            card.Creature = null;

        LoadRoster();
        LoadCards();
    }
    public void RemoveCreatureFromRoster(CreatureInfo creature) // Remove Creature from Player
    {
        if (_player.RemoveCreatureFromRoster(creature, _rentals.Any(slot => slot == creature)))
        {
            foreach (CardInfo card in _cards)
            {
                if (card.Creature != null)
                    continue;
                
                card.Creature = creature;
                break;
            }
        }
        
        LoadRoster();
        LoadCards();
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
        foreach (RosterCard slot in _slots) // Remove old Creatures from slots
            slot.Creature = null;

        int equipped = 0;

        foreach (CreatureInfo rosterCreature in _player.RosterCreatures) // Add Creatures from Player to slots
        {
            if (rosterCreature == null)
                continue;

            if (equipped < _slots.Length)
                _slots[equipped++].Creature = rosterCreature;
            else
                break;
        }
    }

    private void LoadCollection()
    {
        int pcLength = _player.CollectionCreatures.Length,
            rentals = _rentals?.Length ?? 0;
        
        _cards = new CardInfo[pcLength + rentals]; // Make slots to match max Creature count in collection
        
        for (int i = 0; i < pcLength + rentals; ++i) // Create empty prefabs for all Creatures
        {
            _cards[i] = Instantiate(_cardPrefab.GetComponent<CardInfo>(), _cardContainer);
            _cards[i].CardMenu = this;
        }
        
        int used = 0;
        
        foreach (CreatureInfo collectionCreature in _player.CollectionCreatures) // Put all Creatures into cards
        {
            if (collectionCreature == null)
                continue;
            if (collectionCreature.Base == null)
                continue;
            
            if (used < _cards.Length)
                _cards[used++].Creature = collectionCreature; // Put player Creature into card
            else
                break;
        }

        if (_rentals == null)
            return;
        
        foreach (CreatureInfo rental in _rentals) // Put all rental Creatures into cards
        {
            if (rental == null)
                continue;
            if (rental.Base == null)
                continue;
            
            if (used < _cards.Length)
                _cards[used++].Creature = rental; // Put rental Creature into card
            else
                break;
        }
    }
    
    public void CurSelected(CreatureInfo creature)
    {
        _name.text = creature.Base.Name;
        _description.text = creature.Base.Description;
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