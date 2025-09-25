using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerSystemAction inputActions;

    private PlayerMove playerMove;

    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isTouchingWall;

    [SerializeField] private bool jumpHeld;
    [SerializeField] private float jumpForce = 7f;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.8f, 0.2f);
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public Vector2 wallCheckSize = new Vector2(0.8f, 0.2f);
    public Transform wallCheck;
    public LayerMask climbLayer;

    private float verticalMoveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();

        inputActions = new PlayerSystemAction();
        inputActions.Enable();

        inputActions.Player.Jump.started += OnJumpStart;
        inputActions.Player.Jump.canceled += OnJumpCancel;

        inputActions.Player.Move.performed += OnClimbStart;
        inputActions.Player.Move.canceled += OnClimbCancel;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
        isTouchingWall = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0f, climbLayer);

        if (rb.linearVelocity.y < 0)
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        else if (rb.linearVelocity.y > 0 && !jumpHeld)
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;

        // Coyote time
        if (isGrounded) coyoteTimeCounter = coyoteTime;
        else coyoteTimeCounter -= Time.fixedDeltaTime;

        // Jump Buffer
        if (jumpBufferCounter > 0) jumpBufferCounter -= Time.fixedDeltaTime;

        // Jump
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0;
        }

        if (isTouchingWall)
        {
            if (Mathf.Abs(verticalMoveInput) > 0) // climbing
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalMoveInput * playerMove.GetSpeed());
                rb.gravityScale = 0f;
            }
            else // sliding
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -2);
                rb.gravityScale = 0f;
            }
        }
        else
        {
            rb.gravityScale = 1f;
        }

        if (isTouchingWall && jumpBufferCounter > 0)
        {
            rb.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * jumpForce, jumpForce);
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
            rb.gravityScale = 1f;
        }
    }

    void OnJumpStart(InputAction.CallbackContext context)
    {
        jumpBufferCounter = jumpBufferTime;
        jumpHeld = true;
    }

    void OnJumpCancel(InputAction.CallbackContext context)
    {
        jumpHeld = false;
    }

    void OnClimbStart(InputAction.CallbackContext context)
    {
        verticalMoveInput = context.ReadValue<Vector2>().y;
    }

    void OnClimbCancel(InputAction.CallbackContext context)
    {
        verticalMoveInput = 0f;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
            Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
        }
    }
}
