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
    
    [Header("Missile Settings")]
    public Projectile missilePrefab;
    public ProjectileData missileData;
    public float missileCooldown = 3f;
    
    [Header("Audio")]
    public AudioClip shootClip;
    public AudioSource audioSource;


    private bool missilePressed;
    public float nextMissileTime = 0f;


    void Awake()
    {
        input = new PlayerInputActions();

        input.Player.Shoot.started += ctx => isShooting = true;
        input.Player.Shoot.canceled += ctx => isShooting = false;
        
        input.Player.Missile.started += ctx => missilePressed = true;

    }
    
    void OnEnable() => input.Enable();
    void OnDisable() => input.Disable();
    
    
    void Update()
    {
        if (missilePressed)
        {
            FireMissile();
            missilePressed = false;
        }
        
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
        
        if (audioSource != null && shootClip != null)
            audioSource.PlayOneShot(shootClip);
    }
    
    private void FireMissile()
    {
        if (Time.time < nextMissileTime)
            return;

        Projectile p = Instantiate(missilePrefab, firePoint.position, Quaternion.identity);
        p.data = missileData;
        p.Init(Vector2.right);

        nextMissileTime = Time.time + missileCooldown;
    }

}
