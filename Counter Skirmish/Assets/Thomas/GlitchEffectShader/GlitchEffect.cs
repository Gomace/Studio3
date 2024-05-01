using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{

    public float glitchChance = .1f;

    private Renderer holoRender;
    private WaitForSeconds glitchLoopWait = new WaitForSeconds(.1f);
    private WaitForSeconds glitchDuration = new WaitForSeconds(.1f);

    private void Awake()
    {
        holoRender = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);

            if (glitchTest <= glitchChance)
            {
                StartCoroutine(Glitch ());
            }
            yield return glitchLoopWait;
        }
    }

    // Update is called once per frame
    IEnumerator Glitch()
    {
        glitchDuration = new WaitForSeconds(Random.Range(.05f, .25f));
        holoRender.material.SetFloat("_Amount", 1f);
        holoRender.material.SetFloat("_CutoutThresh", .29f);
        holoRender.material.SetFloat("_Amplitude", Random.Range(100, 250));
        holoRender.material.SetFloat("_Speed", Random.Range(1, 10));
        yield return glitchDuration;
        holoRender.material.SetFloat("Amount", 0f);
        holoRender.material.SetFloat("_CutoutTresh", 0f);
    }
}
