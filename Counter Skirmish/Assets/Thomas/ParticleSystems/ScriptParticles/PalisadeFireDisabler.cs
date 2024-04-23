using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeFireDisabler : MonoBehaviour
{

    public float timerDisable = 2.0f;

    private void Update()
    {
        timerDisable -= Time.deltaTime;

        if (timerDisable < 0)
        {
            Debug.Log("Timer is zero");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Palisade"))
        {
            this.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            

            Destroy(this.transform.parent.GetComponent<GameObject>());
        }
    }

}
