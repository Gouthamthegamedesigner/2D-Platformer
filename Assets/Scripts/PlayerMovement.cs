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
    private SpriteRenderer spriteRenderer;
    private Animator animator; // Reference to Animator

    private const float movementThreshold = 0.2f; // Threshold for detecting intentional movement

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    private void FixedUpdate()
    {
        // Horizontal movement
        float hInput = joystick.Horizontal * moveSpeed;
        rb.velocity = new Vector2(hInput, rb.velocity.y); // Apply horizontal movement

        // Flip the sprite based on horizontal movement
        if (hInput > 0)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (hInput < 0)
        {
            spriteRenderer.flipX = true; // Facing left
        }

        // Update animation states
        if (Mathf.Abs(joystick.Horizontal) > movementThreshold) // Check joystick input directly
        {
            animator.SetBool("IsRunning", true); // Trigger Run animation
        }
        else
        {
            animator.SetBool("IsRunning", false); // Trigger Idle animation
        }

        // Jumping
        if (joystick.Vertical > 0.5f && isGrounded) // Jump only if the joystick is pushed upwards and the player is grounded
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Directly set vertical velocity for jump
            isGrounded = false; // Prevent jumping mid-air
            animator.SetTrigger("Jump"); // Trigger Jump animation
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
