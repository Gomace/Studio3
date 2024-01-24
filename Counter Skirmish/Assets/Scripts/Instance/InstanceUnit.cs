using UnityEngine;
using UnityEngine.UI;

public class InstanceUnit : MonoBehaviour
{
    [SerializeField] private CreatureBase _base;
    [SerializeField] private int _level;
    
    [Header("Only players get icons.")]
    [SerializeField] private Image _icon;

    private Creature _creature;

    public Creature Creature => _creature;
    
    public void Setup()
    {
        _creature = new Creature(_base, _level); // add model somehow. Figure it out vvv icon there
        if (_icon)
            _icon.sprite = _creature.Base.Icon;
    }
}