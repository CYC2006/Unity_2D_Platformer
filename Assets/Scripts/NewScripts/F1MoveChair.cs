using UnityEngine;

public class F1MoveChair : MonoBehaviour
{
    [SerializeField] private float movementDistanceV;
    [SerializeField] private float movementSpeedV;

    private bool movingDown;
    private float topEdge;
    private float downEdge;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        float startY = rb.position.y;
        downEdge = startY - movementDistanceV;
        topEdge = startY + movementDistanceV;
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 targetPos = currentPos;

        if (movingDown)
        {
            if (currentPos.y > downEdge)
            {
                targetPos.y -= movementSpeedV * Time.deltaTime;
            }
            else
            {
                movingDown = false;
            }
        }
        else
        {
            if (currentPos.y < topEdge)
            {
                targetPos.y += movementSpeedV * Time.deltaTime;
            }
            else
            {
                movingDown = true;
            }
        }

        rb.MovePosition(targetPos);
    }
}


