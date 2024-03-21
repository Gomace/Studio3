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
    // private LayerMask _useLayer = 1 << 6; TODO remove this later

    private GameObject _indicator;
    private int _curSlot;
    private bool _modifier = false;
    
    private int _CurAbility
    {
        get => _curSlot;
        set
        {
            _curSlot = value;
            CastAbility();
        }
    }

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
        
        _movement.Indicating = false;
        _unit.Creature.PerformAbility(_CurAbility, _movement.MouseLocation());
    }
    
    private void OnIndicatorCast(InputValue value) => _modifier = value.isPressed;

    private void OnAbilityQ() => _CurAbility = 0;

    private void OnAbilityW() => _CurAbility = 1;

    private void OnAbilityE() => _CurAbility = 2;

    private void OnAbilityR() => _CurAbility = 3;
    
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

    private void CastAbility()
    {
        if (_unit.Creature.Abilities[_CurAbility] == null) // Check if ability is equipped
            return;
        
        Ability curAbi = _unit.Creature.Abilities[_CurAbility];

        if (curAbi.Cooldown > 0)
            return;
        
        _indicator = _unit.Indicators[curAbi.Base.Indicator.name]; // Grab correct indicator
        _indicator.transform.localScale = curAbi.Base.IndHitBox; // Rescale indicator
        
        if (_modifier)
            _movement.ShowIndicator(_indicator); // Indicator follows mouse (player only)
        else
            _unit.Creature.PerformAbility(_CurAbility, _movement.MouseLocation());
    }
}