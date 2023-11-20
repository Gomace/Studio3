using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type", menuName = "CouSki/Type")]
public class TypeBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private TypeBase[] _strength, _weakness;

    public string Name => _name;
    public Sprite Icon => _icon;
    
    public TypeBase[] Strength => _strength;
    public TypeBase[] Weakness => _weakness;
}
