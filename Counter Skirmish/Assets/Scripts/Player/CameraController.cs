using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    public float mouseSens = 10, moveSpeed = 20f, screenEdgeThresh = 25f;

    private bool camCentered = false, camLocked = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
            camLocked = !camLocked;
        if (Input.GetKeyDown(KeyCode.Space))
            camCentered = true;
        if (Input.GetKeyUp(KeyCode.Space))
            camCentered = false;

        // If camera is centered, set its position to match the player's position
        if ((camCentered || camLocked) && target != null)
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z - 6.5f);
        else // Camera movement code based on mouse position
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 movement = Vector3.zero;
     
            if (mousePosition.x < screenEdgeThresh)
                movement += Vector3.left;
            else if (mousePosition.x > Screen.width - screenEdgeThresh)
                movement += Vector3.right;
     
            if (mousePosition.y < screenEdgeThresh)
                movement += Vector3.back;
            else if (mousePosition.y > Screen.height - screenEdgeThresh)
                movement += Vector3.forward;
     
            movement.Normalize();
            movement *= moveSpeed * Time.deltaTime;
            transform.position += movement;
        }
    }
}