using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject audioSource;
    [SerializeField] private Transform playerBody;
    [SerializeField] private GameObject projectile;

    public float fireRate = 0.5f;
    public float damage = 40f;
    public bool isAutomatic = false;
    public bool canShoot = false;
    public AudioClip shotSound;
    public GameObject muzzleFlashSpawn;

    void Start()
    {
        // Start the coroutine
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            // Wait for the input
            if (isAutomatic) yield return new WaitUntil(() => Input.GetButton("Fire1"));
            else yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));

            // Shoot the bullet
            Shoot();

            // Wait for the cooldown
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Shoot()
    {
        if (!canShoot) return;

        // Play the shoot sound
        audioSource.GetComponent<AudioSource>().PlayOneShot(shotSound);

        // Create a raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Show the raycast
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

        // Check if the raycast hits something
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit an enemy
            if (hit.transform.tag == "Enemy")
            {
                // Deal damage to the enemy
                hit.transform.GetComponent<EnemyBehaviour>().TakeDamage(damage);
            }
            else
            {
                // Draw an bullet hole texture on the hit object
                DrawBulletHole(hit);
            }
        }

        // Play the muzzle flash as child of the muzzle flash spawn
        GameObject flash = Instantiate(muzzleFlash, muzzleFlashSpawn.transform.position, muzzleFlashSpawn.transform.rotation);
        flash.transform.parent = muzzleFlashSpawn.transform;

        // Destroy the muzzle flash after 0.5 seconds
        Destroy(flash, 0.1f);

        // Change xRotation of the camera
        Camera.main.GetComponent<MouseLook>().xRotation -= Random.Range(0.2f, 0.4f);
        playerBody.Rotate(0, Random.Range(-0.15f, 0.15f), 0);

        // Quickly create and translate the projectile from the muzzle flash spawn to the hit point
        GameObject projectileObject = Instantiate(projectile, muzzleFlashSpawn.transform.position, Camera.main.transform.rotation);
        projectileObject.transform.Translate(Vector3.forward * 1.3f);
        projectileObject.transform.Rotate(90, 0, 0);
        // Give the projectile a rigidbody
        Rigidbody projectileRigidbody = projectileObject.AddComponent<Rigidbody>();
        projectileRigidbody.useGravity = false;

        // Add force to the projectile
        projectileRigidbody.AddForce((ray.GetPoint(30) - muzzleFlashSpawn.transform.position) * 300);
        Destroy(projectileObject, 0.2f);
    }

    private void DrawBulletHole(RaycastHit hit)
    {
        // Clone bulletHole
        GameObject clone = Instantiate(bulletHole, hit.point + hit.normal * 0.001f, Quaternion.FromToRotation(Vector3.back, hit.normal));

        // Destroy the bullet hole after 5 seconds
        Destroy(clone, 5f);
    }
}
