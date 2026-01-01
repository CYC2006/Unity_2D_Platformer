// 2026/1/1 AI-Tag
// This was created with the help of Assistant, a Unity Artificial Intelligence product.

using UnityEngine;

public class EnemyAutoDestroy : MonoBehaviour
{
    [Tooltip("設定敵人消失的時間（秒）")]
    public float lifetime = 5f;

    private void Start()
    {
        // 在指定的時間後銷毀該物件
        Destroy(gameObject, lifetime);
    }
}