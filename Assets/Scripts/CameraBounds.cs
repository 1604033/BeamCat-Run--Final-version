using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Transform player;  // The player or target object
    
    // These values define the min/max boundaries for the camera
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void LateUpdate()
    {
        if (player != null)
        {
            // Get the camera's current position
            Vector3 newPosition = transform.position;

            // Set the camera to follow the player, but restrict it within the specified boundaries
            newPosition.x = Mathf.Clamp(player.position.x, minX, maxX);
            newPosition.y = Mathf.Clamp(player.position.y, minY, maxY);

            // Apply the clamped position to the camera
            transform.position = newPosition;
        }
    }
}
