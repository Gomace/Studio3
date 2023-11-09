using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadingParentDisabler : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (this.gameObject.CompareTag("Palisade"))
        {

            this.gameObject.SetActive(true);
            Debug.Log("StayingPalisade");

        }

        if (!this.gameObject.CompareTag("Palisade"))
        {
            Debug.Log("NotStayingAnymore");
            Destroy(this.gameObject);
        }
    }
}
