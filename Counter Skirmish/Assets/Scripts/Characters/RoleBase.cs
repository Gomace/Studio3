using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Role", menuName = "CouSki/Role")]
public class RoleBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    [TextArea]
    [SerializeField] private string _description;

    public string Name => _name;
    public Sprite Icon => _icon;
    public string Description => _description;
}
