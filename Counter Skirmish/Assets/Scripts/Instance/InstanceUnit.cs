using UnityEngine;
using UnityEngine.UI;

public class InstanceUnit : MonoBehaviour
{
    [SerializeField] private CreatureBase _base;
    [SerializeField] private int level;
    [SerializeField] private bool isPlayerUnit;

    public Creature Creature { get; set; }
    
    public void Setup()
    {
        Creature = new Creature(_base, level); // add model somehow. Figure it out vvv icon there
        if (isPlayerUnit)
            GetComponent<Image>().sprite = Creature.Base.Icon;
    }
}