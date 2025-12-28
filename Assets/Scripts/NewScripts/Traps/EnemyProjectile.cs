using System.Runtime.CompilerServices;
using UnityEngine;
using Platformer.Gameplay; // 為了使用 PlayerEnemyCollision 事件
using Platformer.Mechanics; // 為了識別 PlayerController
using static Platformer.Core.Simulation; // 為了直接使用 Schedule 方法
using System.Collections.Generic;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private Health playerHealth; // 直接拖入玩家的 Health 組件

    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. 過濾不需要碰撞的標籤
        if (collision.CompareTag("Tank") || collision.CompareTag("Firepoint") || collision.CompareTag("Untagged")) return;

        // 2. 檢查撞到的是不是玩家 (尋找 PlayerController)
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null) player = collision.gameObject.GetComponentInParent<PlayerController>();

        if (player != null)
        {
            // 3. 調度官方碰撞事件
            var ev = Schedule<PlayerEnemyCollision>();
            ev.player = player;

            // 關鍵：子彈必須指定「誰是傷害來源」
            // 如果子彈物件上沒有 EnemyController，這裡會傳 null，可能會導致官方邏輯失效
            ev.enemy = GetComponent<EnemyController>();

            if (anim != null) anim.SetTrigger("explode");
        }
        else
        {
            // 撞到牆壁等物件
            gameObject.SetActive(false);
        }
    }
    private void Deactivate()
    {
        playerHealth.Decrement();
        gameObject.SetActive(false);
    }
}