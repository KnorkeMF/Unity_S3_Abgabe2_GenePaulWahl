using UnityEngine;

public class WorldScrollManager : MonoBehaviour
{
    public static WorldScrollManager Instance;

    [Header("Scroll Speed")]
    public float scrollSpeed = 3f;

    private void Awake()
    {
        Instance = this;
    }
}