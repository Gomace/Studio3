using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private Camera _cam;
    private float _camStartFOV = 52f, _zoomSpeed = 10f;

    private Coroutine _camUpdate;

    private void Awake()
    {
        _cam = GetComponent<Camera>();

        _cam.transform.localPosition = new Vector3(0, 11f, -7.5f);
        _cam.transform.localRotation = Quaternion.Euler(60f, 0f, 0f);
    }
    
    public void CameraZoom(float axis)
    {
        _camStartFOV -= (axis / 120f) * _zoomSpeed;
        _camStartFOV = Mathf.Clamp(_camStartFOV, 30f, 52f);
        
        if (_camUpdate != null) 
            StopCoroutine(_camUpdate);
        _camUpdate = StartCoroutine(UpdateCam());
    }
    
    private IEnumerator UpdateCam()
    {
        float time = 0;
        while (time < 1)
        {
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _camStartFOV, time);
            yield return null;
            time += Time.deltaTime * _zoomSpeed;
        }
    }

    
}