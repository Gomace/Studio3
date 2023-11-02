using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBroken : MonoBehaviour
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
    
    /*[SerializeField] GameObject ropeModel;

    //The palisade models, parent is the regular one, Broken has the rigidbody.
    public GameObject palisadeParent;
    public GameObject palisadeBroken;
 
    //All the ropes that are spawned on start which will be checked to see if they are active or not.
    public GameObject brokenRope;
    public GameObject brokenRope1;
    public GameObject brokenRope2;
    public GameObject brokenRope3;
    public GameObject brokenRope4;
    public GameObject brokenRope5;
    public GameObject brokenRope6;
    public GameObject brokenRope7;
    public GameObject brokenRope8;
    public GameObject brokenRope9;

    

    private void Start()
    {
    }


    //OnTriggerEnter method, activates when a collider enters another (requires one object to have a rigidbody)
    public void OnTriggerEnter(Collider other)
    {
        //Checks if the collided object has the rope tag attached to it, and only then will the statement run
        if (other.CompareTag ("Rope"))
        {           
            //Destroys the object to get it out of the scene and become inactive to make sure the update method can run.
            
            Destroy(other.gameObject);
            //Instantiates a prefab at the location and position of the collided object.
            Instantiate(ropeModel, transform.position, transform.rotation);

        } 
    }
    private void Update()
    {
        //And if statement to see if all ropes have been destroyed, if so, this code will run
        if (brokenRope == false && brokenRope1 == false && brokenRope2 == false && brokenRope3 == false && brokenRope4 == false
                    && brokenRope5 == false && brokenRope6 == false && brokenRope7 == false && brokenRope8 == false && brokenRope9 == false)
        {
            
            //Sets the current Palisade model inactive and activates the one with a rigidbody.
            Destroy(palisadeParent);
            palisadeBroken.SetActive(true);
        }
    }*/
}
