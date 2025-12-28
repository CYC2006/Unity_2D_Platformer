using System.Runtime.CompilerServices;
using UnityEngine;
using Platformer.Gameplay; // 為了使用 PlayerEnemyCollision 事件
using Platformer.Mechanics; // 為了識別 PlayerController
using static Platformer.Core.Simulation; // 為了直接使用 Schedule 方法
using System.Collections.Generic;
using System.Collections;

public class KnightEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;

    private KnightPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<KnightPatrol>();
        if (enemyPatrol == null)
        {
            //Debug.Log("patrol is null");
        }
    }   



    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                // Attack
                cooldownTimer = 0;
                //Debug.Log("Knight Attack!");
                anim.SetTrigger("KnightAttack");
            }
        }

        if (enemyPatrol != null)
        {
            //Debug.Log("playerinsight: " + PlayerInSight());
            enemyPatrol.enabled = !PlayerInSight();
            //Debug.Log("KnightPatrol.enabled: " + enemyPatrol.enabled);
        }
        else
        {
            //Debug.Log("enemyPatrol is null");
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance
            , new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer() 
    {
        // damage player
        if (PlayerInSight())
        {
            var playerHealth = GetComponent<Platformer.Mechanics.Health>();
            if (playerHealth != null && playerHealth.IsAlive)
            {
                Debug.Log("Knight Decrement Player Health!");
                // 4. 呼叫官方的扣血方法 (Decrement)
                // 這會自動觸發受傷動畫或 PlayerDeath 事件
                playerHealth.Decrement();
            }

            /*RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

            if (hit.collider != null)
            {
                Debug.Log("Knight Damage Player!");
                // 3. 獲取玩家身上的 Health 組件
                var playerHealth = hit.collider.GetComponent<Platformer.Mechanics.Health>();

                if (playerHealth != null && playerHealth.IsAlive)
                {
                    Debug.Log("Knight Decrement Player Health!");
                    // 4. 呼叫官方的扣血方法 (Decrement)
                    // 這會自動觸發受傷動畫或 PlayerDeath 事件
                    playerHealth.Decrement();
                }
            }*/
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. 檢查撞到的是不是玩家
        var player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            // 2. 建立並排程一個官方的碰撞事件
            // 這會啟動官方的判斷邏輯（踩頭還是受傷）
            var ev = Schedule<PlayerEnemyCollision>();
            ev.player = player;

            // 3. 雖然這是一個 KnightEnemy，但我們假裝自己是 EnemyController 傳進去
            // 這樣官方的 PlayerEnemyCollision 腳本才不會報錯
            // 注意：這裡如果報錯，可能需要 KnightEnemy 繼承或掛載 EnemyController 組件
            ev.enemy = GetComponent<EnemyController>();
        }
    }
}
