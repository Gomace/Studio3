using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player, menuScreen;
    [SerializeField] private SceneLoader _sceneLoader;
    
    public bool startPaused;
    [HideInInspector] public bool disabled;

    private void Awake()
    {
        if (GetComponent<SceneLoader>())
            _sceneLoader = GetComponent<SceneLoader>();
    }

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        
        if (startPaused)
             Pause();
    }
    
    /*void Update()
    {
        if (!menuScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Time.timeScale = 1;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                Time.timeScale = 0;
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                switch (Time.timeScale)
                {
                    case > 1:
                        Time.timeScale -= 1;
                        break;
                    case 1:
                        Time.timeScale = 0.5f;
                        break;
                    case 0.5f:
                        Time.timeScale = 0.25f;
                        break;
                    case 0.25f:
                        Time.timeScale = 0;
                        break;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                switch (Time.timeScale)
                {
                    case 0:
                        Time.timeScale = 0.25f;
                        break;
                    case 0.25f:
                        Time.timeScale = 0.5f;
                        break;
                    case 0.5f:
                        Time.timeScale = 1;
                        break;
                    case >= 1:
                        Time.timeScale += 1;
                        break;
                }
            }
        }
    }*/

    public void PauseMenu()
    {
        if (!menuScreen.activeSelf)
            Pause();
        else
            Play();
    }

    public void Disabled(bool state) => disabled = state;

    #region Menu
    private void Pause()
    {
        MenuToggle(true);
        Time.timeScale = 0;
        disabled = true;
    }
    public void Play()
    {
        MenuToggle(false);
        Time.timeScale = 1;
        disabled = false;
    }

    public void Restart()
    {
        if (_sceneLoader)
            _sceneLoader.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() => Application.Quit();
    
    private void MenuToggle(bool state)
    {
        menuScreen.SetActive(state);
        
        if (state)
        {
            menuScreen.transform.GetChild(0).gameObject.SetActive(true);
            menuScreen.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    #endregion Menu
}