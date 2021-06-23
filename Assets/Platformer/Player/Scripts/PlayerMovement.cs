using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    
    // Prevent non-singleton constructor use.
    protected PlayerMovement() { }

    bool isEnabled = true;

    [Header("Components")]
    Rigidbody2D rb;
    Animator animator;

    [Header("Movement Variables")]
    [SerializeField] float movementAcceleration = 50f;
    [SerializeField] float maxMoveSpeed = 12f;
    [SerializeField] float groundLinearDrag = 10f;
    [SerializeField] float groundLinearDragThreshold = 0.4f;
    bool canMove => isEnabled && !isDashing;

    [Header("Jump Variables")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float airLinearDrag = 0.2f;
    [SerializeField] float fallMultiplier = 8f;
    [SerializeField] float lowJumpFallMultiplier = 5f;
    [SerializeField] int maxJumpsAllowed = 2;
    [SerializeField] float jumpTime = .1f;
    bool canJump => (jumpsCounter < maxJumpsAllowed) && isEnabled;
    bool willJump => Input.GetButtonDown("Jump") && canJump;
    bool stillJumping => Input.GetButton("Jump") && isJumping;
    int jumpsCounter = 0;
    bool isJumping;
    float jumpTimeCounter;

    [Header("Dash Variables")]
    [SerializeField] float dashSpeed = 100f;
    [SerializeField] float dashTime = .1f;
    [SerializeField] int maxDashesAllowed = 1;
    float dashTimeCounter = 0f;
    int dashesCounter = 0;
    Vector2 dashDirection;
    bool isDashing = false;
    bool canDash => !isDashing && (dashesCounter < maxDashesAllowed) && isEnabled;
    bool willDash => Input.GetButtonDown("Dash") && canDash;

    [Header("Layer Masks")]
    [SerializeField] LayerMask groundLayer;

    [Header("Ground Collision Variables")]
    [SerializeField] float groundRaycastLength = 2.7f;
    bool onGround;

    Vector2 direction;
    bool changingDirection => (rb.velocity.x > 0f && direction.x < 0f) ||
                              (rb.velocity.x < 0f && direction.x > 0f);

    PlatformerRoom currentRoom;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateForDash();
        UpdateForJump();
    }

    void UpdateForDash() {
        if (willDash) {
            isDashing = true;
            dashDirection = direction;        
            dashesCounter++;
        }
        else if (onGround) {
            dashesCounter = 0;
        }

        if (isDashing) {
            if (dashTimeCounter >= dashTime) {
                dashTimeCounter = 0;
                isDashing = false;
                rb.velocity = Vector2.zero;
            } else {
                dashTimeCounter += Time.deltaTime;
                rb.velocity = dashDirection * dashSpeed;
            }
        }
    }

    void UpdateForJump()
    {
        if (willJump) {
            jumpsCounter++;
            Jump();
        }
        else if (onGround) {
            jumpsCounter = 0;
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
        direction = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (canMove) {
            MoveCharacter();
        }

        if (onGround) {
            ApplyGroundLinearDrag();
        } else {
            ApplyAirLinearDrag();
            FallMultiplier();
        }

        UpdateAnimatorState();
    }

    void MoveCharacter() {
        rb.AddForce(new Vector2(direction.x, 0f) * movementAcceleration);

        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed) {
            rb.velocity = new Vector2(
                Mathf.Sign(rb.velocity.x) * maxMoveSpeed,
                rb.velocity.y
            );
        }

        if (changingDirection) {
            int orientation = direction.x < 0 ? 180 : 0;
            transform.rotation = Quaternion.Euler(0, orientation, 0);
        }
    }

    void UpdateAnimatorState() {
        animator.SetBool("isFalling", rb.velocity.y < -0.1f);
        animator.SetBool("isJumping", rb.velocity.y > 0.1f);
        animator.SetBool("isMoving", rb.velocity.x != 0);
    }

    void ApplyGroundLinearDrag() {
        if (Mathf.Abs(direction.x) < groundLinearDragThreshold || changingDirection) {
            rb.drag = groundLinearDrag;
        } else {
            rb.drag = 0f;
        }
    }

    void ApplyAirLinearDrag() {
        rb.drag = airLinearDrag;
    }

    void Jump() {
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

    public void EnableMovements() {
        isEnabled = true;
    }

    public void DisableMovements() {
        isEnabled = false;
    }

    public void SetCurrentRoom(PlatformerRoom room) {
        currentRoom = room;
    }

    public PlatformerRoom GetCurrentRoom() {
        return currentRoom;
    }
}
