using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeFireDisabler : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody palisadeRigidbody;

    private GameObject palisade;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Palisade"))
        {
            this.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            

            Destroy(this.transform.parent.GetComponent<GameObject>());
        }
    }

}
