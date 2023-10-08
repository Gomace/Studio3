using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Global global;
    [SerializeField] private RectTransform uiOverlay;
    private CameraController camCont;
    
    private GameObject roster;
    
    private LayerMask useLayer = 1 << 6;

    private void Awake()
    {
        if (!global || !uiOverlay)
            Debug.Log("Missing inspector drag & drop reference. Please help :[");

        if (Camera.main != null)
            camCont = Camera.main.GetComponent<CameraController>();
        
        if (uiOverlay.GetChild(1) != null)
            roster = uiOverlay.GetChild(1).gameObject;
    }
    
    /* void Update()
    {
        if (global.disabled)
            return;
    }*/

    private void OnCameraZoom(InputValue value) => camCont.CameraZoom(value.Get<float>());
    
    private void OnMenu() => global.PauseMenu();

    private void OnRoster() => roster.SetActive(!roster.activeSelf);

    public void OnQ(InputValue value)
    {
        
    }
    public void OnW(InputValue value)
    {
        
    }
    public void OnE(InputValue value)
    {
        
    }
    public void OnR(InputValue value)
    {
        
    }
}