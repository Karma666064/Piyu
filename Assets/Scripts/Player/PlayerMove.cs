using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerSystemAction inputActions;

    private Vector2 moveInput;

    [SerializeField] private float speed = 9f;
    [SerializeField] private float acceleration = 4f;

    [SerializeField] private bool isFacingRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inputActions = new PlayerSystemAction();
        inputActions.Enable();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = Mathf.Lerp(velocity.x, moveInput.x * speed, Time.fixedDeltaTime * acceleration);
        rb.linearVelocity = velocity;

        // Regard du personnage 
        if (moveInput.x > 0.1f) isFacingRight = true;
        else if (moveInput.x < -0.1f) isFacingRight = false;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public float GetSpeed()
    {
        return speed;
    }
}
