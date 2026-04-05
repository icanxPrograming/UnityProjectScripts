using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float detectRadius = 3f;
    public Transform player;

    Rigidbody2D rb;
    Animator anim;

    Vector2 move;

    int direction = 0;
    int lastDirection = -1;
    bool isMoving = false;
    bool lastIsMoving = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        direction = 0;
        anim.SetInteger("Direction", direction);
        anim.SetBool("IsMoving", false);
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectRadius)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            move = dir;
            isMoving = true;

            // pilih arah dominan
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                direction = dir.x > 0 ? 1 : 0;
            else
                direction = dir.y > 0 ? 2 : 3;
        }
        else
        {
            move = Vector2.zero;
            isMoving = false;
        }

        if (direction != lastDirection)
        {
            anim.SetInteger("Direction", direction);
            lastDirection = direction;
        }

        if (isMoving != lastIsMoving)
        {
            anim.SetBool("IsMoving", isMoving);
            lastIsMoving = isMoving;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}