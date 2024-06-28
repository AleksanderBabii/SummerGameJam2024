
using UnityEngine;

public class TrapSideways : MonoBehaviour
{
   [SerializeField] private float movementDistance;
   [SerializeField] private float movementSpeed;
   [SerializeField] private float damage;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;


    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }


    private void Update()
    {
        if (movingLeft) 
        {
            if(transform.position.x > leftEdge)
            { 
                transform.position = new Vector2(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector2(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y);
            }
            else
                movingLeft = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
