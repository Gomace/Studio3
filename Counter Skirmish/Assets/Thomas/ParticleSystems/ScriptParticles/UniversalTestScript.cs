using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UniversalTestScript : MonoBehaviour
{
    [SerializeField] private GameObject[] fire;

    public void RemoveFire()
    {
        foreach (GameObject decal in fire)
            decal.SetActive(false);
    }
}
