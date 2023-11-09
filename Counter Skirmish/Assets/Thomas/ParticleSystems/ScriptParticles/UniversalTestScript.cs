using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!this.CompareTag("Palisade"))
        {
            Debug.Log("PalisadeColided");
            Debug.Log(other.name);

            this.gameObject.SetActive(false);

            
        }
    }

}
