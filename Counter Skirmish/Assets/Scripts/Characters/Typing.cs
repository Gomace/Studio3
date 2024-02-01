using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type", menuName = "CouSki/Type")]
public class Typing : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [Header("Defending, these typings are my")]
    [SerializeField] private Typing[] _strengths, _weaknesses;

    public string Name => _name;
    public Sprite Icon => _icon;
    
    public Typing[] Strengths => _strengths;
    public Typing[] Weaknesses => _weaknesses;
}
