// 2025/12/31 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class F3SavePoint : MonoBehaviour
{
    public Transform spawnPoint; // Reference to the SpawnPoint Transform
    public string playerTag = "Player"; // Tag of the player GameObject

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the F3SavePoint
        if (other.CompareTag(playerTag))
        {
            if (spawnPoint != null)
            {
                // Update SpawnPoint position to F3SavePoint's position
                spawnPoint.position = transform.position;
                //Debug.Log("SpawnPoint updated to F3SavePoint position: " + transform.position);
            }
        }
    }
}