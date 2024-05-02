using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ClickMarker))]
public class PlayerMovement : MonoBehaviour
{
    private Transform _character;
    private Camera _mainCam;
    private ClickMarker _clickArrow;
    private GameObject _abilityIndicator;
    private NavMeshAgent _navMA;

    private const float _turnSpeed = 50f;
    
    // Raycast
    private const float _maxUseDistance = 200f;
    private Ray _ray;
    private RaycastHit _hit;
    private readonly LayerMask _groundLayer = (1 << 6)/*, _useLayer = 1 << 7*/;
    
    private Coroutine _clickAnim, _indicUpdate; // Arrow & Indicator

    public bool MoveOnUI { get; set; } = true;

    // Targeting Indicator
    public bool Indicating { get; set; }
    //public bool Channeling { get; set; } // Not used yet
    
    private void Awake()
    {
        _character = transform.Find("Character");
        
        _mainCam = Camera.main; 
        _clickArrow = GetComponent<ClickMarker>();
        
        _navMA = GetComponent<NavMeshAgent>();
        _navMA.updateRotation = false;

        if (SceneManager.GetActiveScene().name == "Hub")
            MoveOnUI = false;
    }
    
    private void LateUpdate()
    {
        if (_navMA.velocity.sqrMagnitude > Mathf.Epsilon)
            _character.rotation = Quaternion.Slerp(_character.rotation, Quaternion.LookRotation(_navMA.velocity.normalized), Time.deltaTime * _turnSpeed);
    }

    private void OnMove()
    {
        if (Indicating)
        {
            Indicating = false;
            return;
        }
        
        //Debug.Log("MoveOnUI is: " + MoveOnUI + " and EventSystem is: " + EventSystem.current.IsPointerOverGameObject());
        if (!MoveOnUI && EventSystem.current.IsPointerOverGameObject())
                return;
        
        _ray = _mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer))
        {
            _navMA.SetDestination(_hit.point); // Movement
            
            if (_clickAnim != null) // Arrow
                StopCoroutine(_clickAnim);
            _clickAnim = StartCoroutine(_clickArrow.ArrowMarker(_hit.point));
        }
    }

    public void OnStopMoving()
    {
        _ray = new Ray(transform.position + new Vector3(0f, 2f, 0f), Vector3.down);
        _navMA.SetDestination(Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer) ? _hit.point : transform.position);
    }
    
    public Vector3 MouseLocation()
    {
        _ray = _mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer))
            return _hit.point;
        return Mouse.current.position.ReadValue();
    }
    
    public void ShowIndicator(GameObject indicator)
    {
        if (_abilityIndicator) // Turns off old indicator
            _abilityIndicator.SetActive(false);
        
        if (_indicUpdate != null)
            StopCoroutine(_indicUpdate);
        
        _abilityIndicator = indicator;
        indicator.SetActive(true); // Turns on new indicator

        Indicating = true;
        _indicUpdate = StartCoroutine(IndicDirection(indicator.transform));
    }
    
    private IEnumerator IndicDirection(Transform indicator)
    {
        Quaternion rotation;
        while (Indicating)
        {
            _ray = _mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer))
            {
                rotation = Quaternion.LookRotation(indicator.position - _hit.point, Vector3.up);
                rotation.eulerAngles = new Vector3(0f, rotation.eulerAngles.y, 0);
                indicator.rotation = rotation;
            }

            yield return null;
        }
        
        indicator.gameObject.SetActive(false);
        StopCoroutine(_indicUpdate);
    }
}