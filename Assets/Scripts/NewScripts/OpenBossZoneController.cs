// 2025/12/31 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using Unity.Cinemachine;
using UnityEngine;

public class OpenBossZoneController : MonoBehaviour
{
    public CinemachineCamera virtualCamera; // Reference to the Cinemachine Virtual Camera
    public float enlargedOrthographicSize; // The 

    public Transform spawnPoint; // Reference to the

    public GameObject bossPatrol; // Reference to the BossPatrol GameObject
    public GameObject bossPatrol1; // Reference to the BossPatrol GameObject
    public GameObject bossPatrol2; // Reference to the BossPatrol GameObject
    public GameObject bossPatrol3; // Reference to the BossPatrol GameObject
    public GameObject bossPatrol4; // Reference to the BossPatrol GameObject
    public string playerTag = "Player"; // Tag of the player GameObject

    private void Start()
    {
        // Ensure BossPatrol is initially disabled
        if (bossPatrol != null)
        {
            bossPatrol.SetActive(false);
            bossPatrol1.SetActive(false);
            bossPatrol2.SetActive(false);
            bossPatrol3.SetActive(false);
            bossPatrol4.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the OpenBossZone
        if (other.CompareTag(playerTag))
        {
            if (bossPatrol != null)
            {
                // Enable BossPatrol when the player enters the zone
                bossPatrol.SetActive(true);
                bossPatrol1.SetActive(true);
                bossPatrol2.SetActive(true);
                bossPatrol3.SetActive(true);
                bossPatrol4.SetActive(true);
            }

            if (virtualCamera != null)
            {
                // Adjust the Orthographic Size of the Cinemachine Virtual Camera
                virtualCamera.Lens.OrthographicSize = enlargedOrthographicSize;
            }

            if (spawnPoint != null)
            {
                // Update SpawnPoint position to OpenBossZone's position
                spawnPoint.position = transform.position;
            }
        }
    }
}