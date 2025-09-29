using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerSystemAction inputActions;

    private Vector2 moveInput;

    [SerializeField] private float speed = 9f;
    [SerializeField] private float acceleration = 4f;

    [SerializeField] private bool isFacingRight = true;

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
        if (moveInput.x > 0.1f && transform.localScale.x < 0)
        {
            isFacingRight = true;
            Flip();
        }
        else if (moveInput.x < -0.1f && transform.localScale.x > 0)
        {
            isFacingRight = false;
            Flip();
        }
    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public float GetSpeed()
    {
        return speed;
    }

    public bool GetIsFacingRight()
    {
        return isFacingRight;
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

/*
 * Things to modify
 * - Mettre 2 wall detector pour savoir si je m'acroche sur la gauche & droite
*/