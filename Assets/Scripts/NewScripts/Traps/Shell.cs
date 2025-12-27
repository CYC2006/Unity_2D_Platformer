using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float lifeTime = 3f;

    void Start()
    {
        // 預防子彈飛出世界，3秒後自動銷毀
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 不改 Layer 的判斷邏輯：
        // 1. 如果撞到 Player (標籤判定)
        // 2. 或者是任何「名字不是 Tank」的物件 (例如牆壁、地面、箱子)
        if (other.CompareTag("Player") || other.gameObject.name != "Tank")
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            // 在子彈目前撞擊的位置產生爆炸
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}