using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class F1SlideChair : MonoBehaviour
{
    //V
    [SerializeField] private float movementDistanceV;
    [SerializeField] private float movementSpeedV;
    private bool movingDown;
    private float topEdge;
    private float downEdge;

    private void Awake()
    {
        
        downEdge = transform.position.y - movementDistanceV;
        topEdge = transform.position.y + movementDistanceV;
    }

    private void Update()
    {
        

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
