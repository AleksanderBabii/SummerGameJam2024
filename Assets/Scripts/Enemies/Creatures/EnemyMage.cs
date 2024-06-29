using UnityEngine;

public class EnemyMage : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;


    [Header("Fire Ball Attack Parameters")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBall;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //References
    private Animator animator;
    private Health playerHealth;
    private EnemiesPatrol enemiesPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemiesPatrol = GetComponentInParent<EnemiesPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //Attack
                cooldownTimer = 0;
                animator.SetTrigger("FireBallAttack");
            }
        }
        if (enemiesPatrol != null)
            enemiesPatrol.enabled = !PlayerInSight();

    }

    private void FireBallAttack()
    {
        cooldownTimer = 0;
        fireBall[FindFireball()].transform.position = firePoint.position;
        fireBall[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }


    private int FindFireball()
    {
        for (int i = 0; i < fireBall.Length; i++)
        {
            if (!fireBall[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }


}
