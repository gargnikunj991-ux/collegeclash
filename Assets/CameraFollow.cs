using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Drag your Player here
    public Vector3 offset = new Vector3(0, 15, -10); // Matches the position above

    void LateUpdate()
    {
        if (target != null)
        {
            // Smoothly move the camera to stay at the offset distance
            transform.position = target.position + offset;
        }
    }
}