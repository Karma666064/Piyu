using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    EnemyAttack enemyAttack;
    private Rigidbody2D rb;

    [Header("Values")]
    public float speed = 5f;

    [SerializeField] private Transform detectorObj;
    [SerializeField] private float detectorRadius = 6.5f;
    [SerializeField] private LayerMask playerLayer;

    [Header("Setup")]
    [SerializeField] private bool autoMove = true;
    [SerializeField] private bool looping = true;

    [Header("Points List")]
    [SerializeField] private List<Transform> posPoints = new List<Transform>();

    //public 

    [SerializeField] private Transform player;

    private int currentIndex;
    private Transform currentTarget;

    private void Start()
    {
        enemyAttack = GetComponent<EnemyAttack>();
        rb = GetComponent<Rigidbody2D>();

        if (posPoints.Count > 0)
        {
            currentIndex = 0;
            currentTarget = posPoints[currentIndex];
        }
    }

    private void FixedUpdate()
    {
        Collider2D isPlayerTargeted = Physics2D.OverlapCircle(detectorObj.position, detectorRadius, playerLayer);
        float oldSpeed = speed;

        if (isPlayerTargeted)
        { 
            Vector2 moveDir = (player.position - gameObject.transform.position).normalized;
            Vector2 velocity = rb.linearVelocity;



            velocity.x = Mathf.Lerp(velocity.x, moveDir.x * speed, Time.fixedDeltaTime * (speed - 2));
            rb.linearVelocity = velocity;
        }
        else
        {
            Vector2 moveDir = (currentTarget.position - gameObject.transform.position).normalized;
            Vector2 velocity = rb.linearVelocity;

            speed = oldSpeed;

            velocity.x = Mathf.Lerp(velocity.x, moveDir.x * speed, Time.fixedDeltaTime * (speed - 2));
            rb.linearVelocity = velocity;

            if (autoMove && (gameObject.transform.position - currentTarget.position).magnitude < 1f)
            {
                SetupNextPos();
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

    void OnDrawGizmosSelected()
    {
        if (detectorObj != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectorObj.position, detectorRadius);
        }
    }
}
