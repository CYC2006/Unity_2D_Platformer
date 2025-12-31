// 2025/12/31 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class BossPatrolController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of BossPatrol's movement
    public float leftBoundary = 50f; // Left boundary for movement
    public float rightBoundary = 100f; // Right boundary for movement
    public GameObject enemyPrefab; // Reference to the Enemy prefab
    public float spawnInterval = 2f; // Time interval for spawning enemies

    private bool movingRight = true; // Direction of movement
    private float nextSpawnTime;

    void Update()
    {
        // Move BossPatrol horizontally
        float moveDirection = movingRight ? 1 : -1;
        transform.Translate(Vector3.right * moveDirection * moveSpeed * Time.deltaTime);

        // Check boundaries and reverse direction if needed
        if (transform.position.x > rightBoundary)
        {
            movingRight = false;
        }
        else if (transform.position.x < leftBoundary)
        {
            movingRight = true;
        }

        // Spawn enemy at DropPoint
        Transform dropPoint = transform.Find("DropPoint");
        if (dropPoint != null && Time.time >= nextSpawnTime)
        {
            Instantiate(enemyPrefab, dropPoint.position, Quaternion.identity);
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
}