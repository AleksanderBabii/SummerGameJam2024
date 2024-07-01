using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; // Sound that we play when checkpoint is activated
    private Transform currentCheckpoint; //Storing last checkpoint
    private Health playerHealth;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.position;//Moves player to the checkpoint location
        playerHealth.Respawn(); //Restore player health and reset animation


        //Move camera to checkpoint
        Camera.main.GetComponent<CameraController>().MoveCamera(currentCheckpoint.parent);
    }


    //Activate checkpoints
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // Stores activated checkpoint as current checkpoint
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; // Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("OnTheCheckpoint"); // Trigger checkpoint animation
            
        }
    }
}
