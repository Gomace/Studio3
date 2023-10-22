using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBroken : MonoBehaviour
{

    [SerializeField] GameObject ropeModel;

    public GameObject[] ropesToBeBroken;

    //Compare tag method learned from: https://www.youtube.com/shorts/r7H1hmiCdWU
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KollidertDa");
        if (other.CompareTag ("Rope"))
        {
            Debug.Log("ItHasInstantiated");
            other.gameObject.SetActive(false);
            Instantiate(ropeModel, transform.position, transform.rotation);
            
        } 
    }
}
