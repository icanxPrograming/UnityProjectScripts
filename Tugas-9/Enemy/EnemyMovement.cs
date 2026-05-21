using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float detectRadius = 3f;
    public Transform player;

    [Header("Patrol Settings")]
    public float patrolChangeTime = 2f;

    [Header("Detection")]
    public LayerMask obstacleLayer;
    public float checkDistance = 0.5f;

    Rigidbody2D rb;
    Animator anim;

    Vector2 move;
    float patrolTimer;
    bool isChasing;

    Bounds mapBounds;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        GameObject boundsObj = GameObject.FindGameObjectWithTag("MapBounds");
        if (boundsObj)
        {
            BoxCollider2D bc = boundsObj.GetComponent<BoxCollider2D>();
            mapBounds = bc.bounds;
        }
        else
        {
            Debug.LogError("MapBounds tidak ditemukan!");
        }

        ChooseSafeDirection();
    }

    void Update()
    {
        if (!player) return;

        float dist = Vector2.Distance(rb.position, player.position);
        isChasing = dist <= detectRadius;

        if (isChasing)
        {
            TrySetDirection((player.position - transform.position).normalized);
        }
        else
        {
            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 0f)
                ChooseSafeDirection();
        }

        anim.SetBool("IsMoving", move != Vector2.zero);
    }

    void FixedUpdate()
    {
        if (move == Vector2.zero) return;

        Vector2 nextPos = rb.position + move * speed * Time.fixedDeltaTime;

        if (!mapBounds.Contains(nextPos))
        {
            ChooseSafeDirection();
            return;
        }

        rb.MovePosition(nextPos);
    }

    void ChooseSafeDirection()
    {
        patrolTimer = patrolChangeTime;

        Vector2[] dirs =
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        List<Vector2> validDirs = new();

        foreach (Vector2 dir in dirs)
        {
            Vector2 checkPos = rb.position + dir * checkDistance;

            if (!mapBounds.Contains(checkPos))
                continue;

            if (!Physics2D.Raycast(rb.position, dir, checkDistance, obstacleLayer))
                validDirs.Add(dir);
        }

        move = validDirs.Count > 0
            ? validDirs[Random.Range(0, validDirs.Count)]
            : Vector2.zero;

        SetDirection(move);
    }

    void TrySetDirection(Vector2 dir)
    {
        Vector2 checkPos = rb.position + dir * checkDistance;

        if (!mapBounds.Contains(checkPos)) return;
        if (Physics2D.Raycast(rb.position, dir, checkDistance, obstacleLayer)) return;

        move = dir.normalized;
        SetDirection(move);
    }

    public void ForceChangeDirection()
    {
        ChooseSafeDirection();
    }

    void SetDirection(Vector2 dir)
    {
        if (dir == Vector2.zero) return;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            anim.SetInteger("Direction", dir.x > 0 ? 1 : 0);
        else
            anim.SetInteger("Direction", dir.y > 0 ? 2 : 3);
    }
}