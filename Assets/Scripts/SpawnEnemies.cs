using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int numberOfEnemies = 5;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private GameObject wall;

    private List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Spawn some enemies among the spawn points
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Get a random spawn point
            int randomSpawnPoint = Random.Range(0, spawnPoints.Count);

            // Spawn the enemy
            GameObject spawnedEnemy = Instantiate(enemy, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            enemies.Add(spawnedEnemy);

            // TP the enemy to the ground with a raycast
            RaycastHit hit;
            if (Physics.Raycast(spawnPoints[randomSpawnPoint].position, Vector3.down, out hit))
            {
                float size = spawnedEnemy.GetComponent<Collider>().bounds.size.y;
                spawnedEnemy.transform.position = new Vector3(spawnedEnemy.transform.position.x, hit.point.y + size / 2 + .25f, spawnedEnemy.transform.position.z);
            }

            // Remove the spawn point from the list
            spawnPoints.RemoveAt(randomSpawnPoint);
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
