
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Follow Player
    [SerializeField] private float followSpeed;
    [SerializeField] private float yOffset;
    [SerializeField] private Transform target;

    private void Start()
    {
        transform.position = target.position;
    }
    private void Update()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        Vector3 newPos = new Vector3(target.position.x,target.position.y+ yOffset, -5f);
        transform.position = Vector3.Slerp(transform.position, newPos,followSpeed * Time.deltaTime);
    }
}
