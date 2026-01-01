// 2026/1/1 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 確認碰撞的物件是否是玩家
        if (collision.CompareTag("Player"))
        {
            // 讓 Key 消失
            gameObject.SetActive(false);
        }
    }
}