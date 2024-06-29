using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime; 
    private Animator animator;
    private BoxCollider2D BoxCollider2D;

    private bool hit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        BoxCollider2D = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        BoxCollider2D.enabled = true;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed,0,0);

        lifeTime += Time.deltaTime;
        if(lifeTime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision); //Execute parent script first
        BoxCollider2D.enabled = false; 
        
        if (animator != null)
            animator.SetTrigger("Explode"); // When fireball explodes
        else
            gameObject.SetActive(false); //Deactivate when hits other object
    }

    private void Diactivate()
    {
        gameObject.SetActive(false);
    }
}
