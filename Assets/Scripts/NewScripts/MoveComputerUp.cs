// 2025/12/31 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class MoveComputersUp : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of upward movement
    public float upperBoundary = 10f; // Upper boundary for movement

    void Update()
    {
        // Check if the current position is below the upper boundary
        if (transform.position.y < upperBoundary)
        {
            // Move the Computers GameObject upwards
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Stop movement when the upper boundary is reached
            transform.position = new Vector3(transform.position.x, upperBoundary, transform.position.z);
        }
    }
}