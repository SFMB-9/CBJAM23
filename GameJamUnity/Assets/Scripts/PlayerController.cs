using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    bool IsMoving {
        set {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }
    public float movementSpeed = 3f;
    public float maxSpeed = 1f;
    public float lerpFriction = 0.1f;
    Vector2 movementInput = Vector2.zero;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool canMove = true; // start game idling
    bool isMoving = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        // Check if player can move and input isn't 0
        if(movementInput != Vector2.zero && canMove == true) { 

            // Player accelerates when running, but can't go faster than maxSpeed
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movementInput * movementSpeed * Time.deltaTime), maxSpeed);

            // Set animation parameters for movement behavior tree
            animator.SetFloat("HorizontalMovement", movementInput.x);
            animator.SetFloat("VerticalMovement", movementInput.y);
            // animator.SetFloat("Speed", rb.velocity.sqrMagnitude);

            // Ser movement state
            IsMoving = true;

        } else {
            // Lerp velocity when not moving
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, lerpFriction);
            IsMoving = false;
        }
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    void LockMovement() {
        canMove = false;
    }
    void UnlockMovement() {
        canMove = true;
    }
}