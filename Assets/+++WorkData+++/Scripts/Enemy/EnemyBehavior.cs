using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyData data;
    
    [HideInInspector]
    public int currentHealth;
    
    private float nextFireTime = 0f;

    private Transform player;
    
    private SpriteRenderer spriteRenderer;
    
    public Projectile projectilePrefab;
    public Transform firePoint;
    
    [HideInInspector] public float shootingTimer = 0f;
    [HideInInspector] public bool isShootingPhase = true;
    [HideInInspector] public float nextSpreadTime = 0f;
    
    [HideInInspector] public bool kamikazeInitialized = false;
    [HideInInspector] public Vector2 kamikazeDirection;
    [HideInInspector] public bool kamikazePassedPlayer = false;

    

    public float stopX;
    public bool hasStopX = false;


    void Start()
    {
        if (data == null)
        {
            Debug.LogError("Enemy spawned without EnemyData!", this);
            Destroy(gameObject);
            return;
        }

        currentHealth = data.maxHealth;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (data.sprite != null)
        {
            spriteRenderer.sprite = data.sprite;
        }
    }

    
    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        if(data.movementPattern == null)
            return;

        Vector2 direction = data.movementPattern.GetDirection(this);
        transform.position += (Vector3)(direction * data.speed * Time.deltaTime);
    }

    void HandleShooting()
    {
        if (data.isKamikaze)
            return;
        
        if (data.shootingPattern == null)
            return;

        // Spread-Shooting wird komplett über eigenes Timing gesteuert
        if (data.shootingPattern is ShootSpreadSO spread)
        {
            if (spread.ShouldShoot(this))   // benutzt enemy.nextSpreadTime
            {
                spread.FireSpread(this);
            }
            return;
        }

        // -------- PHASE / NORMAL SCHIESSEN --------
        bool shouldShoot = data.shootingPattern.ShouldShoot(this);

        if (!shouldShoot)
            return;

        // Fire rate check
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + data.fireRate; 
        }
    }





    private void Shoot()
    {
        Projectile p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        
        p.data = data.projectileData;
        
        p.Init(Vector2.left);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if(currentHealth <= 0)
            Die();
            
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public Transform GetPlayer()
    {
        return player;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (data.isKamikaze && other.TryGetComponent(out PlayerHealth player))
        {
            player.TakeDamage(data.contactDamage);
            Die();
        }
    }

}
