using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UniversalTestScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody cubeRigid;

        if (other.CompareTag("Fire"))
        {
            cubeRigid = GetComponent<Rigidbody>();
            cubeRigid.isKinematic = false;
            cubeRigid.useGravity = true;
        }
    }
}
