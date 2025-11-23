using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Types")]
    public EnemyBehavior enemyPrefab;
    public EnemyData[] enemyTypes;
    
    [Header("Spawner Settings")]
    public float spawnInterval = 2f;
    public Vector2 spawnYRange = new Vector2(-3f, 3f);
    public float spawnXOffset = 10f;
    
    private float timer;
    
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyTypes.Length == 0)
        {
            Debug.LogWarning("No enemy types assigned!");
            return;
        }
        
        EnemyData chosenType = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Length)];
        
        float y = UnityEngine.Random.Range(spawnYRange.x, spawnYRange.y);
        float x = cam.transform.position.x + spawnXOffset;

        Vector3 spawnPos = new Vector3(x, y, 0f);
        
        EnemyBehavior enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        
        enemy.data = chosenType;
    }
}