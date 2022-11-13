using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 1f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private GameObject weaponParent;
    [SerializeField] private float weaponMovement = 20f;

    public float xRotation = 0f;

    void Update()
    {
        // Get the mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Set boundaries for the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Only in-game (not in the menus)
        if (Time.timeScale == 1)
        {
            // Rotate the camera
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotate the player
            playerBody.Rotate(Vector3.up * mouseX);
        }

        // Smoothly rotate the weapon along the mouseX input
        // Only execute in Build
        Quaternion desiredRotation = Quaternion.Euler(mouseX * weaponMovement * .5f, 90 - mouseX * weaponMovement, mouseY * weaponMovement);
        weaponParent.transform.localRotation = Quaternion.Lerp(weaponParent.transform.localRotation, desiredRotation, .03f);
    }

    private void OnDrawGizmos()
    {
        // Draw a line to represent the player's sight
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 5);
    }
}
