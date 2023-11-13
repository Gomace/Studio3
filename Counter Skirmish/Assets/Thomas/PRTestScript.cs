using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PRTestScript : MonoBehaviour
{
    private Rigidbody rb;
    private void Start()
    {
       
    }

    private void Update()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rope"))
        {
            Destroy(this.gameObject);
        }
    }
}
