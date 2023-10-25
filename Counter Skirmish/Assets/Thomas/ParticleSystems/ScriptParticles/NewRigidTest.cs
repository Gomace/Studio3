using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRigidTest : MonoBehaviour
{
    Rigidbody rb;
    public GameObject crate;

    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rope"))
        {
            rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            
        }
    }

}
