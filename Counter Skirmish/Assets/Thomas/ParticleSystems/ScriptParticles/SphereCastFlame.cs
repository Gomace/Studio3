using System.Collections;
using Unity.Jobs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphereCastFlame : MonoBehaviour
{
    //Makes me able to change the radius in-engine
    [SerializeField] float radius;
    //Makes me able to change the max distance of the ray in-engine
    [SerializeField] float maxDistance;
    //Makes me able to change the layers it should collide with or ignore in-engine
    [SerializeField] LayerMask layerMask;
    //Lets me manipulate the fire particle system within the script
    [SerializeField] GameObject FirePS;
    //A bool I will use to turn the particle system on and off to test the raycast
    [SerializeField] bool FirePSOn;
   /*Gets information back from a raycast, in this example I will use the word hit while coding
   to tell the raycast that it has hit something.*/
    RaycastHit hit;

    [SerializeField] GameObject decalPrefab;

    [SerializeField] bool decalOn;
    [SerializeField] ParticleSystem flamePS;

    [SerializeField] float decalTimer;

    public bool timerCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        decalOn = false;
        decalTimer = 0.1f;
    }
    void OnFireButton(InputValue value)
    {
        FirePSOn = true;
    }

    void OnFireOff(InputValue value)
    {
        FirePSOn = false;
    }

    // Update is called once per frame
    void Update()
    {

        

        if (timerCheck == true)
            decalTimer -= Time.deltaTime;

        if (FirePSOn == false)
        {
            timerCheck = false;
            decalTimer = 2.0f;
        }
        //If the fire particle boolean is on,
        //the fire PS will start emitting and
        //run the RayHasCast function
        if (FirePSOn == true)
        {
            timerCheck = true;
            decalOn = true;
            FirePS.SetActive(true);
            
            if (decalTimer < 0)
            RayHasCast();

            //A Debug Log to test if the script
            //has reached this if statement
            Debug.Log("Sphere has cast");
        }
        //If the if statement above is not true
        //(boolean is off) the fire PS will stop emitting    
        else
        {
            FirePS.SetActive(false);
        }
    }

    void RayHasCast()
    {
        
        Debug.Log("Void entered");
        /* The spherecast takes information on where it needs to start, the radius of the sphere, the direction it point towards
         * An out variable that tells the ray cast that it has hit something, the max distance (How far it will go) and what 
         * layer mask it should hit to get a reaction. The public LayerMask I set up above is the layer that the raycast will react
         * to, I can put a ~ before the layermask to invert it, meaning that it will answer to every layer except the one mentioned
         * in the script.*/

        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, maxDistance) && decalOn == true && hit.transform.CompareTag("Tool"))
        {
            //Checks if the raycast has hit the collider of the gameobject or not.

            hit.collider.GetComponent<GameObject>();
            hit.collider.transform.GetChild(0).gameObject.SetActive(true);
            ///hit.collider.transform.GetChild(1).gameObject.SetActive(true);
            //hit.collider.gameObject.SetActive();


            Debug.Log("ObectHit");

            //Get me the difference between the angle of the object that we hit (The normal is a line going perpendicular away from the object)
            //We are getting the difference between Vector3.Back. and hit.normal so that it spawns in the corret angle.

            //Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.back, hit.normal);
            //Instantiate(decalPrefab, hit.point, spawnRotation);

            decalTimer = 0.1f;
        }
    }
}
        
            

        
    

