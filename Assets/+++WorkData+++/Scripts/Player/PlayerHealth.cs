using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public GameObject deathExplosionPrefab;

    public bool isInvulnerable = false;

    public event System.Action<int, int> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable || currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
            StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        GetComponent<PlayerShooting>().enabled = false;
        GetComponent<PlayerShield>().enabled = false;

        
        yield return new WaitForSeconds(1.5f);
        
        ScoreManager.Instance.SaveHighscore();
        GameOverUI.Instance.Show(ScoreManager.Instance.score);
        
        Time.timeScale = 0f;
    }
}