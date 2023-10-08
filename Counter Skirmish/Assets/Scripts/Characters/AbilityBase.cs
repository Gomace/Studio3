using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Create new Ability")]
public class AbilityBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private CreatureType type;
    [SerializeField] private int power, cooldown, resource;
    
    public string Name => name;
    public string Description => description;
    public CreatureType Type => type;
    public int Power => power;
    public int Cooldown => cooldown;
    public int Resource => resource;
}