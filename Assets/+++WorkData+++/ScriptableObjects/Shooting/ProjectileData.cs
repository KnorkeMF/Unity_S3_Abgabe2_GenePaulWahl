using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public float speed = 12f;
    public float lifetime = 2f;
    public int damage = 1;
    
    public bool isAOE = false;
    public float explosionRadius = 2f;
    public int explosionDamage = 10;
    public GameObject explosionPrefab;
    
    public bool isPlayerProjectile = false;
}
