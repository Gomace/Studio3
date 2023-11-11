using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolverScriptForPR : MonoBehaviour
{
    // The duration of how long it takes for the lerp to go from 0 to 1 
    public float dissolveDuration = 10;
    // The lerp amount, starting at 0.
    public float dissolveStrength;
    //A timer that checks how 
    //public float timerCheck = 0;

    //public bool onOff = false;

    //public Color startColor;
    //public Color endColor;

    //Cause of the issues
    //public Material startMaterial;
    //public Material endMaterial;

    /*Rigidbody rb;
    public Rigidbody cubeRigidBody;*/

    private void Start()
    {
        /*rb = this.GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = true;*/


        //Cause of the issues
        //rend = GetComponent<Renderer>();
        //rend.material = startMaterial;
    }

    public void StartDissolver()
    {
        StartCoroutine(Dissolver());
    }

    public IEnumerator Dissolver()
    {
        float elapsedTime = 0;

        Material dissolveMaterial = GetComponent<Renderer>().material;
        //Color lerpedColor;

        //Material lerpedMaterial;

        while ( elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;

            //timerCheck /= elapsedTime;

            dissolveStrength = Mathf.Lerp(0, 1, elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);

            //lerpedColor = Color.Lerp(startColor, endColor, dissolveStrength);

            //dissolveMaterial.SetColor("_BaseColor", lerpedColor);


            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            StartDissolver();

        }
    }

}
