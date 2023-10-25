using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeFallOver : MonoBehaviour
{

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Staying");

        if (other.CompareTag("Palisade"))
        {
            Debug.Log("RopeIsStaying");
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        if (other.CompareTag("Palisade"))
        {
            Debug.Log("RopeHasExited");
            rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
        }
            
    }

}
