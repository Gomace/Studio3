using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeFallOver : MonoBehaviour
{
    private bool staying;
    //Rigidbody component that I use later in the script
    //to tell the Palisade's rigidbody to change
    Rigidbody rb;

    // Rigidbody of the rope
    public Rigidbody ropeRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        // Gets the rigidbody of this object (Palisade pole)
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set
        rb.useGravity = ropeRigidbody.useGravity;
        rb.isKinematic = ropeRigidbody.isKinematic;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Fire") == false)
            return;
        Debug.Log("Staying");

        if (other.CompareTag("Fire"))
        {
            Debug.Log("RopeIsStaying");
            staying = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        staying = false;
        Debug.Log("Exited");
        if (staying == true)
            return;

        /*if (other.CompareTag("Fire") && staying == false)
        {
            Debug.Log("RopeHasExited");
            rb = this.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
        }*/

    }

}
