using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Setup")]
    public GameObject enemyPrefab;      // Your enemy prefab
    public Transform spawnPoint;        // Optional. If null, uses this spawner's position.
    public float respawnDelay = 3f;     // Seconds to wait before respawn

    private GameObject currentEnemy;
    private bool isRespawning;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        // If the enemy has been destroyed and we're not already waiting, start a respawn
        if (!isRespawning && currentEnemy == null)
        {
            StartCoroutine(RespawnCo());
        }
    }

    void Spawn()
    {
        Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
        currentEnemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    IEnumerator RespawnCo()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnDelay);
        Spawn();
        isRespawning = false;
    }
}
