using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    
    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;
    
    [HideInInspector]
    public bool isInvulnerable = false;
    
    
    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return;
        if(currentHealth <= 0) return;
        
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

    }
    
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void Die()
    {
        Debug.Log("Player died");
        OnDeath?.Invoke();
    }
}
