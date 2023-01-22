using System;
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

    [Header("Input Settings")] 
    [SerializeField] private InputActionReference bite;
    
    [Header("Movement Parameters")]
    public float movementSpeed = 3f;
    public float maxSpeed = 1f;
    public float lerpFriction = 0.1f;
    
    [Header("Bite Parameters")]
    public float biteRange = 1.5f;
    public LayerMask interactableLayer;
    public LayerMask obstructionMask;


    Vector2 movementInput = Vector2.zero;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool canMove = true; // start game idling
    bool isMoving = false;
    private bool canInteract;
    private Transform target;
    
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FOVroutine());
    }

    private void OnEnable()
    {
        bite.action.performed += Bite;
    }

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
    
    private void Bite(InputAction.CallbackContext obj)
    {
        if (!canInteract) return;
        if (target.CompareTag("NPC"))
        {
            target.GetComponent<NPCController>().Infect();
        }
        animator.SetTrigger("biteAttack");
    }
    void LockMovement() {
        canMove = false;
    }
    void UnlockMovement() {
        canMove = true;
    }
    
    private IEnumerator FOVroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    
    private void FieldOfViewCheck()
    {
        float range = biteRange;
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, biteRange, interactableLayer);
        
        if (rangeChecks.Length != 0)
        {
            target = rangeChecks[0].transform;
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= range)
            {
                if (Physics2D.Raycast(transform.position, directionToTarget, obstructionMask))
                    canInteract = true;
                else
                    canInteract = false;
            }
        }
        else
        {
            canInteract = false;
        }
    }
    
    // FOV gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, biteRange);

        if (canInteract)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, (target.transform.position - transform.position).normalized * biteRange);    
        }
    }
}