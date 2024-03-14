using UnityEngine;

[CreateAssetMenu(fileName = "CalcMetric", menuName = "CouSki/Abilities/CalculationMetric")]
public class CalcMetric : ScriptableObject
{
    [SerializeField] private Sprite _icon;

    [TextArea]
    [SerializeField] private string _description;

    public string Name => name;
    public Sprite Icon => _icon;
    public string Description => _description;
}