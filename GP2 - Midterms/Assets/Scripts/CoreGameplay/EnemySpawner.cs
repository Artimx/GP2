
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform targetPoint;

    [Header("Spawn Timing")]
    [SerializeField] private float spawnInterval = 3f;

    private float spawnTimer;

    private void Start()
    {
        SpawnEnemy();
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!", this);
            return;
        }

        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs assigned!", this);
            return;
        }

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        if (spawnPoint == null)
            return;

        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.y = 1.95f;

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject chosenPrefab = enemyPrefabs[enemyIndex];

        if (chosenPrefab == null)
            return;

        GameObject spawnedEnemy = Instantiate(
            chosenPrefab,
            spawnPosition,
            spawnPoint.rotation
        );

        EnemyBase enemy = spawnedEnemy.GetComponent<EnemyBase>();

        if (enemy != null && targetPoint != null)
        {
            enemy.SetTarget(targetPoint);
        }
    }
}
