using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalisadeFireDisabler : MonoBehaviour
{

    public float timer = 5.0f;

    public GameObject palisade;
    public GameObject palisade1;
    public GameObject palisade2;
    public GameObject palisade3;
    public GameObject palisade4;
    public GameObject palisade5;
    public GameObject palisade6;
    public GameObject palisade7;
    public GameObject palisade8;
    public GameObject palisade9;

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



        CapsuleCollider palisadeCollider = palisade.GetComponent<CapsuleCollider>();
        CapsuleCollider palisadeCollider1 = palisade.GetComponent<CapsuleCollider>();
        CapsuleCollider palisadeCollider2 = palisade.GetComponent<CapsuleCollider>();
        CapsuleCollider palisadeCollider3 = palisade.GetComponent<CapsuleCollider>();
        CapsuleCollider palisadeCollider4 = palisade.GetComponent<CapsuleCollider>();
        CapsuleCollider palisadeCollider5 = palisade.GetComponent<CapsuleCollider>();
        CapsuleCollider palisadeCollider6 = palisade.GetComponent<CapsuleCollider>();
        CapsuleCollider palisadeCollider7 = palisade.GetComponent<CapsuleCollider>();
        CapsuleCollider palisadeCollider8 = palisade.GetComponent<CapsuleCollider>();
        CapsuleCollider palisadeCollider9 = palisade.GetComponent<CapsuleCollider>();


        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Palisade")
        {

            Debug.Log("DoNothing");
            //Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("ElsedYo");
            Destroy(this.gameObject);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Palisade"))
        {
            Debug.Log("StayingPalisade");   
        }
        if (!other.CompareTag("Palisade"))
        {
            Destroy(this.gameObject);
        }
        
    }*/


}
