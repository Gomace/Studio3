using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private Image _barFill;
    [SerializeField] private TMP_Text _progressTxt, _loadingTxt;
    [SerializeField] private RectTransform _loadingIndicator;
    
    [Header("Are you testing the loading screen?")]
    [SerializeField] private bool _testing = false;
    
    private float _delay = 0, _dots = 0;
    
    private void Awake()
    {
        if (!_screen)
            Debug.Log("Loading Screen component missing from " + gameObject.name);
        if (!_barFill)
            Debug.Log("Slider component missing from " + gameObject.name);
        if (!_progressTxt)
            Debug.Log("Progress Text component missing from " + gameObject.name);
        
        _loadingTxt.text = "Loading";
    }
    
    private void Update()
    {
        if (_testing)
            LoadTesting();
    }
    
    public void Quit() => Application.Quit();
    
    public void LoadScene(string sceneName) => StartCoroutine(Loading(sceneName));
    
    private IEnumerator Loading(string sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);

        if (!_screen)
            yield break;
        
        _screen.SetActive(true);
        
        _loadingTxt.text = "Loading";
        _delay = _dots = 0;
        while (!loading.isDone)
        {
            float progress = Mathf.Clamp01(loading.progress / 0.9f);
            Debug.Log(progress);
            
            if (_barFill)
                _barFill.fillAmount = progress;
            if (_progressTxt)
                _progressTxt.text = (int)(progress * 100f) + "%";
            
            if (_loadingIndicator)
                _loadingIndicator.Rotate(Vector3.forward, 180f * Time.deltaTime);
            
            if (_delay > 1f)
            {
                if (_dots < 3f)
                {
                    _loadingTxt.text += ".";
                    _dots++;
                }
                else
                {
                    _loadingTxt.text = "Loading";
                    _dots = 0;
                }
                _delay = 0;
            }
            _delay += Time.deltaTime;

            yield return null;
        }
    }

    private void LoadTesting()
    {
        _delay = Mathf.Clamp01(_delay);
        Debug.Log(_delay);
            
        if (_barFill)
            _barFill.fillAmount = _delay;

        float progress = _delay * 100f;
        if (_progressTxt)
            _progressTxt.text = (int)progress + "%";
        
        if (_loadingIndicator)
            _loadingIndicator.Rotate(Vector3.forward, 180f * Time.deltaTime);

        if (_delay >= 1f)
        {
            if (_dots < 3f)
            {
                _loadingTxt.text += ".";
                _dots++;
            }
            else
            {
                _loadingTxt.text = "Loading";
                _dots = 0;
            }
            _delay = 0;
        }
        _delay += Time.deltaTime;
    }
}
