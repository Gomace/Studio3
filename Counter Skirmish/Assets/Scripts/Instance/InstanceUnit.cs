using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceUnit : MonoBehaviour
{
    [SerializeField] private CreatureBase _base;
    [SerializeField] private int level;
    //[SerializeField] private bool isPlayerUnit;

    public Creature Creature { get; set; }
    
    public void Setup()
    {
        Creature = new Creature(_base, level);
        /*if (isPlayerUnit)
            GetComponent<Image>().sprite = Creature.Base.BackSprite;*/
    }
}
