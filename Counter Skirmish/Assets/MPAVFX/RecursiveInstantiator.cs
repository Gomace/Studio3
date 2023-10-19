using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveInstantiator : MonoBehaviour
{
    public int recurse = 5;
    void Start()
    {
        recurse -= 1;
        if (recurse > 0)
        {
            var copy = Instantiate(gameObject);
            var recursive = copy.GetComponent<RecursiveInstantiator>();
            SendMessage("Generated");
        }
    }
}
