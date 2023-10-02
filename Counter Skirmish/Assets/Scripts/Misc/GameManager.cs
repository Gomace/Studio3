using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PlayerInput _playerInput;
    
    public void EnterGate()
    {
        _playerInput.SwitchCurrentActionMap("Menu");
    }
}