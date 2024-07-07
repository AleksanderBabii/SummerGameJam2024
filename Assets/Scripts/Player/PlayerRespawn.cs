using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; // Sound that we play when checkpoint is activated
    private Transform currentCheckpoint; //Storing last checkpoint
    private Health playerHealth;

    private UIManager uiManager;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void RespawnCheck()
    {
        //Check if checkpoint is available
        if (currentCheckpoint == null)
        {
            //Show game over screen
            uiManager.GameOver();
            return; //won't execute the rest of the code
        }
        else
            RespawnMade();
       
    }
    public void RespawnMade()
    {
        playerHealth.Respawn(); //Restore player health and reset animation
        transform.position = currentCheckpoint.position;//Moves player to the checkpoint location

        //Move camera to checkpoint
        Camera.main.GetComponent<CameraController>().FollowPlayer();
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
