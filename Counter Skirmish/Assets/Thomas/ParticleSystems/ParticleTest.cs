using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParticleTest : MonoBehaviour
{
    public bool fireOn;
    private ParticleSystem thisSystem;

    

    // Start is called before the first frame update
    void Start()
    {
        thisSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Q"))
        {
            fireOn = !fireOn;
            OnOffFire();
        }

        if (fireOn)
        {
            thisSystem.Play();
        }

        if (!fireOn)
        {
            thisSystem.Stop();
        }
    }

    private void OnOffFire()
    {
        if (fireOn)
        {
            thisSystem.Play();
        }

        if (!fireOn)
        {
            thisSystem.Stop();
        }
    }
}
