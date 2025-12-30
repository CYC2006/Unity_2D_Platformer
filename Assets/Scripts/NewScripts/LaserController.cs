using System.Runtime.CompilerServices;
using UnityEngine;
using Platformer.Gameplay; // 為了使用 PlayerEnemyCollision 事件
using Platformer.Mechanics; // 為了識別 PlayerController
using static Platformer.Core.Simulation; // 為了直接使用 Schedule 方法
using System.Collections.Generic;
using System.Collections;
public class LaserController : MonoBehaviour
{
    public float activeTime = 2f; // Laser顯示的時間，玩家可設定
    public float inactiveTime = 2f; // Laser隱藏的時間，玩家可設定
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isActive = true;

    void Start()
    {
        // 獲取 SpriteRenderer 和 BoxCollider2D 組件
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // 啟動循環顯示/隱藏的協程
        StartCoroutine(LaserCycle());
    }

    IEnumerator LaserCycle()
    {
        while (true)
        {
            // 顯示 laser
            isActive = true;
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;

            yield return new WaitForSeconds(activeTime);

            // 隱藏 laser
            isActive = false;
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;

            yield return new WaitForSeconds(inactiveTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. 檢查撞到的是不是玩家 (使用 other)
        var player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            // 2. 建立並排程碰撞事件
            var ev = Schedule<PlayerEnemyCollision>();
            ev.player = player;

            // 3. 傳入 EnemyController
            // 注意：如果這個雷射陷阱本身沒有 EnemyController 組件，這裡會傳回 null
            ev.enemy = GetComponent<EnemyController>();
        }
    }
}