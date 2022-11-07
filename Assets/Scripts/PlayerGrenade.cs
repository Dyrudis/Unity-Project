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
    [SerializeField] private float grenadeRotation = 5f;

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

        Debug.Log(grenadeSpawn.transform.up);

        // Add rotation force
        grenadeRigidbody.AddTorque(transform.right * grenadeRotation, ForceMode.Impulse);

        // Destroy the grenade after 5 seconds
        Destroy(grenade, 5f);
    }
}
