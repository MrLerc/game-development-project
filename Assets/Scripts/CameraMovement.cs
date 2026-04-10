using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;

    void Update()
    {
        if(player == null)
        {
            return;
        }
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
    }
}