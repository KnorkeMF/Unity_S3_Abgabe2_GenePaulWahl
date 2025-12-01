using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShield : MonoBehaviour
{
    public ShieldBubble shieldPrefab;
    public Transform shieldHolder;
    
    public float cooldown = 5f;
    public float cooldownTimer = 0f;
    
    private ShieldBubble activeShield;
    private PlayerInputActions input;

    void Awake()
    {
        input = new PlayerInputActions();

        input.Player.Shield.started += ctx => TryActivateShield();
    }
    
    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();
    
    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    void TryActivateShield()
    {
        if (cooldownTimer > 0f)
        {
            return;
        }

        if (activeShield == null)
        {
            activeShield = Instantiate(shieldPrefab, shieldHolder.position, Quaternion.identity, shieldHolder);
        }
        activeShield.Activate();
        cooldownTimer = cooldown;
    }
}
