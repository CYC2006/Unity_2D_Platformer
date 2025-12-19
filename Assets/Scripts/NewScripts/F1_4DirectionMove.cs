using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class F1_4DirectionMove : MonoBehaviour
{
    //H
    [SerializeField] private float movementDistance;
    [SerializeField] private float movementSpeed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    //V
    [SerializeField] private float movementDistanceV;
    [SerializeField] private float movementSpeedV;
    private bool movingDown;
    private float topEdge;
    private float downEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        downEdge = transform.position.y - movementDistanceV;
        topEdge = transform.position.y + movementDistanceV;
    }

    private void Update()
    {
        //H
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }

        //V
        if (movingDown)
        {
            if (transform.position.y > downEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeedV * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingDown = false;
            }
        }
        else
        {
            if (transform.position.y < topEdge)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeedV * Time.deltaTime, transform.position.z);
            }
            else
            {
                movingDown = true;
            }
        }
    }
}
