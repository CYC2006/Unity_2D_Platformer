using UnityEngine;
using Platformer.Mechanics;

public class TankEnemyController : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] shells;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    // Referances
    private Animator anim;
    private EnemyPatrol enemyPatrol;

    private RaycastHit2D hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Attack only when player in sight?
        if (PlayerInSight())
        {
            if (enemyPatrol != null) enemyPatrol.enabled = false;

            // 既然確定看到了玩家，就從 playerHit 提取玩家的 X 座標
            float playerX = hit.transform.position.x;
            float tankX = transform.position.x;

            // 判斷玩家在左還是右
            float directionToPlayer = (playerX > tankX) ? 1 : -1;

            // 強制坦克轉向
            transform.localScale = new Vector3(1 * directionToPlayer,
                                               1, 1);
            //Debug.Log("玩家進入範圍");
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                //anim.SetTrigger("attack");
                RangedAttack();
            }
        }
        else
        {
            enemyPatrol.enabled = true;
        }

    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        int shellIndex = FindShell();
        shells[shellIndex].transform.position = firePoint.position;
        shells[shellIndex].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindShell()
    {
        for (int i = 0; i < shells.Length; i++)
        {
            if (!shells[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    
    private bool PlayerInSight()
    {
        hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}