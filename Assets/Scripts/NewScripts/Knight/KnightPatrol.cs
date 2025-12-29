using UnityEngine;

public class KnightPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool moveingLeft;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        //Debug.Log("Disable");
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        //Debug.Log("Update Patrol");
        if (moveingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1); // Move left
            else
            {
                moveingLeft = false;
                //MoveInDirection(1); // Move right
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1); // Move right
            else
            {
                moveingLeft = true;
                //MoveInDirection(-1); // Move left
            }
        }
    }

    private void MoveInDirection(int _direction)
    {
        anim.SetBool("moving", true);
        // make the enemy face the direction it's moving
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, 
            initScale.y, initScale.z);

        // move the enemy in the specified direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, 
            enemy.position.y, enemy.position.z);
    }
}
