using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    //Array that allows us  to use any number of materials
    public Material[] material;

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        //We make the renderer equal the game objects renderer
        rend = GetComponent<Renderer>();
        //This is to make sure it is enabled when the game starts
        rend.enabled = true;
        //It will access the first material in the array
        rend.sharedMaterial = material[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        { 
            //If there is collision, the material will change to the second option in the array.
            rend.sharedMaterial = material[1];
        }
    }
}
