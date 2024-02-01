using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class InstanceUnit : MonoBehaviour
{
    [SerializeField] private CreatureBase _base;
    [SerializeField] private int _level;
    
    [Header("Only players get icons.")]
    [SerializeField] private Image _icon;
    [SerializeField] private bool isPlayerUnit;

    private Creature _creature;

    public CreatureBase Base => _base;
    public Creature Creature => _creature;
    
    public void Setup()
    {
        _creature = new Creature(_base, _level); // add model somehow. Figure it out vvv icon there
        if (_icon || isPlayerUnit)
            _icon.sprite = _creature.Base.Icon;
        
        PlayEnterAnim();
    }

    public void PlayEnterAnim()
    {
        if (isPlayerUnit)
            Debug.Log("Player entered.");
        else
            Debug.Log("Enemy entered.");
    }

    public void PlayAttackAnim()
    {
        if (isPlayerUnit)
            Debug.Log("Player attacked.");
        else
            Debug.Log("Enemy attacked.");
    }

    public void PlayHitAnim()
    {
        Debug.Log("Target hit.");
    }

    public void PlayFaintAnim()
    {
        Debug.Log("Target fainted.");
    }
}