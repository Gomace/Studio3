using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFire : MonoBehaviour
{

    public bool hasCaughtFire;
    public GameObject arrow;
    public GameObject arrowFire;
    public ParticleSystem arrowFirePSTest;
    public GameObject fireCollider;

    public GameObject decalPrefab;

    RaycastHit hit;
    Ray beam;
    // Start is called before the first frame update
    void Start()
    {
        hasCaughtFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCaughtFire == true && Physics.Raycast(beam, out hit))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.back, hit.normal);
            Instantiate(decalPrefab, hit.point, spawnRotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ColliderEntered");
        if (other.CompareTag("Fire"))
        {
            
            hasCaughtFire = true;
            Debug.Log("FireHasCaught");
            arrowFire.SetActive(true);
        }
        
    }
}
