using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Arrow
    [SerializeField] private Transform _arrowEffect;
    private Transform _circle;
    private Transform[] _arrows = new Transform[4];
    private Quaternion[] _arrowRotsFrom = {Quaternion.Euler(-90f, 0f, 0f),
                                        Quaternion.Euler(90f, 0f, 90f),
                                        Quaternion.Euler(90f, 0f, 0f),
                                        Quaternion.Euler(-90f, 0f, -90f)};
    private Quaternion[] _arrowRotsTo = {Quaternion.identity,
                                        Quaternion.Euler(0f, -90f, 0f),
                                        Quaternion.identity,
                                        Quaternion.Euler(0f, -90f, 0f)};
    private Vector3[] _arrowPos = {new Vector3(0f, 0.5f, -0.5f),
                                new Vector3(-0.5f, 0.5f, 0f),
                                new Vector3(0f, 0.5f, 0.5f),
                                new Vector3(0.5f, 0.5f, 0f)};

    private float _downA = 40f, _forwardA = 3.5f, _timeDiv = 0.2f, _circleS = 0.5f;
    
    // Movement
    [SerializeField] private Transform _character;
    private NavMeshAgent _navMA;
    private LayerMask _useLayer = 1 << 7, _groundLayer = (1 << 6);
    
    private float _maxUseDistance = 1000f;
    private Ray _ray;
    private RaycastHit _hit;

    private Coroutine _clickAnim;

    public bool MoveOnUI { get; set; } = true;

    private void Awake()
    {
        // Arrow
        _circle = _arrowEffect.GetChild(0);
        
        for (int i = 0; i < _arrows.Length; i++)
            _arrows[i] = _arrowEffect.GetChild(i+1);
        
        // Movement
        _navMA = GetComponent<NavMeshAgent>();
        _navMA.updateRotation = false;
    }
    
    private void LateUpdate()
    {
        if (_navMA.velocity.sqrMagnitude > Mathf.Epsilon)
            _character.rotation = Quaternion.LookRotation(_navMA.velocity.normalized);
    }

    private void OnMove()
    {
        //Debug.Log("MoveOnUI is: " + MoveOnUI + " and EventSystem is: " + EventSystem.current.IsPointerOverGameObject());
        if (!MoveOnUI && EventSystem.current.IsPointerOverGameObject())
                return;
        
        _ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if (Physics.Raycast(_ray, out _hit, _maxUseDistance, _groundLayer))
        {
            _navMA.SetDestination(_hit.point); // Movement
            
            if (_clickAnim != null) // Arrow
                StopCoroutine(_clickAnim);
            _clickAnim = StartCoroutine(ClickMarker(_hit.point));
        }
    }

    #region Arrow
    private IEnumerator ClickMarker(Vector3 clickSpot)
    {
        _arrowEffect.gameObject.SetActive(false);
        
        foreach (Transform arrow in _arrows)
        {
            arrow.gameObject.SetActive(false);
            arrow.GetChild(0).GetChild(0).GetComponent<TrailRenderer>().Clear();
        }
        
        _arrowEffect.position = clickSpot;
        
        _circle.localScale = new Vector3(0.175f, _circle.localScale.y, 0.175f);
        _circle.gameObject.SetActive(true);

        for (int i = 0; i < _arrows.Length; i++)
        {
            _arrows[i].localPosition = _arrowPos[i];
            _arrows[i].localRotation = _arrowRotsFrom[i];
        }

        _arrowEffect.gameObject.SetActive(true);
        
        float time = 0f;
        while (time < 0.1f)
        {
            _circle.localScale -= new Vector3(_circleS, 0f, _circleS) * Time.deltaTime;
            yield return null;
            time += Time.deltaTime;
        }
        foreach (Transform arrow in _arrows)
            arrow.gameObject.SetActive(true);
        
        while (time < 0.2f)
        {
            _circle.localScale -= new Vector3(_circleS, 0f, _circleS) * Time.deltaTime;

            _arrows[0].localPosition -= new Vector3(0f, (time - 0.1f) * _downA, -_forwardA * (1 - (time - 0.1f) / _timeDiv)) * Time.deltaTime;
            _arrows[1].localPosition -= new Vector3(-_forwardA * (1 - (time - 0.1f) / _timeDiv), (time - 0.1f) * _downA, 0f) * Time.deltaTime;
            _arrows[2].localPosition -= new Vector3(0f, (time - 0.1f) * _downA, _forwardA * (1 - (time - 0.1f) / _timeDiv)) * Time.deltaTime;
            _arrows[3].localPosition -= new Vector3(_forwardA * (1 - (time - 0.1f) / _timeDiv), (time - 0.1f) * _downA, 0f) * Time.deltaTime;
            
            for (int i = 0; i < _arrows.Length; i++)
                _arrows[i].localRotation = Quaternion.Slerp(_arrowRotsFrom[i], _arrowRotsTo[i], (time - 0.1f) / _timeDiv);

            yield return null;
            time += Time.deltaTime;
        }
        _circle.gameObject.SetActive(false);
        
        while (time < 0.3f)
        {
            _arrows[0].localPosition -= new Vector3(0f, (time - 0.1f) * _downA, -_forwardA * (1 - (time - 0.1f) / _timeDiv)) * Time.deltaTime;
            _arrows[1].localPosition -= new Vector3(-_forwardA * (1 - (time - 0.1f) / _timeDiv), (time - 0.1f) * _downA, 0f) * Time.deltaTime;
            _arrows[2].localPosition -= new Vector3(0f, (time - 0.1f) * _downA, _forwardA * (1 - (time - 0.1f) / _timeDiv)) * Time.deltaTime;
            _arrows[3].localPosition -= new Vector3(_forwardA * (1 - (time - 0.1f) / _timeDiv), (time - 0.1f) * _downA, 0f) * Time.deltaTime;
            
            for (int i = 0; i < _arrows.Length; i++)
                _arrows[i].localRotation = Quaternion.Slerp(_arrowRotsFrom[i], _arrowRotsTo[i], (time - 0.1f) / _timeDiv);
            
            yield return null;
            time += Time.deltaTime;
        }
        
        for (int i = 0; i < _arrows.Length; i++)
            _arrows[i].localRotation = Quaternion.Slerp(_arrowRotsFrom[i], _arrowRotsTo[i], (time - 0.1f) / _timeDiv);
        
        yield return new WaitForSeconds(0.5f);
        _arrowEffect.gameObject.SetActive(false);
    }
    #endregion
}