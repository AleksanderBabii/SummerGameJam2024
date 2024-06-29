using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    [SerializeField] private float damage;


    [Header("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool trapTriggered; 
    private bool trapActivated;

    private Health playerHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerHealth != null && trapActivated) 
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = GetComponent<Health>();

            if (!trapActivated)
                StartCoroutine(ActivateFiretrap());
            
            if (trapActivated)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        //turn sprite red to notify Player about danger
        trapTriggered = true;
        spriteRenderer.color = Color.red;

        //Wait for delay, trap activation, animation turn on, return to idle state
        yield return new WaitForSeconds(activationDelay);
        spriteRenderer.color = Color.white;
        trapActivated = true;
        animator.SetBool("FireTrapActive", true);

        //Wait X seconds for trap deactivation and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        trapActivated = false;
        trapTriggered = false;
        animator.SetBool("FireTrapActive",false);
    }
}
