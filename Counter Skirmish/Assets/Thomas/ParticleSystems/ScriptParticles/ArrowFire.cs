using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFire : MonoBehaviour
{
    // Bool that checks if the arrow is on fire or not
    public bool hasCaughtFire;
    
    // The fire placed on the particle effect
    public GameObject arrowFire;



    // The decal prefab that will spawn when hitting the palisade
    /*public GameObject decalPrefab;
    public float radius;
    public LayerMask layerMask;
    public float maxDistance;
    RaycastHit hit;*/
    
    // Start is called before the first frame update
    void Start()
    {
        // When the game starts the arrow will not be on fire
        hasCaughtFire = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            // If the arrow collides with an object that has the tag "Fire"
            // the bool will be set to true and the particle system will activate
            hasCaughtFire = true;
            arrowFire.SetActive(true);
        }   

        if (other.CompareTag("Tool"))
        {
            // If the arrow collides with an object that has the tag "Tool"
            // it will get the gameobject it collided with and activate its first child
            other.GetComponent<GameObject>();
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    /*void Update()
    {
        if (hasCaughtFire == true && (Physics.SphereCast(transform.position, radius, 
            transform.forward, out hit, maxDistance, layerMask)))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.back, hit.normal);
            Instantiate(decalPrefab, hit.point, spawnRotation);
        }
    }*/
}
