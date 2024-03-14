using UnityEngine;

[CreateAssetMenu(fileName = "AbiClass", menuName = "CouSki/Abilities/AbilityClass")]
public class AbiClass : ScriptableObject
{
    [TextArea]
    [SerializeField] private string _description;

    [SerializeField] private GameObject _model;
    [SerializeField] private float _speed = 0;
    

    public string Name => name;
    public string Description => _description;

    public GameObject Model => _model;
    public float Speed => _speed;
}