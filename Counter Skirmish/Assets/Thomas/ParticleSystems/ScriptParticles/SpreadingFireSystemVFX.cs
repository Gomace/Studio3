using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingFireSystemVFX : MonoBehaviour
{

    public bool onFire = false;

    private float timer = 1.5f;
    private bool timerOnOff = false;

    BoxCollider myCollider;



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

        if (timer <= 0)
        {
            BoxCollider myCollider = GetComponent<BoxCollider>();
            myCollider.enabled = true;   
        }
        if (timer > 0)
            onFire = true;

        if (timer < 0)
        {
            onFire = false;         
        }            
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.GetChild(0).gameObject.activeInHierarchy)
            onFire = true;

        if (other.CompareTag("Tool") && onFire == true && !other.transform.GetChild(0).gameObject.activeInHierarchy && timer <= 0)
        {
            Debug.Log("HelloWorld");

            other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.GetChild(1).gameObject.SetActive(true);

            BoxCollider myCollider = GetComponent<BoxCollider>();
            myCollider.enabled = true;

            //transform.gameObject.SetActive(false);
            //other.transform.gameObject.SetActive(false);

        }

        if (timer <= 0)
            
        if (other.transform.GetChild(0).gameObject.activeInHierarchy && timer <= -1)
        {
            onFire = false;
                

        }

        if (other.transform.GetChild(0).gameObject.activeInHierarchy && timer > 0)
        {
            onFire = true;
        }
    }




    private void Start()
    {
        Debug.Log("SpreadingFireSystemVFXStarted");
    }


    /*private void OnTriggerStay(Collider other)
{
    if (other.CompareTag("Tool") && onFire == true && !other.transform.GetChild(0).gameObject.activeInHierarchy && timer <= 0)
    {
        Debug.Log("HelloWorld");

        other.transform.GetChild(0).gameObject.SetActive(true);


        //transform.gameObject.SetActive(false);
        //other.transform.gameObject.SetActive(false);

    }

    if (other.transform.GetChild(0).gameObject.activeInHierarchy && timer <= -1)
    {
        onFire = false;

    }

    if (other.transform.GetChild(0).gameObject.activeInHierarchy && timer > 0)
    {
        onFire = true;
    }
}*/

    /*if (other.CompareTag("Tool") && onFire == true && !other.transform.GetChild(0).gameObject.activeInHierarchy && timerOnOff == true && timer == 0.1f)
{
    Debug.Log("HelloWorld");
    Debug.Log(transform.childCount);


    other.transform.GetChild(0).gameObject.SetActive(true);
    Debug.Log(transform.GetChild(0).name);

    //transform.gameObject.SetActive(false);
    //other.transform.gameObject.SetActive(false);

}

if (other.transform.GetChild(0).gameObject.activeInHierarchy)
{
    onFire = true;
}*/

}
