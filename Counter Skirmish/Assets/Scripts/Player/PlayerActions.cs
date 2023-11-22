using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RectTransform uiOverlay;
    private CameraController camCont;
    
    private GameObject roster;
    
    private LayerMask useLayer = 1 << 6;

    private void Awake()
    {
        if (!gameManager || !uiOverlay)
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

    #region Actions
    private void OnCameraZoom(InputValue value) => camCont.CameraZoom(value.Get<float>());
    
    private void OnMenu() => gameManager.PauseMenu();

    private void OnRoster() => roster.SetActive(!roster.activeSelf);

    public void OnAbilityQ(InputValue value)
    {
        
    }
    public void OnAbilityW(InputValue value)
    {
        
    }
    public void OnAbilityE(InputValue value)
    {
        
    }
    public void OnAbilityR(InputValue value)
    {
        
    }
    #endregion Actions
}