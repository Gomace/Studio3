using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ClickMarker))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _character;

    private Camera _mainCam;
    private ClickMarker _clickArrow;
    private GameObject _abilityIndicator;
    private NavMeshAgent _navMA;
    private LayerMask _useLayer = 1 << 7, _groundLayer = (1 << 6);
    
    private const float _maxUseDistance = 200f;
    private Ray _ray;
    private RaycastHit _hit;
    
    private Coroutine _clickAnim, _indicUpdate; // Arrow & Indicator

    public bool MoveOnUI { get; set; } = true;

    // Targeting Indicator
    public bool Indicating { get; set; }
    public bool Channeling { get; set; }
    
    private void Awake()
    {
        _mainCam = Camera.main; 
        _clickArrow = GetComponent<ClickMarker>();
        
        _navMA = GetComponent<NavMeshAgent>();
        _navMA.updateRotation = false;
    }
    
    private void LateUpdate()
    {
        if (_navMA.velocity.sqrMagnitude > Mathf.Epsilon)
            _character.rotation = Quaternion.LookRotation(_navMA.velocity.normalized);
    }

    private void OnMove() // TODO make so Cancel ability and Interact (at least in Hub)
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