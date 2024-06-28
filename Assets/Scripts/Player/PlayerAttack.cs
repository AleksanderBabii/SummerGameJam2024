using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float range;
    [SerializeField] private int damage1;
    [SerializeField] private int damage2;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    private Animator anim;
    private PlayerMovement playerMovement;
    private Health enemyHealth;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack1();
        else if(Input.GetMouseButton(1) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack2();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack1()
    {
        anim.SetTrigger("Attack1");
        cooldownTimer = 0;
    }

    private void Attack2()
    {
        anim.SetTrigger("Attack2");
        cooldownTimer = 0;
    }


    private bool EnemyInSight()
    {
        RaycastHit2D hit =
           Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
           0, Vector2.left, 0, enemyLayer);

        if (hit.collider != null)
            enemyHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void EasyAttack()
    {
        if (EnemyInSight())
            enemyHealth.TakeDamage(damage1);
    }

    private void HeavyAttack()
    {
        if(EnemyInSight())
            enemyHealth.TakeDamage(damage2);
    }
}
