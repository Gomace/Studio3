using UnityEngine;

[CreateAssetMenu(fileName = "CalcRef", menuName = "CouSki/Abilities/CalculationReference")]
public class CalcNumFrom : ScriptableObject
{
    [TextArea]
    [SerializeField] private string _description;

    public string Name => name;
    public string Description => _description;
}