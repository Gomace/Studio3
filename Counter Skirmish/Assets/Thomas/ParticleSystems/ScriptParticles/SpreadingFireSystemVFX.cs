using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingFireSystemVFX : MonoBehaviour
{
    // Checks if the Palisade is on fire or not.
    public bool onFire = false;

    // Timer between each grid activation
    private float timer = 1.5f;
    // Bool to turn on and off the timer
    private bool timerOnOff = false;

    private void Update()
    {
        // If the Palisade is on fire, start the timer
        if (onFire == true)
        {
            timerOnOff = true;
        }

        // If the Palisade is on fire and the timer is on, take the float and count downwards
        if (onFire == true && timerOnOff == true)
        {
            timer -= Time.deltaTime;
        }

        // If the timer is less than 0, get the box collider of this object and activate it.
        if (timer <= 0)
        {
            BoxCollider myCollider = GetComponent<BoxCollider>();
            myCollider.enabled = true;   
        }

        //If the timer is more than 0, it will be on fire. 
        //The reason I have it like this is that the game object starts inactive, and when it
        //activates it will spread its fire. Since the timer has a higher number than 0, it will
        //activate the bool as soon as the gameobject is activated.
        if (timer > 0)
            onFire = true;

        //If the timer is less than 0 it will not be on fire anymore, and the flame will die out.
        if (timer < 0)
        {
            onFire = false;         
        }            
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.CompareTag("Tool") && onFire == true && !other.transform.GetChild(0).gameObject.activeInHierarchy && timer <= 0)
        {
            // If the object I collide with has the tag "Tool" and I am on fire and the first child of the other object is not active and the timer is less than 0 then:
            // Activate the first and second child of the object I collided with
            other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.GetChild(1).gameObject.SetActive(true);

            BoxCollider child0Collider = transform.GetChild(0).gameObject.GetComponent<BoxCollider>();
            child0Collider.enabled = true;

            BoxCollider child1Collider = transform.GetChild(1).gameObject.GetComponent<BoxCollider>();
            child1Collider.enabled = true;

            // Get my collider and enable it
            BoxCollider myCollider = GetComponent<BoxCollider>();
            myCollider.enabled = true;
        }

        
        if (other.transform.GetChild(0).gameObject.activeInHierarchy)
            // If the child of the other object is active (Seen in the last If statement) then:
            // Set it on fire
            onFire = true;

        if (timer <= 0)
            
        if (other.transform.GetChild(0).gameObject.activeInHierarchy && timer <= -1)
        {
                // if the first child of the other object is active and the timer is less than or equal to -1
                // Remove the fire
                onFire = false;
        }

        if (other.transform.GetChild(0).gameObject.activeInHierarchy && timer > 0)
        {
            // If the child of the other object is active and is more than 0
            // set it on fire.
            onFire = true;
        }
    }
    /*private void OnTriggerStay(Collider other)
{
    if (other.CompareTag("Tool") && onFire == true && !other.transform.GetChild(0).gameObject.activeInHierarchy && timer <= 0)
    {
        Debug.Log("HelloWorld");

        other.transform.GetChild(0).gameObject.SetActive(true);


        //transform.gameObject.SetActive(false);
        //other.transform.gameObject.SetActive(false);

    }

    if (other.transform.GetChild(0).gameObject.activeInHierarchy && timer <= -1)
    {
        onFire = false;

    }

    if (other.transform.GetChild(0).gameObject.activeInHierarchy && timer > 0)
    {
        onFire = true;
    }
}*/

    /*if (other.CompareTag("Tool") && onFire == true && !other.transform.GetChild(0).gameObject.activeInHierarchy && timerOnOff == true && timer == 0.1f)
{
    Debug.Log("HelloWorld");
    Debug.Log(transform.childCount);


    other.transform.GetChild(0).gameObject.SetActive(true);
    Debug.Log(transform.GetChild(0).name);

    //transform.gameObject.SetActive(false);
    //other.transform.gameObject.SetActive(false);

}

if (other.transform.GetChild(0).gameObject.activeInHierarchy)
{
    onFire = true;
}*/

}
