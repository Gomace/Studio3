using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRigidTest : MonoBehaviour
{
    Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Rope"))
        {
            Debug.Log("FireInTheRope");

            rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
           
            SphereCollider boxCollider = other.GetComponent<SphereCollider>();
            boxCollider.enabled = true;

        }
    }

}
