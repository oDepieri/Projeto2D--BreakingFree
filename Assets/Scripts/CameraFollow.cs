using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public float offsetY;
    public float offsetZ;

    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(player.position.x,offsetY,offsetZ);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}

