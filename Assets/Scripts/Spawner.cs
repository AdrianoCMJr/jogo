using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnRate = 2f;
    public float minSpawnRate = 0.3f;

    public float spawnDistance = 10f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
        InvokeRepeating(nameof(IncreaseDifficulty), 10f, 10f);
    }

    void SpawnEnemy()
    {
        if (player == null)
            return;

        // Escolhe uma direção aleatória ao redor do jogador
        Vector2 direction = Random.insideUnitCircle.normalized;

        // Define onde o inimigo vai nascer
        Vector2 pos = (Vector2)player.position + direction * spawnDistance;

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    void IncreaseDifficulty()
    {
        spawnRate -= 0.2f;

        if (spawnRate < minSpawnRate)
            spawnRate = minSpawnRate;

        CancelInvoke(nameof(SpawnEnemy));
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnRate);

        Debug.Log("Nova taxa: " + spawnRate);
    }
}