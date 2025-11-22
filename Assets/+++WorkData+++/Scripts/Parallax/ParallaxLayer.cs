using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxLayer3 : MonoBehaviour
{
    public float parallaxFactor = 0.5f;

    private float spriteWidth;
    private Transform cam;
    private Transform parent;

    private void Start()
    {
        cam = Camera.main.transform;
        parent = transform.parent;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;
    }

    private void Update()
    {
        float speed = WorldScrollManager.Instance.scrollSpeed;
        
        transform.position += Vector3.left * speed * parallaxFactor * Time.deltaTime;
        
        if (cam.position.x - transform.position.x > spriteWidth * 1.5f)
        {
            transform.position += new Vector3(spriteWidth * 3f, 0f, 0f);
            Debug.Log(spriteWidth);
        }
    }
}