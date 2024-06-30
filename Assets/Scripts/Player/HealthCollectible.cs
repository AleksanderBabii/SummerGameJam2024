using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [Header("Health Value")]
    [SerializeField] private float healthValue;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip healthPickUp;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(healthPickUp);  
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }

  
}
