using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform target;
    public float height = 100f;

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(target.position.x, height, target.position.z);
        transform.rotation = Quaternion.Euler(100f, 0f, 0f);
    }
}