using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolverScript : MonoBehaviour
{

    public float dissolveDuration = 2;
    public float dissolveStrength;
    public float timerCheck = 0;

    public float transitionDuration = 5;
    public float transitionStrength;

    public bool onOff = false;

    public Color startColor;
    public Color endColor;

    public Material startMaterial;
    public Material endMaterial;

    Renderer rend;

    Rigidbody rb;
    public Rigidbody cubeRigidBody;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.isKinematic = true;

        rend = GetComponent<Renderer>();
        rend.material = startMaterial;
    }

    public void StartDissolver()
    {
        StartCoroutine(dissolver());
    }

    public IEnumerator dissolver()
    {
        float elapsedTime = 0;

        Material dissolveMaterial = GetComponent<Renderer>().material;
        Color lerpedColor;

        Material lerpedMaterial;

        while ( elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;

            timerCheck /= elapsedTime;

            dissolveStrength = Mathf.Lerp(0, 1, elapsedTime / dissolveDuration);
            dissolveMaterial.SetFloat("_DissolveStrength", dissolveStrength);

            lerpedColor = Color.Lerp(startColor, endColor, dissolveStrength);

            transitionStrength = Mathf.Lerp(0, 1, elapsedTime / transitionDuration);
            dissolveMaterial.SetFloat("_DiffuseTransition", transitionStrength);

            dissolveMaterial.SetColor("_BaseColor", lerpedColor);


            yield return null;
        }

        //Destroy(gameObject);
        //Destroy(dissolveMaterial);

    }

    private void Update()
    {



        if (timerCheck == 1)
        {
            Debug.Log("TimerIsChecked");
            
        }
            

        if (dissolveStrength == 1f)
        {
            Debug.Log("Dissolved");
            rb.useGravity = true;
            rb.isKinematic = false;
            Debug.Log("RigidChanged");
        }

        if (onOff == true)
            StartDissolver();


        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            onOff = true;

        }
    }

}
