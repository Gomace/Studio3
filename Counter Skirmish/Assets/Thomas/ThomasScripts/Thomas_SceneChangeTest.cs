using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thomas_SceneChangeTest : MonoBehaviour
{

    public string sceneName;

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);

    }
}
