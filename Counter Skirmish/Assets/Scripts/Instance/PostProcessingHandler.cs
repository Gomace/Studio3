using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingHandler : MonoBehaviour
{

    [SerializeField] GameObject stealthPostProcessing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stealthPostProcessing.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            stealthPostProcessing.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stealthPostProcessing.SetActive(false);
    }

}
