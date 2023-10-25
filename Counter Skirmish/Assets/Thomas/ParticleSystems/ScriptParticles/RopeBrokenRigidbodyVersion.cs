using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBrokenRigidbodyVersion : MonoBehaviour
{
    /* The way that this script works is that it first uses an OnTriggerEnter function to check if
    * an object has collided with another object that has the Rope tag attached. If it collides with any
    * other tag nothing will happen. If it does collide with the Rope tag, the code will run.
    * The code in the function will first check in an if statement, if you have collided with the rope tag or not
    * 
    * If you have, it will destroy the object you collided with (The one that has the rope tag)
    * and will instantiate a prefab called ropeModel.
    * 
    * It will instantiate at the transform and rotation of the collided object, so that it
    * looks like the model is swapping. 
    * 
    * In the update method I have an if statement that checks if all ropes have been destroyed, if they have, they will
    * replace the palisade model with ones that have colliders without triggers and rigidbodies so that they become
    * physics objects and will therefore fall over.*/

    [SerializeField] GameObject ropeModel;

    //The palisade models, parent is the regular one, Broken has the rigidbody.
    /*public GameObject palisadeParent;
    public GameObject palisadeBroken;

    public RopeRigidbody rope;

    //All the ropes that are spawned on start which will be checked to see if they are active or not.
    public RopeRigidbody Rope;
    public RopeRigidbody Rope1;
    public RopeRigidbody Rope2;
    public RopeRigidbody Rope3;
    public RopeRigidbody Rope4;
    public RopeRigidbody Rope5;
    public RopeRigidbody Rope6;
    public RopeRigidbody Rope7;
    public RopeRigidbody Rope8;
    public RopeRigidbody Rope9;

    public GameObject ropeObject;
    public GameObject ropeObject1;
    public GameObject ropeObject2;
    public GameObject ropeObject3;
    public GameObject ropeObject4;
    public GameObject ropeObject5;
    public GameObject ropeObject6;
    public GameObject ropeObject7;
    public GameObject ropeObject8;
    public GameObject ropeObject9;
    



    private void Update()
    {

        
    }

    private void Start()
    {
    }
    //OnTriggerEnter method, activates when a collider enters another (requires one object to have a rigidbody)
    public void OnTriggerEnter(Collider other)
    {
        //Checks if the collided object has the rope tag attached to it, and only then will the statement run
        if (other.CompareTag("Rope"))
        {
            rope.RopeFunction();
            Debug.Log("RopeFunctionStarted");
        }

        if (other.CompareTag("Rope1"))
        {
            rope.RopeFunction1();
            Debug.Log("RopeFunctionONEStarted");
        }

        if (ropeObject2.CompareTag("Rope2"))
        {
            rope.RopeFunction2();
        }

        if (ropeObject3.CompareTag("Rope3"))
        {
            rope.RopeFunction3();
        }

        if (ropeObject4.CompareTag("Rope"))
        {
            Rope.RopeFunction4();
        }

        if (ropeObject5.CompareTag("Rope"))
        {
            Rope.RopeFunction5();
        }

        if (ropeObject6.CompareTag("Rope"))
        {
            Rope.RopeFunction6();
        }

        if (ropeObject7.CompareTag("Rope"))
        {
            Rope.RopeFunction7();
        }

        if (ropeObject8.CompareTag("Rope"))
        {
            Rope.RopeFunction8();
        }

        if (ropeObject9.CompareTag("Rope"))
        {
            Rope.RopeFunction9();
        }

    }*/

}
