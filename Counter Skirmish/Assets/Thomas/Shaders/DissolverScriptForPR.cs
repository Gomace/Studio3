using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolverScriptForPR : MonoBehaviour
{
    // The duration of how long it takes for the lerp to go from 0 to 1 
    public float textureChangeDuration = 10;
    // The lerp amount, starting at 0.
    public float textureStrength;

    public void StartTextureChange()
    {
        StartCoroutine(textureChange());
    }

    public IEnumerator textureChange()
    {
        float elapsedTime = 0;

        Material dissolveMaterial = GetComponent<Renderer>().material;
        //Color lerpedColor;

        //Material lerpedMaterial;

        while ( elapsedTime < textureChangeDuration)
        {
            elapsedTime += Time.deltaTime;

            //timerCheck /= elapsedTime;

            textureStrength = Mathf.Lerp(0, 1, elapsedTime / textureChangeDuration);
            dissolveMaterial.SetFloat("_TextureStrength", textureStrength);

            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            //Dissolver();

        }
    }

}
