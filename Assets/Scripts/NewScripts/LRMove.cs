using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class LRMove : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float movementSpeed;

    // Inspector 可設定的初始方向
    [SerializeField] private bool startMovingLeft = true;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private Rigidbody2D rb;
    private Vector2 previousPosition;

    // 正在站在平台上的玩家
    private readonly HashSet<Platformer.Mechanics.PlayerController> riders
        = new HashSet<Platformer.Mechanics.PlayerController>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;

        // 使用 Inspector 設定的初始方向
        movingLeft = startMovingLeft;

        previousPosition = rb.position;
    }

    private void FixedUpdate()
    {
        float targetX = movingLeft
            ? Mathf.Max(leftEdge, rb.position.x - movementSpeed * Time.fixedDeltaTime)
            : Mathf.Min(rightEdge, rb.position.x + movementSpeed * Time.fixedDeltaTime);

        Vector2 newPos = new Vector2(targetX, rb.position.y);
        Vector2 delta = newPos - rb.position;

        rb.MovePosition(newPos);

        if (delta != Vector2.zero)
        {
            var ridersArray = new Platformer.Mechanics.PlayerController[riders.Count];
            riders.CopyTo(ridersArray);
            foreach (var player in ridersArray)
            {
                if (player != null)
                {
                    player.ApplyPlatformMovement(delta);
                }
            }
        }

        previousPosition = newPos;

        // 邊界反向
        if (movingLeft)
        {
            if (rb.position.x <= leftEdge + 0.0001f)
                movingLeft = false;
        }
        else
        {
            if (rb.position.x >= rightEdge - 0.0001f)
                movingLeft = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryAddRider(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryAddRider(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var pc = collision.gameObject.GetComponent<Platformer.Mechanics.PlayerController>();
            if (pc != null)
                riders.Remove(pc);
        }
    }

    private void TryAddRider(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                var pc = collision.gameObject.GetComponent<Platformer.Mechanics.PlayerController>();
                if (pc != null)
                {
                    riders.Add(pc);
                }
                return;
            }
        }
    }
}

