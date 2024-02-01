using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneLoader))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    
    [HideInInspector] public bool Disable { get; set; }

    public GameObject Player { get; set; }
    public GameObject MenuScreen { get; set; }
    public bool StartPaused { get; set; }

    private void Awake() => _sceneLoader = GetComponent<SceneLoader>();

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        
        if (StartPaused)
             Pause();
    }

    /*private void Update()
    {
        if (!MenuScreen.activeSelf)
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
        if (!MenuScreen.activeSelf)
            Pause();
        else
            Play();
    }

    public void Disabled(bool state) => Disable = state;

    #region Menu
    private void Pause()
    {
        MenuToggle(true);
        Time.timeScale = 0;
        Disable = true;
    }
    public void Play()
    {
        MenuToggle(false);
        Time.timeScale = 1;
        Disable = false;
    }

    public void Restart()
    {
        if (_sceneLoader)
            _sceneLoader.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() => Application.Quit();
    
    private void MenuToggle(bool state)
    {
        MenuScreen.SetActive(state);
        
        if (state)
        {
            MenuScreen.transform.GetChild(0).gameObject.SetActive(true);
            MenuScreen.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    #endregion Menu
}