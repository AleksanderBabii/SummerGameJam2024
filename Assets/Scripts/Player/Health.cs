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

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hurt;
    [SerializeField] private AudioClip dyingSound;

    private bool invulnerable;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();  
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
         if (currentHealth > 0)
         {
            anim.SetTrigger("Hurt");
            SoundManager.instance.PlaySound(hurt);
            StartCoroutine(Invulnerability());
         }
         else
         {
            if (!dead)
            {
               
                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                anim.SetBool("Grounded", true);
                anim.SetTrigger("Dies");
                SoundManager.instance.PlaySound(dyingSound);

                dead = true;
            }
         }
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    //Respawn logic
    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("Die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(9, 10, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1,0,0,0.5f); 
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2)); 
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(9, 10, false);

        invulnerable = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    
   

}
