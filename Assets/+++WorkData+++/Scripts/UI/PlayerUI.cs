using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Health UI")]
    public Image healthFill;
    private PlayerHealth playerHealth;

    [Header("Shield UI")]
    public Image shieldFill;
    private PlayerShield playerShield;

    [Header("Missile UI")]
    public Image missileFill;
    private PlayerShooting playerShooting;

    
    void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        playerShield = FindAnyObjectByType<PlayerShield>();
        playerShooting = FindAnyObjectByType<PlayerShooting>();
    }

    
    void Update()
    {
        UpdateHealthUI();
        UpdateShieldUI();
        UpdateMissileUI();
    }

    // ---------------- HEALTH -----------------

    private void UpdateHealthUI()
    {
        float t = (float)playerHealth.currentHealth / playerHealth.maxHealth;
        healthFill.fillAmount = t;
    }

    
    // ---------------- SHIELD -----------------

    private void UpdateShieldUI()
    {
        float cd = playerShield.cooldownTimer;  
        float max = playerShield.cooldown;

        float fill = 1f - Mathf.Clamp01(cd / max); 
        shieldFill.fillAmount = fill;
    }

    
    // ---------------- MISSILE -----------------

    private void UpdateMissileUI()
    {
        float max = playerShooting.missileCooldown;

        // Zeit seit letztem Missile-Use
        float elapsed = Mathf.Clamp(
            playerShooting.missileCooldown - (playerShooting.nextMissileTime - Time.time),
            0f,
            max
        );

        float fill = elapsed / max;
        missileFill.fillAmount = fill;
    }
}