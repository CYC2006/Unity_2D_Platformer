using UnityEngine;
using Platformer.Mechanics;

public class TankControl : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
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
    private TankPatrol enemyPatrol;

    private RaycastHit2D hit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<TankPatrol>();
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
                anim.SetTrigger("attack");
                RangedAttack();
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