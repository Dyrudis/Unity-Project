using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private GameObject grenadeSpawn;
    [SerializeField] private float grenadeVerticalForce = 10f;
    [SerializeField] private float grenadeHorizontalForce = 10f;
    [SerializeField] private float grenadeCooldown = 1f;
    [SerializeField] private float grenadeDelay = 3f;
    [SerializeField] private float grenadeRotation = 5f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioClip grenadeSound;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine
        StartCoroutine(ThrowCoroutine());
    }

    private IEnumerator ThrowCoroutine()
    {
        while (true)
        {
            // Wait for the input
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

            // Throw the grenade
            ThrowGrenade();

            // Wait for the cooldown
            yield return new WaitForSeconds(grenadeCooldown);
        }

    }

    private void ThrowGrenade()
    {
        // Instantiate a grenade at the player's position
        GameObject grenade = Instantiate(grenadePrefab, grenadeSpawn.transform.position, Quaternion.identity);

        // Apply a force to the grenade targeting the mouse position
        Rigidbody grenadeRigidbody = grenade.GetComponent<Rigidbody>();
        Vector3 direction = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 30)) - grenadeSpawn.transform.position);
        direction.Normalize();
        grenadeRigidbody.AddForce(direction * grenadeHorizontalForce + grenadeSpawn.transform.up * grenadeVerticalForce, ForceMode.Impulse);

        // Add rotation force
        grenadeRigidbody.AddTorque(transform.right * grenadeRotation, ForceMode.Impulse);

        // Call the explosion effect
        StartCoroutine(ExplosionEffect(grenade));
    }

    private IEnumerator ExplosionEffect(GameObject grenade)
    {
        // Wait for grenadeDelay seconds
        yield return new WaitForSeconds(grenadeDelay);

        // Play the explosion sound with no volume decrease
        AudioSource.PlayClipAtPoint(grenadeSound, transform.position, 1f);

        // Instantiate the explosion effect
        GameObject explosion = Instantiate(explosionEffect, grenade.transform.position, Quaternion.identity);

        // Check if the grenade hit an enemy
        Collider[] colliders = Physics.OverlapSphere(grenade.transform.position, 7f);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<EnemyBehaviour>().TakeDamage(100f);
            }
        }

        // Destroy the grenade
        Destroy(grenade);

        // Destroy the explosion effect after 3 second
        Destroy(explosion, 3f);
    }
}
