using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grower : MonoBehaviour
{
    public void Generated(int index)
    {
        Transform me = transform;
        me.position += me.up * me.localScale.y;
    }
}
