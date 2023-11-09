using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    
    private void Awake()
    {
        if (GetComponent<SceneLoader>())
            _sceneLoader = GetComponent<SceneLoader>();
    }
    
    public void Quit() => Application.Quit();
}