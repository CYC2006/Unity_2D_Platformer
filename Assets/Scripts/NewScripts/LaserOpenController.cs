// 2026/1/1 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;
using System.Collections;

public class LaserOpenController : MonoBehaviour
{
    [Tooltip("雷射的 GameObject 陣列")]
    public GameObject[] lasers; // 雷射的 GameObject 陣列

    [Tooltip("雷射開啟的間隔時間")]
    public float intervalTime = 2f;

    private bool isRunning = false; // 是否正在執行開關邏輯

    private void Start()
    {
        ResetLaserStates();
    }

    private void ResetLaserStates()
    {
        // 初始化雷射狀態
        foreach (GameObject laser in lasers)
        {
            if (laser != null)
            {
                laser.SetActive(false); // 初始狀態隱藏雷射
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isRunning)
        {
            isRunning = true; // 防止重複執行
            StartCoroutine(ActivateLasersSequentially());
        }
    }

    private IEnumerator ActivateLasersSequentially()
    {
        foreach (GameObject laser in lasers)
        {
            if (laser != null)
            {
                // 等待間隔時間
                yield return new WaitForSeconds(intervalTime);

                // 啟動雷射
                laser.SetActive(true);

                // 等待雷射消失
                yield return new WaitUntil(() => !laser.activeSelf);
            }
        }

        // 重置狀態，允許重新執行邏輯
        isRunning = false;
    }
}