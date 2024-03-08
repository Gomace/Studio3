using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InstanceUnit))]
[RequireComponent(typeof(PlayerMovement))]
public class InstanceActions : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GameObject _roster, _huntScreen, _settings;

    private InstanceUnit _unit;
    private PlayerMovement _movement;
    private CameraController _camCont;
    private LayerMask _useLayer = 1 << 6;

    private int _curAbility;
    private bool _modifier = false;

    private void Awake()
    {
        if (!_settings)
            Debug.Log($"Missing inspector drag & drop reference in {name} on {gameObject.name}. Please help :[");

        if (Camera.main != null && Camera.main.GetComponent<CameraController>())
            _camCont = Camera.main.GetComponent<CameraController>();
        else
            Debug.Log("Put CameraController script on the Camera :|");

        _unit = GetComponent<InstanceUnit>();
        _movement = GetComponent<PlayerMovement>();
    }

    #region Actions
    private void OnCameraZoom(InputValue value) => _camCont.CameraZoom(value.Get<float>());

    private void OnUse()
    {
        if (!_movement.Indicating)
            return;
        
        Debug.Log("Turning off Indicator");
        _movement.Indicating = false;
        _unit.Creature.CastAbility(_curAbility, false);
    }
    
    private void OnIndicatorCast(InputValue value) => _modifier = value.isPressed;

    private void OnAbilityQ()
    {
        Debug.Log("You Q in");
        _curAbility = 0;
        _unit.Creature.CastAbility(0, _modifier);
    }

    private void OnAbilityW()
    {
        Debug.Log("You W in");
        _curAbility = 1;
        _unit.Creature.CastAbility(1, _modifier);
    }

    private void OnAbilityE()
    {
        _curAbility = 2;
        _unit.Creature.CastAbility(2, _modifier);
    }

    private void OnAbilityR()
    {
        _curAbility = 3;
        _unit.Creature.CastAbility(3, _modifier);
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