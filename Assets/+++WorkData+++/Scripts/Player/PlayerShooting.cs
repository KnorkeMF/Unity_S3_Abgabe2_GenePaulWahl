using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public Projectile projectilePrefab;
    public ProjectileData projectileData;
    public Transform firePoint;
    public float fireRate = 0.15f;
    
    private  PlayerInputActions input;
    private bool isShooting = false;
    private float nextFire = 0.0f;

    void Awake()
    {
        input = new PlayerInputActions();

        input.Player.Shoot.started += ctx => isShooting = true;
        input.Player.Shoot.canceled += ctx => isShooting = false;
    }
    
    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();
    
    
    void Update()
    {
        if (!isShooting)
        {
            return;
        }

        if (Time.time >= nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Projectile p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        p.data = projectileData;
        p.Init(Vector2.right);
    }
}
