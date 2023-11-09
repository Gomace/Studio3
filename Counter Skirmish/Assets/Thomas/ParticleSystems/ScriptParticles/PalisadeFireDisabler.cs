using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeFireDisabler : MonoBehaviour
{

    public float timer = 5.0f;

    void Update()
    {    
        if (this.gameObject == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Palisade"))
        {
            Debug.Log("StayingPalisade");   
        }
        if (!other.CompareTag("Palisade"))
        {
            Destroy(this.gameObject);
        }
        
    }
    

}
