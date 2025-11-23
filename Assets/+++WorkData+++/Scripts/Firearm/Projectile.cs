using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData data;
    private Vector3 direction;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, data.lifetime);
    }
    void Update()
    {
        transform.position += (Vector3)direction * data.speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (data.isPlayerProjectile && other.TryGetComponent(out EnemyBehavior enemy))
        {
            enemy.TakeDamage(data.damage);
            Destroy(gameObject);
        }
        
        if (!data.isPlayerProjectile && other.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(data.damage);
            Destroy(gameObject);
        }
    }
}
