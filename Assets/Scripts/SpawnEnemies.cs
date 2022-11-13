using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject wall;

    private List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn some enemies among the spawn points
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Get a random spawn point
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            // Spawn the enemy
            enemies.Add(Instantiate(enemy, spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation));

            // Destroy the spawn point
            Destroy(spawnPoints[randomSpawnPoint].gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all enemies are dead
        if (enemies.Count == 0)
        {
            // Destroy the wall
            Destroy(wall);
        }
    }
}
