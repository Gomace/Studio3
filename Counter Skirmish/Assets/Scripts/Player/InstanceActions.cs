using UnityEngine;
using UnityEngine.InputSystem;

public class InstanceActions : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GameObject _roster, _huntScreen, _settings;

    private CameraController camCont;
    private LayerMask useLayer = 1 << 6;

    private void Awake()
    {
        if (!_settings)
            Debug.Log($"Missing inspector drag & drop reference in {gameObject.name}. Please help :[");

        if (Camera.main != null)
            camCont = Camera.main.GetComponent<CameraController>();
    }
    
    /* void Update()
    {
        if (global.disabled)
            return;
    }*/

    #region Actions
    private void OnCameraZoom(InputValue value) => camCont.CameraZoom(value.Get<float>());
    
    public void OnAbilityQ()
    {
        
    }
    public void OnAbilityW()
    {
        
    }
    public void OnAbilityE()
    {
        
    }
    public void OnAbilityR()
    {
        
    }

    private void OnItem1()
    {
        
    }
    private void OnItem2()
    {
        
    }
    private void OnItem3()
    {
        
    }
    private void OnItem4()
    {
        
    }
    private void OnItem5()
    {
        
    }
    private void OnItem6()
    {
        
    }
    
    private void OnRoster() => _roster.SetActive(!_roster.activeSelf);
    
    private void OnMenu() => _settings.SetActive(!_settings.activeSelf);

    private void OnHunt() => _huntScreen.SetActive(!_huntScreen.activeSelf);
    private void OnHub() => _sceneLoader.LoadScene("Hub");

    #endregion Actions
}