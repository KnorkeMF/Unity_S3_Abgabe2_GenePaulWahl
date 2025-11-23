using UnityEngine;

public class ShieldBubble : MonoBehaviour
{
    public float duration = 2f;
    
    private float timer = 0f;
    private PlayerHealth playerHealth;


    void Awake()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }
    public void Activate()
    {
        timer = duration;
        gameObject.SetActive(true);
        playerHealth.isInvulnerable = true;
    }
    
    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            playerHealth.isInvulnerable = false;
            gameObject.SetActive(false);
        }
    }
}
