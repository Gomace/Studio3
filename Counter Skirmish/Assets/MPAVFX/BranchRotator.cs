using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchRotator : MonoBehaviour
{
    public float angle = 30f;
    
    public void Generated(int index)
    {
       transform.rotation *= Quaternion.Euler(angle * ((index * 2) - 1), 0, 0);
    }
}
