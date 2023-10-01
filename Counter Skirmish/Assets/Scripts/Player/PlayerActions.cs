using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Global global;
    [SerializeField] private RectTransform uiOverlay;
    
    private GameObject roster;
    
    private LayerMask useLayer = 1 << 6;

    private void Awake()
    {
        if (!global || !uiOverlay)
            Debug.Log("Missing inspector drag & drop reference. Please help :[");

        roster = uiOverlay.GetChild(1).gameObject;
    }
    
    /*private void Update()
    {
        if (global.disabled)
            return;
    }*/

    void OnMenu()
    {
        global.PauseMenu();
    }

    private void OnRoster()
    {
        roster.SetActive(!roster.activeSelf);
    }

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