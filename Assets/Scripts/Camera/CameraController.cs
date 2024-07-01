
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float offset;

    private float lookAhead;
    private float currentPosX;
    private Vector2 velocity = Vector2.zero;
   
    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        //Camera folow logic
        transform.position = new Vector2(player.position.x + lookAhead, player.position.y - offset);
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveCamera(Transform _newPosition)
    {
        print("New Position");
        currentPosX = _newPosition.position.x;
    }

}
