using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float life = 100;

    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);

            // Get the spawn enemies script from the parent
            transform.parent.GetComponent<SpawnEnemies>().RemoveEnemy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    { 
        
    }

    // Update is called once per frame
    void Update()
    {
        // Always look at the player + 90 degrees
        transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));
        transform.Rotate(0, 90, 0);
    }
}
