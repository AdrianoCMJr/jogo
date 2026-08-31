using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;

    public float spawnTime = 2f;
    public float spawnDistance = 10f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnTime);
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnDistance;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}