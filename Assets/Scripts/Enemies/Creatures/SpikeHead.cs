using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("SpikeHead Setings")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector2[] directions = new Vector2[4];
    private Vector2 destination;
    private Animator animator;
    private float checkTimer;
    private bool attacking;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //Moving SpikeHead to destination only when he's attacking
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        //Check if SpikeHead sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++) 
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);  
            
            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
                animator.SetBool("NotAttacking", true);
            }
        }
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range; // Right direction
        directions[1] = -transform.right * range; // Left
        directions[2] = transform.up * range; // Up
        directions[3] = -transform.up * range; // Down
    }

    private void Stop()
    {
        destination = transform.position; //Set destination as current position and Spikehead stops
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D (collision);
        Stop();
    }
}
