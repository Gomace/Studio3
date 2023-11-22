using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRigidTest : MonoBehaviour
{
    // This script is used to make the decal object trigger the rope
    // to fall when its hit.

    // Rigidbody Component that I will use
    // later to get the rigidbody of the rope
    Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Rope"))
        {
            Debug.Log("FireInTheRope");

            // If the other object (Rope in this case) has a "Rope" tag then:
            // Get the rigidbody of the rope and set it to not be kinematic and use gravity.
            rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
           
            // If I hit the rope, activate the Sphere collider on it so that it can fall to the ground
            // and roll. The spherecollider is inactive on start.
            SphereCollider ropeCollider = other.GetComponent<SphereCollider>();
            ropeCollider.enabled = true;

            //other.GetComponent<UniversalTestScript>().RemoveFire();

        }
    }

}
