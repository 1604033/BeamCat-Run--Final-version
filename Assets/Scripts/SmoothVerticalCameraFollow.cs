using UnityEngine;

public class SmoothVerticalCameraFollow : MonoBehaviour
{
    public Transform player;  // The player's transform to follow
    public float verticalFollowSpeed = 0.05f;  // Speed for following vertically (lower for smoother movement)
    public float verticalOffset = 2f;  // Vertical offset to keep the camera slightly above the player

    private Vector3 initialPosition;

    void Start()
    {
       // Store the initial camera position
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            return;  // Exit if no player is assigned
        }

        // Get the current camera position
        Vector3 currentCameraPosition = transform.position;

        // Calculate the target vertical position using Lerp for smooth following
        float targetYPosition = Mathf.Lerp(currentCameraPosition.y, player.position.y + verticalOffset, verticalFollowSpeed);

        // Apply the smoother vertical following without changing horizontal movement
        transform.position = new Vector3(currentCameraPosition.x, targetYPosition, currentCameraPosition.z);
    }
}