using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshEnabler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        foreach (Renderer renderer in gameObject.GetComponentsInChildren(typeof(Renderer)))
        {
            renderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
