using UnityEngine;
using UnityEngine.UI;

public class EnemyPosition: MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Image indicatorImage; // Reference to the UI Image representing the indicator
    public float offscreenPadding = 20f; // Padding from the screen edges for the indicator

    void Update()
    {
        // Calculate the position of the enemy relative to the screen
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Check if the enemy is off-screen
        bool isOffScreen = (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1);

        // Activate or deactivate the UI Image based on whether the enemy is off-screen
        indicatorImage.enabled = isOffScreen;

        if (isOffScreen)
        {
            // Calculate the direction vector from the enemy to the player
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

            // Calculate the position of the indicator along the screen edges
            Vector3 indicatorPosition = new Vector3(
                Mathf.Clamp01(viewportPosition.x),
                Mathf.Clamp01(viewportPosition.y),
                0f
            );

            // Apply padding from the screen edges
            indicatorPosition.x = Mathf.Clamp(indicatorPosition.x, offscreenPadding / Screen.width, 1 - offscreenPadding / Screen.width);
            indicatorPosition.y = Mathf.Clamp(indicatorPosition.y, offscreenPadding / Screen.height, 1 - offscreenPadding / Screen.height);

            // Convert the indicator position from viewport space to screen space
            Vector3 screenPosition = new Vector3(
                indicatorPosition.x * Screen.width,
                indicatorPosition.y * Screen.height,
                0f
            );

            // Update the position of the UI Image
            indicatorImage.transform.position = screenPosition;
        }
    }
}
