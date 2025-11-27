using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string name;
    
    [Header("Stats")]
    public int maxHealth = 10;
    public float speed = 2f;
    public int points = 100;

    [Header("Behaviour")] 
    public EnemyMovementSO movementPattern;
    public EnemyShootingSO shootingPattern;
    
    [Header("Shooting Settings")]
    public float fireRate = 0.3f;
    public ProjectileData projectileData;

    
    [Header("Visuals")]
    public Sprite sprite;
    public bool flipSpriteX = false;
    
    [Header(" Entry Settings")]
    public float entrySpeed = 3f;
    
    [Header("Kamikaze Settings")]
    public bool isKamikaze = false;
    public int contactDamage = 1;
    
    [Header("Difficulty Settings")]
    public float weight = 1f;


}
