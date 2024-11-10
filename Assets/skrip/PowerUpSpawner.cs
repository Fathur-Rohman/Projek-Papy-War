using System.Collections;
using UnityEngine;
using Alteruna;

public class PowerUpSpawner : AttributesSync
{
    public GameObject[] powerUpPrefabs;    // Array of power-up prefabs to spawn
    public float spawnInterval = 15f;      // Interval between spawns
    public Transform spawnArea;            // Transform that defines the spawn area bounds

    private void Start()
    {
        if (IsServer()) // Only the server or host handles spawning
        {
            StartCoroutine(SpawnPowerUpRoutine());
        }
    }

    private IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            BroadcastRemoteMethod("SpawnRandomPowerUp"); // Call spawn method on all clients
        }
    }

    [SynchronizableMethod]
    private void SpawnRandomPowerUp()
    {
        if (powerUpPrefabs.Length == 0 || spawnArea == null)
        {
            Debug.LogWarning("No power-up prefabs or spawn area assigned!");
            return;
        }

        // Randomly select a power-up type
        GameObject selectedPowerUp = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        
        // Calculate a random position within the spawn area
        Vector2 spawnPosition = GetRandomPositionInArea();

        // Spawn the selected power-up at the chosen position
        Instantiate(selectedPowerUp, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetRandomPositionInArea()
    {
        // Get the boundaries of the spawn area
        Vector2 areaMin = (Vector2)spawnArea.position - (Vector2)spawnArea.localScale / 2;
        Vector2 areaMax = (Vector2)spawnArea.position + (Vector2)spawnArea.localScale / 2;

        // Calculate a random position within the boundaries
        float randomX = Random.Range(areaMin.x, areaMax.x);
        float randomY = Random.Range(areaMin.y, areaMax.y);

        return new Vector2(randomX, randomY);
    }

    private bool IsServer()
    {
        // Check if this client is the server or host
        Alteruna.Avatar avatar = GetComponent<Alteruna.Avatar>();
        return avatar != null && avatar.IsMe;
    }
}
