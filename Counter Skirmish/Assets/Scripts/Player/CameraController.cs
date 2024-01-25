using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Camera _cam;

    private float _camStartFOV, _zoomSpeed = 10f;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
        _camStartFOV = _cam.fieldOfView;
    }
    
    private void Update()
    {
        if (!_target)
            return;
        
        transform.position = new Vector3(_target.position.x, transform.position.y, _target.position.z - 7.5f);
        
        _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _camStartFOV, _zoomSpeed * Time.deltaTime);
    }

    public void CameraZoom(float axis)
    {
        _camStartFOV -= (axis / 120f) * _zoomSpeed;
        _camStartFOV = Mathf.Clamp(_camStartFOV, 30f, 52f);
    }
}