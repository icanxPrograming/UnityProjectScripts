using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float detectRadius = 3f;
    public Transform player;

    [Header("Random Move")]
    public float randomYStrength = 0.5f;

    Rigidbody2D rb;
    Animator anim;

    Vector2 move;

    bool isChasing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool("IsMoving", true);
        anim.SetInteger("Direction", 0);
    }

    void Update()
    {
        if (player == null)
            return;
        float distanceToPlayer = Vector2.Distance(rb.position, player.position);

        isChasing = distanceToPlayer <= detectRadius;

        if (isChasing)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            move = dir;

            SetDirection(dir);
        }
        else
        {
            float randomY = Random.Range(-randomYStrength, randomYStrength);
            move = new Vector2(-1f, randomY).normalized;

            anim.SetInteger("Direction", 0);
        }

        anim.SetBool("IsMoving", true);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

    void SetDirection(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            anim.SetInteger("Direction", dir.x > 0 ? 1 : 0);
        else
            anim.SetInteger("Direction", dir.y > 0 ? 2 : 3);
    }
}