using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Camera cam;

    private float camFOV, zoomSpeed = 10f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (!cam)
            Debug.Log("Put this script on the Camera :|");
        
        if (cam != null)
            camFOV = cam.fieldOfView;
    }
    
    private void Update()
    {
        if (!target)
            return;
        
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z - 7.5f);
        
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, zoomSpeed * Time.deltaTime);
    }

    public void CameraZoom(float axis)
    {
        camFOV -= (axis / 120f) * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 30f, 52f);
    }
}