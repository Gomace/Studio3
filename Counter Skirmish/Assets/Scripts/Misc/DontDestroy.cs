using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class DontDestroy : MonoBehaviour
{
    // public static SceneLoader Instance { get; private set; }

    [HideInInspector] public string ObjectID { get; set; }

    /*private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        }
    }*/
    
    private void Awake() => ObjectID = name + transform.position.ToString()/* + transform.eulerAngles.ToString()*/;

    private void Start()
    {
        foreach (DontDestroy script in Object.FindObjectsOfType<DontDestroy>())
        {
            if (script != this)
            {
                if (script.ObjectID == ObjectID)
                    Destroy(gameObject);
            }
        }
        
        /*for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
             if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
             {
                 if (Object.FindObjectsOfType<DontDestroy>()[i].objectID == objectID)
                     Destroy(gameObject);
             }
        }*/
        DontDestroyOnLoad(gameObject);
    }
}
