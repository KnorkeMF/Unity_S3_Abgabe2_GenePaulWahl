using UnityEngine;

public class EnemyDespawn : MonoBehaviour
{
    private Camera cam;
    public float extraDistance = 7f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);
        
        if (viewportPos.x < -extraDistance ||
            viewportPos.x > 1 + extraDistance ||
            viewportPos.y < -extraDistance ||
            viewportPos.y > 1 + extraDistance)
        {
            Destroy(gameObject);
        }
    }
}