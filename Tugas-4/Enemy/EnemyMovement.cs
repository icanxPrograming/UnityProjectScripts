using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float detectRadius = 3f;
    public float patrolDistance = 2f;
    public Transform player;

    Rigidbody2D rb;
    Animator anim;

    Vector2 startPos;
    Vector2 move;

    int patrolDirection = -1; // -1 kiri, 1 kanan

    bool isChasing = false;
    bool isReturning = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        startPos = rb.position;

        anim.SetBool("IsMoving", true);
        anim.SetInteger("Direction", 0); // default kiri
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(rb.position, player.position);

        // =====================
        // DETEKSI PLAYER
        // =====================
        if (distanceToPlayer <= detectRadius)
        {
            isChasing = true;
            isReturning = false;
        }
        else if (isChasing)
        {
            // player kabur → balik ke start
            isChasing = false;
            isReturning = true;
        }

        // =====================
        // LOGIC UTAMA
        // =====================
        if (isChasing)
        {
            // KEJAR PLAYER
            Vector2 dir = (player.position - transform.position).normalized;
            move = dir;

            SetDirection(dir);
            anim.SetBool("IsMoving", true);
        }
        else if (isReturning)
        {
            // BALIK KE START POS
            Vector2 dir = (startPos - rb.position);

            if (dir.magnitude < 0.05f)
            {
                // sudah sampai start
                isReturning = false;
                patrolDirection = -1; // reset arah awal (kiri)
                move = Vector2.zero;
                return;
            }

            dir = dir.normalized;
            move = dir;

            SetDirection(dir);
            anim.SetBool("IsMoving", true);
        }
        else
        {
            // PATROLI KIRI-KANAN
            float offset = rb.position.x - startPos.x;

            if (Mathf.Abs(offset) >= patrolDistance)
                patrolDirection *= -1;

            move = new Vector2(patrolDirection, 0);

            anim.SetBool("IsMoving", true);
            anim.SetInteger("Direction", patrolDirection > 0 ? 1 : 0);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
    }

    // =====================
    // HELPER ARAH ANIMASI
    // =====================
    void SetDirection(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            anim.SetInteger("Direction", dir.x > 0 ? 1 : 0);
        else
            anim.SetInteger("Direction", dir.y > 0 ? 2 : 3);
    }
}