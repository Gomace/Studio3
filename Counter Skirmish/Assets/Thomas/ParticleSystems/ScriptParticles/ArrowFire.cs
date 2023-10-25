using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFire : MonoBehaviour
{
    
    public bool hasCaughtFire;
    public GameObject arrowFire;
    
    public GameObject decalPrefab;

    public float radius;
    public LayerMask layerMask;
    public float maxDistance;
    RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
        hasCaughtFire = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            hasCaughtFire = true;
            arrowFire.SetActive(true);
        }   
    }

    void Update()
    {
        if (hasCaughtFire == true && (Physics.SphereCast(transform.position, radius, 
            transform.forward, out hit, maxDistance, layerMask)))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.back, hit.normal);
            Instantiate(decalPrefab, hit.point, spawnRotation);
        }
    }
}
