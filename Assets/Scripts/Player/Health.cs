using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;


    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();  
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
       if (currentHealth > 0)
       {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
       }
       else
       {
            if (!dead)
            {
                anim.SetTrigger("Dies");

                //Player
                if(GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                // Enemy
                if (GetComponentInParent<EnemiesPatrol>() != null)
                {
                    GetComponentInParent<EnemiesPatrol>().enabled = false;
                }
                    

                if (GetComponent<EnemyKnight>() != null)
                {
                    GetComponent<EnemyKnight>().enabled = false;
                }
                    

                if (GetComponentInParent<EnemyMage>() != null)
                {
                    GetComponent<EnemyMage>().enabled = false;
                }
                    
                dead = true;
            }
       }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1,0,0,0.5f); 
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); 
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
