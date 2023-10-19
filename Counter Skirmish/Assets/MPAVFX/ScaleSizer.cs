using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSizer : MonoBehaviour
{
    public float scalar = 0.5f;
    
    public void Generated(int index)
    {
        transform.localScale *= scalar;
    }
}
