using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRTestScript : MonoBehaviour
{
    private Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rope"))
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
        }
    }
}
