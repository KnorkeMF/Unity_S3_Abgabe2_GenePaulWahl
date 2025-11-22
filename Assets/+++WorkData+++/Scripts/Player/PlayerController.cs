using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;

    [Header("Movement Limits")]
    [Range(0.1f, 0.5f)]
    public float horizontalLimitPercent = 0.30f;  
    public float verticalPadding = 0.3f;
    public float horizontalPadding = 0.5f;

    private PlayerInputActions input;
    private Vector2 moveInput;

    private Camera cam;

    private float minX, maxX, minY, maxY;

    private void Awake()
    {
        input = new PlayerInputActions();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();

    private void Start()
    {
        cam = Camera.main;
        CalculateBounds();
    }

    private void Update()
    {
        Move();
        ClampPosition();
    }

    private void Move()
    {
        Vector3 delta = new Vector3(moveInput.x, moveInput.y, 0f) * moveSpeed * Time.deltaTime;
        transform.position += delta;
    }

    private void CalculateBounds()
    {
        float distZ = Mathf.Abs(cam.transform.position.z - transform.position.z);

        Vector3 screenMin = cam.ScreenToWorldPoint(new Vector3(0, 0, distZ));
        Vector3 screenMax = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, distZ));
        
        maxX = Mathf.Lerp(screenMin.x, screenMax.x, horizontalLimitPercent);
        
        minX = screenMin.x + horizontalPadding;
        minY = screenMin.y + verticalPadding;
        maxY = screenMax.y - verticalPadding;
    }

    private void ClampPosition()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
