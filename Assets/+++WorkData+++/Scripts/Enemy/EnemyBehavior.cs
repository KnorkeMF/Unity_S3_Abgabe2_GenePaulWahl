using DG.Tweening;
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
    public GameObject explosionPrefab;
    public GameObject scorePopupPrefab;
    
    private Color originalColor;
    private Tween flashTween;
    private Sequence hitSequence;

    
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
            Destroy(gameObject);
            return;
        }
        
        currentHealth = data.maxHealth;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        
        originalColor = spriteRenderer.color;

        if (data.sprite != null)
        {
            spriteRenderer.sprite = data.sprite;
        }
        
        if (data.flipSpriteX)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
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
        
        PlayHitFeedback();
        
        if(currentHealth <= 0)
            Die();
            
    }

    void Die()
    {
        hitSequence?.Kill();
        flashTween?.Kill();
        
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        
        if (scorePopupPrefab != null)
        {
            GameObject popup = Instantiate(scorePopupPrefab, transform.position, Quaternion.identity);
            popup.GetComponent<ScorePopup>().Show(data.points);
        }
        
        ScoreManager.Instance.AddScore(data.points);
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

    public void PlayHitFeedback()
    {
        if (spriteRenderer == null) return;

        hitSequence?.Kill();
        flashTween?.Kill();

        spriteRenderer.color = originalColor;

        Color hitColor = new Color(1f, 0.3f, 0.3f, 1f); // hellrot

        hitSequence = DOTween.Sequence();
        hitSequence
            .Append(spriteRenderer.DOColor(hitColor, 0.05f))
            .Append(spriteRenderer.DOColor(originalColor, 0.1f));

        hitSequence.Play();
    }




}
