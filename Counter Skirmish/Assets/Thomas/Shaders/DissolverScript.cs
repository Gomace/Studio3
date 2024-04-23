using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolverScript : MonoBehaviour
{
    // The duration of how long it takes for the lerp to go from 0 to 1 
    public float textureChangeDuration = 5;
    // The lerp amount, starting at 0.
    public float textureStrength;

    // A function that tells Unity to start running the code.
    public void StartTextureChange()
    {
        StartCoroutine(textureChange());
    }

    // A Coroutine runs across multiple frames or until a contition is met
    //Example: frame 1, frame 2, frame 3, condition is met

    //The update function will run this code every single frame, which will cost a lot of performance.
    //The coroutine can run the same code across multiple frames, allowing for better performance.
    public IEnumerator textureChange()
    {
        // Since the texture change is running over a period of time, we need to keep track of
        // how long it takes.
        float elapsedTime = 0;

        // I access the renderer component of a gameObject, which holds the material. In this case
        // I am getting the shader material, due to it being assigned to the gameobject that has this script (The palisade poles)
        Material dissolveMaterial = GetComponent<Renderer>().material;

        //I want this to run until the time I have set is over.
        // While the elapsed time is less than the dissolveDuration, run the following code:
        while ( elapsedTime < textureChangeDuration)
        {
            // Keeps track of how much time has passed. By writing += Time.deltaTime we count up by seconds.
            elapsedTime += Time.deltaTime;

            //Mathf.Lerp takes three values (start value, end value and time).
            // We set elapsedTime over dissolve duration so that no matter how long the duration is
            //the lerp will always happen at the same rate. This means you can have as long or short duration as you want
            //and the effect will always be the same, it just takes longer or shorter.
            textureStrength = Mathf.Lerp(0, 1, elapsedTime / textureChangeDuration);

            // Here I access the dissolve strength property in my shader and set it as a float value. The SetFloat takes two values
            // (the reference we are trying to change, the number we want it to be.
            dissolveMaterial.SetFloat("_TextureStrength", textureStrength);

            // Rather than running this code every frame, I use a yield return null; statement to tell Unity when I want it to run
            // and when it should be paused saving performance. This tells Unity when to continue in the next frame.
            yield return null;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Fire"))
        {
            // If the Palisade collides with the tag "Fire", run the coroutine.
            StartTextureChange();

        }
    }

}
