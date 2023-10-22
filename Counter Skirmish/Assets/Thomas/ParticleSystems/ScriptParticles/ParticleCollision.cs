using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{

    [SerializeField] GameObject decalPrefab;
    public bool decalSpawned = false;
    public float countdown = 2.0f;
    public float killTimer = 5.0f;
    public bool timerStart = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (decalSpawned == true)
        {
            countdown -= Time.deltaTime;
        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle has collided");

        if (countdown < 0.0f)
        {
            Debug.Log("Particle has IFFED");
            Instantiate(decalPrefab, transform.position, transform.rotation);
            countdown = 2.0f;
        }


        //Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.back);
    }

}
