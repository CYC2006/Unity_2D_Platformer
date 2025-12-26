using Platformer.Mechanics;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
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

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ("Tank") || collision.tag == ("Firepoint") || collision.tag == ("Untagged")) return; //Ignore collision with tank enemy

        hit = true;
        base.OnTriggerEnter2D(collision); //Execute logic from parent script first
        coll.enabled = false;

        if (anim != null && collision.tag == ("Player"))
        {
            //Debug.Log("Explode on player");
            anim.SetTrigger("explode"); //When the object is a fireball explode it
            collision.GetComponent<Health>().Die();
        }
        else
            gameObject.SetActive(false); //When this hits any object deactivate arrow
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}