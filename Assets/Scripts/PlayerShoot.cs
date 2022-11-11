using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject muzzleFlashSpawn;

    public bool canShoot = false;

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
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            // Shoot the bullet
            Shoot();

            // Wait for the cooldown
            yield return new WaitForSeconds(1f / 6.75f);
        }
    }

    private void Shoot()
    {
        if (!canShoot) return;

        // Play the shoot sound
        AudioSource.PlayClipAtPoint(shotSound, transform.position);
        
        // Create a raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Show the raycast
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);

        // Check if the raycast hits something
        if (Physics.Raycast(ray, out hit))
        {
            // Draw an bullet hole texture on the hit object
            DrawBulletHole(hit);
        }

        // Play the shoot animation
        gunAnimator.Play("Gun Shot");

        // Play the muzzle flash as child of the muzzle flash spawn
        GameObject flash = Instantiate(muzzleFlash, muzzleFlashSpawn.transform.position, muzzleFlashSpawn.transform.rotation);
        flash.transform.parent = muzzleFlashSpawn.transform;

        // Destroy the muzzle flash after 0.5 seconds
        Destroy(flash, 0.5f);
    }

    private void DrawBulletHole(RaycastHit hit)
    {
        // Clone bulletHole
        GameObject clone = Instantiate(bulletHole, hit.point + hit.normal * 0.001f, Quaternion.FromToRotation(Vector3.back, hit.normal));
    }
}
