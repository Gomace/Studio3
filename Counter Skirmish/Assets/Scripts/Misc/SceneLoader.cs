using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image bar;
    [SerializeField] private TextMeshProUGUI progressText, loadingText;
    [SerializeField] private RectTransform loadingIndicator;
    
    [Header("Are you testing the loading screen?")]
    [SerializeField] private bool testing = false;
    
    float delay = 0, dots = 0;
    
    private void Awake()
    {
        if (!loadingScreen)
            Debug.Log("Loading Screen component missing from " + gameObject.name);
        if (!bar)
            Debug.Log("Slider component missing from " + gameObject.name);
        if (!progressText)
            Debug.Log("Progress Text component missing from " + gameObject.name);
        
        loadingText.text = "Loading";
    }
    
    private void Update()
    {
        if (!testing)
            return;
        
        delay = Mathf.Clamp01(delay);
        Debug.Log(delay);
            
        if (bar)
            bar.fillAmount = delay;

        float progress = delay * 100f;
        if (progressText)
            progressText.text = (int)progress + "%";
        
        if (loadingIndicator)
            loadingIndicator.Rotate(Vector3.forward, 180f * Time.deltaTime);

        if (delay >= 1f)
        {
            if (dots < 3f)
            {
                loadingText.text += ".";
                dots++;
            }
            else
            {
                loadingText.text = "Loading";
                dots = 0;
            }
            delay = 0;
        }
        delay += Time.deltaTime;
    }
    
    public void LoadScene(string sceneName) => StartCoroutine(LoadingScreen(sceneName));
    
    private IEnumerator LoadingScreen(string sceName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceName);

        if (!loadingScreen)
            yield break;
        
        loadingScreen.SetActive(true);
        
        loadingText.text = "Loading";
        delay = dots = 0;
        while (!loading.isDone)
        {
            float progress = Mathf.Clamp01(loading.progress / 0.9f);
            Debug.Log(progress);
            
            if (bar)
                bar.fillAmount = progress;
            if (progressText)
                progressText.text = progress * 100f + "%";
            
            if (loadingIndicator)
                loadingIndicator.Rotate(Vector3.forward, 180f * Time.deltaTime);
            
            if (delay > 1f)
            {
                if (dots < 3f)
                {
                    loadingText.text += ".";
                    dots++;
                }
                else
                {
                    loadingText.text = "Loading";
                    dots = 0;
                }
                delay = 0;
            }
            delay += Time.deltaTime;

            yield return null;
        }
    }
}
