using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    [SerializeField] float distance = 15f;
    [SerializeField] float height = 4f;

    void LateUpdate()
    {
        Vector3 position = player.position - player.forward * distance + Vector3.up * height;

        transform.position = position;

        transform.LookAt(player);
    }
}