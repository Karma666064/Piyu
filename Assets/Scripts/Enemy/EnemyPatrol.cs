using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D rb;

    private EnemyAttack enemyAttack;

    [Header("Values")]
    public float speed = 5f;

    [Header("Setup")]
    [SerializeField] private bool autoMove = true;
    [SerializeField] private bool looping = true;

    [Header("Points List")]
    [SerializeField] private List<Transform> posPoints = new List<Transform>();

    [SerializeField] private Transform player;

    private int currentIndex;
    private Transform currentTarget;

    public bool isOnTargetPlayer;

    private float lastPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAttack = GetComponent<EnemyAttack>();

        if (posPoints.Count > 0)
        {
            currentIndex = 0;
            currentTarget = posPoints[currentIndex];
        }
    }

    private void Update()
    {
        if (!!enemyAttack.GetIsPlayerTargeted())
        { 
            Vector2 moveDir = (player.position - transform.position).normalized;
            Vector2 velocity = rb.linearVelocity;
            float currentPos = transform.position.x;

            velocity.x = Mathf.Lerp(velocity.x, moveDir.x * speed, Time.fixedDeltaTime * (speed - 2));
            rb.linearVelocity = velocity;

            if (lastPos < currentPos && transform.localScale.x < 0) Flip();
            else if (lastPos > currentPos && transform.localScale.x > 0) Flip();

            lastPos = currentPos;
        }
        else
        {
            Vector2 moveDir = (currentTarget.position - transform.position).normalized;
            Vector2 velocity = rb.linearVelocity;

            velocity.x = Mathf.Lerp(velocity.x, moveDir.x * speed, Time.fixedDeltaTime * (speed - 2));
            rb.linearVelocity = velocity;

            if (autoMove && (gameObject.transform.position - currentTarget.position).magnitude < 1f)
            {
                SetupNextPos();
                Flip();
            }
        }

    }

    void SetupNextPos()
    {
        currentIndex++;

        if (looping)
        {
            if (currentIndex > posPoints.Count)
            {
                currentIndex = 0;
            }
        }

        if (currentIndex < posPoints.Count)
        {
            currentTarget = posPoints[currentIndex];
        }
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
