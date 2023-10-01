using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    
    private void Update()
    {
        if (!target)
            return;
        
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z - 8f);
    }
}