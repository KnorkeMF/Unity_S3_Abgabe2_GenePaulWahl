using System;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Types")]
    public EnemyBehavior enemyPrefab;
    public EnemyData[] enemyTypes;
    
    [Header("Spawner Settings")]
    public Vector2 spawnYRange = new Vector2(-3f, 3f);
    public float spawnXOffset = 10f;
    
    [Header("Difficulty")]
    public float difficulty = 1f;
    public float difficultyIncreasePerSecond = 0.1f;

    public float baseSpawnInterval = 2f;
    public float minSpawnInterval = 0.5f;
    
    private float timer;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        // Difficulty wächst mit der Zeit
        difficulty += difficultyIncreasePerSecond * Time.deltaTime;

        // aktuelles Spawnintervall berechnen
        float currentSpawnInterval = Mathf.Max(
            minSpawnInterval,
            baseSpawnInterval - (difficulty * 0.1f)
        );

        if (timer >= currentSpawnInterval)
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
        
        EnemyData chosenType = GetEnemyBasedOnDifficulty();
        
        float y = UnityEngine.Random.Range(spawnYRange.x, spawnYRange.y);
        float x = cam.transform.position.x + spawnXOffset;

        Vector3 spawnPos = new Vector3(x, y, 0f);
        
        EnemyBehavior enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.data = chosenType;
    }

    private EnemyData GetEnemyBasedOnDifficulty()
    {
        float maxWeight = difficulty;

        List<EnemyData> possible = new List<EnemyData>();

        foreach (var e in enemyTypes)
        {
            if (e.weight <= maxWeight)
                possible.Add(e);
        }

        if (possible.Count == 0)
            possible.AddRange(enemyTypes);

        return possible[UnityEngine.Random.Range(0, possible.Count)];
    }
}
