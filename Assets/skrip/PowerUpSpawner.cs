using System.Collections;
using UnityEngine;
using Alteruna;

public class PowerUpSpawner : AttributesSync
{
    public GameObject[] powerUpPrefabs;
    public float spawnInterval = 15f;
    public GameObject spawnAreaObj;
    public Collider2D spawnArea;

    [SynchronizableField] private Vector2 spawnPosition;

    private void Start()
    {
        BroadcastRemoteMethod("StartSpawnPowerUpRoutine");
        spawnAreaObj = GameObject.Find("PowerUpSpawner");
        spawnArea = spawnAreaObj.GetComponent<Collider2D>();
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0, spawnInterval / 2));

        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Check if a GameObject named "PowerUp" exists in the scene
            if (GameObject.Find("PowerUp") == null)
            {
                Vector2 randomPosition = GetRandomPositionInArea();

                spawnPosition = randomPosition;
                BroadcastRemoteMethod("OnSpawnPositionChanged", spawnPosition);
            }
        }
    }

    [SynchronizableMethod]
    private void OnSpawnPositionChanged(Vector2 newPosition)
    {
        if (GameObject.Find("PowerUp") == null)
        {
            SpawnRandomPowerUp(newPosition);
        }
    }

    private void SpawnRandomPowerUp(Vector2 spawnPosition)
    {
        if (powerUpPrefabs.Length == 0 || spawnArea == null)
        {
            Debug.LogWarning("No power-up prefabs or spawn area assigned!");
            return;
        }

        GameObject selectedPowerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        GameObject spawnedPowerUp = Instantiate(selectedPowerUp, spawnPosition, Quaternion.identity);
        spawnedPowerUp.name = "PowerUp"; // Ensure the name matches for checking
    }

    private Vector2 GetRandomPositionInArea()
    {
        if (spawnArea == null)
        {
            Debug.LogError("Spawn area collider is not assigned.");
            return Vector2.zero;
        }

        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }

    [SynchronizableMethod]
    private void StartSpawnPowerUpRoutine()
    {
        StartCoroutine(SpawnPowerUpRoutine());
    }
}
