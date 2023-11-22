using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRSpread : MonoBehaviour
{
    public bool onFire = false;

    public float timer = 5f;
    private bool timerOnOff = false;

   

    private void Update()
    {
        if (onFire == true)
        {
            timerOnOff = true;
        }

        if (onFire == true && timerOnOff == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer > 0)
            onFire = true;

        if (timer <= 0)
        {
            BoxCollider myCollider = GetComponent<BoxCollider>();
            myCollider.enabled = true;
            onFire = false;
        }

    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Tool") && onFire == true && 
            !other.transform.GetChild(0).gameObject.activeInHierarchy && timer <= 0)
        {
            other.transform.GetChild(0).gameObject.SetActive(true);

            other.transform.GetChild(1).gameObject.SetActive(true);
        }
     
        if (other.CompareTag("Tool") && other.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            onFire = true;  
        }
     }
}
