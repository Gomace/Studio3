using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRigidbody : MonoBehaviour
{
    public RopeBroken ropeBrokenScript;
    public Rigidbody rb;
    public Rigidbody rb1;
    public Rigidbody rb2;

    // Start is called before the first frame update
    void Start()
    {
        
        rb.GetComponent<Rigidbody>();
        rb1.GetComponent<Rigidbody>();
        rb2.GetComponent<Rigidbody>();
    }

    public void RopeFunction()
    {      
        rb.isKinematic = false;
        rb.useGravity = true;
        Debug.Log("RopeFunctionActivated");
    }

    public void RopeFunction1()
    {
        rb1.isKinematic = false;
        rb1.useGravity = true;
        Debug.Log("RopeFunctionONEActivated");
    }

    public void RopeFunction2()
    {
        rb2.isKinematic = false;
        rb2.useGravity = true;
    }

    public void RopeFunction3()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void RopeFunction4()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void RopeFunction5()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void RopeFunction6()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void RopeFunction7()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void RopeFunction8()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void RopeFunction9()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }


}
