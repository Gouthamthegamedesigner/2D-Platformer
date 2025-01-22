using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public float moveSpeed; // Speed for horizontal movement
    public float jumpForce; // Force applied when jumping
    private bool isGrounded; // To check if the player is on the ground

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    private void FixedUpdate()
    {
        // Horizontal movement
        float hInput = joystick.Horizontal * moveSpeed;
        Vector3 move = new Vector3(hInput, 0, 0);
        transform.Translate(move * Time.deltaTime);

        // Jumping
        if (joystick.Vertical > 0.5f && isGrounded) // Jump only if the joystick is pushed upwards and the player is grounded
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent jumping mid-air
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detect if the player is back on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
