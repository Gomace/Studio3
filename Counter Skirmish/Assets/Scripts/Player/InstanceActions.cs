using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InstanceUnit))]
public class InstanceActions : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private GameObject _roster, _huntScreen, _settings;

    private InstanceUnit _unit;
    private CameraController _camCont;
    private LayerMask _useLayer = 1 << 6;

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
    }

    #region Actions
    private void OnCameraZoom(InputValue value) => _camCont.CameraZoom(value.Get<float>());

    public void OnIndicatorCast(InputAction.CallbackContext context)
    {
        if (context.started)
            _modifier = true;
        else if (context.canceled)
            _modifier = false;
    }
    
    public void OnAbilityQ() => _unit.Creature.CastAbility(0, _modifier);
    public void OnAbilityW() => _unit.Creature.CastAbility(1, _modifier);
    public void OnAbilityE() => _unit.Creature.CastAbility(2, _modifier);
    public void OnAbilityR() => _unit.Creature.CastAbility(3, _modifier);
    
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