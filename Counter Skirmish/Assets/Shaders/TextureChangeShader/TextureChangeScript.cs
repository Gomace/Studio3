using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChangeScript : MonoBehaviour
{

    

    public float textureChangeDuration = 5;
    public float textureStrength;

    //public GameObject cubeTest;

    public void StartTextureChange()
    {
        StartCoroutine(textureChange());
    }

    public IEnumerator textureChange()
    {
        float elapsedTime = 0;

        Material dissolveMaterial = GetComponent<Renderer>().material;

        while ( elapsedTime < textureChangeDuration)
        {
            elapsedTime += Time.deltaTime;

            textureStrength = Mathf.Lerp(0, 1, elapsedTime / textureChangeDuration);

            dissolveMaterial.SetFloat("_TextureStrength", textureStrength);

            yield return null;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Fire"))
        {
            StartTextureChange();
        }
    }

    private void Update()
    {
        if (textureStrength == 1.0f)
        {
            Destroy(this.gameObject);
        }
    }

}
