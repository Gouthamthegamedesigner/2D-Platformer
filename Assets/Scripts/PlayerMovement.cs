using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public FixedJoystick joystick;
    public float moveSpeed = 5f; // Speed for horizontal movement
    public float jumpForce = 5f; // Force applied when jumping
    private bool isGrounded = true; // To check if the player is on the ground

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void FixedUpdate()
    {
        // Horizontal movement
        float hInput = joystick.Horizontal * moveSpeed;
        rb.velocity = new Vector2(hInput, rb.velocity.y); // Apply horizontal movement

        // Jumping
        if (joystick.Vertical > 0.5f && isGrounded) // Jump only if the joystick is pushed upwards and the player is grounded
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Directly set vertical velocity for jump
            isGrounded = false; // Prevent jumping mid-air
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detect if the player is back on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
