
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float offset;

    private float lookAhead;
   
    private void Update()
    {
        //Camera folow logic
        transform.position = new Vector2(player.position.x + lookAhead,player.position.y - offset);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }


}
