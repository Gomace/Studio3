using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallForPalisade : MonoBehaviour
{
    // Bool that checks if the arrow is on fire or not
    public bool hasCaughtFire;
    
    // The fire placed on the particle effect
    public GameObject arrowFire;
    
    // Start is called before the first frame update
    void Start()
    {
        // When the game starts the arrow will not be on fire
        hasCaughtFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flamethrower"))
        {
            // If the arrow collides with an object that has the tag "Fire"
            // the bool will be set to true and the particle system will activate
            hasCaughtFire = true;
            arrowFire.SetActive(true);
        }   

        if (other.CompareTag("Tool") && hasCaughtFire == true)
        {
            // If the arrow collides with an object that has the tag "Tool"
            // it will get the gameobject it collided with and activate its first child
            other.GetComponent<GameObject>();
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
