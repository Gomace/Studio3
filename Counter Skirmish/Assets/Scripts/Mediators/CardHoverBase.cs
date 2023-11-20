using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardHover", menuName = "Mediator/CardHover")]
public class CardHoverBase : ScriptableObject
{
    [SerializeField] private RectTransform _hover, _popUp;

    private CreatureBase _currentCreature;
    
    public void CardHover(bool reveal, RectTransform card)
    {
        _hover.position = card.position;
        _currentCreature = card.GetComponent<CardInfo>().Base;
        _hover.gameObject.SetActive(reveal);
    }

    public void CreatureOverview()
    {
        
    }
}