// 2025/12/31 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class OpenComputerPointController : MonoBehaviour
{
    public GameObject computers; // Reference to the Computers GameObject
    public Vector3 initialPosition = new Vector3(0f, 0f, 0f); // Initial position to reset Computers
    public string playerTag = "Player"; // Tag of the player GameObject

    private void Start()
    {
        // Ensure Computers is initially invisible and inactive
        if (computers != null)
        {
            computers.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the OpenComputerPoint
        if (other.CompareTag(playerTag))
        {
            if (computers != null)
            {
                // Reset Computers to initial position
                computers.transform.position = initialPosition;

                // Make Computers visible and active
                computers.SetActive(true);
            }
        }
    }
}