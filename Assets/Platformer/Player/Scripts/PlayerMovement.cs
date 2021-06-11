using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D rb;
    Animator animator;

    [Header("Movement Variables")]
    [SerializeField] float movementAcceleration = 50f;
    [SerializeField] float maxMoveSpeed = 12f;
    [SerializeField] float groundLinearDrag = 10f;
    [SerializeField] float groundLinearDragThreshold = 0.4f;

    [Header("Jump Variables")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float airLinearDrag = 0.2f;
    [SerializeField] float fallMultiplier = 8f;
    [SerializeField] float lowJumpFallMultiplier = 5f;
    [SerializeField] int extraJumps = 1;
    [SerializeField] float jumpTime = .1f;
    bool canJump => Input.GetButtonDown("Jump") && (onGround || extraJumpsValue > 0);
    bool stillJumping => Input.GetButton("Jump") && isJumping;
    int extraJumpsValue;
    bool isJumping;
    float jumpTimeCounter;

    [Header("Layer Masks")]
    [SerializeField] LayerMask groundLayer;

    [Header("Ground Collision Variables")]
    [SerializeField] float groundRaycastLength = 2.7f;
    bool onGround;

    float horizontalDirection;
    bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) ||
                              (rb.velocity.x < 0f && horizontalDirection > 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canJump) {
            Jump();
        }
        if (stillJumping) {
            IncreaseJumping();
        }
        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
        }
    }

    void FixedUpdate() {
        CheckCollisions();

        horizontalDirection = Input.GetAxisRaw("Horizontal");
        MoveCharacter();
        UpdateAnimatorState();

        if (onGround) {
            ApplyGroundLinearDrag();
        } else {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
    }

    void MoveCharacter() {
        rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);

        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed) {
            rb.velocity = new Vector2(
                Mathf.Sign(rb.velocity.x) * maxMoveSpeed,
                rb.velocity.y
            );
        }

        if (changingDirection) {
            int direction = horizontalDirection < 0 ? 180 : 0;
            transform.rotation = Quaternion.Euler(0, direction, 0);
        }
    }

    void UpdateAnimatorState() {
        animator.SetBool("isFalling", rb.velocity.y < -0.1f);
        animator.SetBool("isJumping", rb.velocity.y > 0.1f);
        animator.SetBool("isMoving", rb.velocity.x != 0);
    }

    void ApplyGroundLinearDrag() {
        if (Mathf.Abs(horizontalDirection) < groundLinearDragThreshold || changingDirection) {
            rb.drag = groundLinearDrag;
        } else {
            rb.drag = 0f;
        }
    }

    void ApplyAirLinearDrag() {
        rb.drag = airLinearDrag;
    }

    void Jump() {
        if (!onGround) {
            extraJumpsValue--;
        } else {
            extraJumpsValue = extraJumps;
        }

        isJumping = true;
        jumpTimeCounter = jumpTime;

        rb.velocity = new Vector2(
            rb.velocity.x, 0f
        );
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void IncreaseJumping() {
        if (jumpTimeCounter > 0) {
            rb.velocity = new Vector2(
                rb.velocity.x,
                (Vector2.up * jumpForce).y
            );
            jumpTimeCounter -= Time.deltaTime;
        } else {
            isJumping = false;
        }
    }

    void CheckCollisions() {
        onGround = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundRaycastLength,
            groundLayer
        );
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position,
            transform.position + Vector3.down * groundRaycastLength
        );
    }

    void FallMultiplier() {
        if (rb.velocity.y < 0) {
            rb.gravityScale = fallMultiplier;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.gravityScale = lowJumpFallMultiplier;
        } else {
            rb.gravityScale = 1f;
        }
    }
}
