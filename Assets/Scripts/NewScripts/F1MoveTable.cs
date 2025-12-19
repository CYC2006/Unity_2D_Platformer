using UnityEngine;

public class F1MoveTable : MonoBehaviour
{
    [SerializeField] private float movementDistance = 2f;
    [SerializeField] private float movementSpeed = 2f;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private Vector3 lastPosition;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        lastPosition = transform.position;
    }

    private void Update()
    {
        MovePlatform();
        lastPosition = transform.position;
    }

    private void MovePlatform()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
                transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            else
                movingLeft = true;
        }
    }

    // 玩家站在平台上時，補償平台位移
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Vector3 platformDelta = transform.position - lastPosition;
        collision.transform.position += platformDelta;
    }
}
