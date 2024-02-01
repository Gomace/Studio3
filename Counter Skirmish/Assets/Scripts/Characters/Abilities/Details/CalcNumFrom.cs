using UnityEngine;

[CreateAssetMenu(fileName = "CalcRef", menuName = "CouSki/Abilities/CalculationReference")]
public class CalcNumFrom : ScriptableObject
{
    [SerializeField] private string _name;

    [TextArea]
    [SerializeField] private string _description;

    public string Name => _name;
    public string Description => _description;
}