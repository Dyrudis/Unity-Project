using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Camera cam;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float crouchSpeed = 3f;
    [SerializeField] private float crouchDelay = 10f;
    [SerializeField] private float FOV = 50f;
    [SerializeField] private float sprintFOV = 60f;
    [SerializeField] private float FOVDelay = 10f;
    [SerializeField] private float friction = 9f;
    [SerializeField] private float mass = 3f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    Vector3 velocity;
    Vector3 currentVelocity;
    bool isGrounded;
    bool isCrouch = false;
    float currentSpeed;

    void Start()
    {
        currentSpeed = speed;
        cam.fieldOfView = FOV;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // Crouch
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            currentSpeed = crouchSpeed;
            isCrouch = true;
            cam.transform.localPosition = Mathf.Lerp(cam.transform.localPosition.y, 0.25f, Time.deltaTime * crouchDelay) * Vector3.up;
        }
        else if (isGrounded)
        {
            currentSpeed = speed;
            isCrouch = false;
            cam.transform.localPosition = Mathf.Lerp(cam.transform.localPosition.y, 0.5f, Time.deltaTime * crouchDelay) * Vector3.up;
        }

        // Sprint
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && (velocity.x != 0 || velocity.z != 0) && !isCrouch)
        {
            currentSpeed = sprintSpeed;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, FOVDelay * Time.deltaTime);
        }
        else if (isGrounded && !isCrouch)
        {
            currentSpeed = speed;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, FOV, FOVDelay * Time.deltaTime);
        }
        else if (isGrounded)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, FOV, FOVDelay * Time.deltaTime);
        }

        // Normal movements
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = (transform.right * x + transform.forward * z).normalized;

        // Apply friction only on x and z axis (not y)
        velocity.x = moveDirection.x * currentSpeed;
        velocity.z = moveDirection.z * currentSpeed;
        currentVelocity = Vector3.Lerp(currentVelocity, velocity, friction * Time.deltaTime);
        currentVelocity.y = velocity.y;

        // Reset the y velocity if grounded
        if (isGrounded && velocity.y < -2f)
        {
            velocity.y = -2f;
        }

        // Jumps
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * mass * Time.deltaTime;

        controller.Move(currentVelocity * Time.deltaTime);

        // Reset the scene when pressing R
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}