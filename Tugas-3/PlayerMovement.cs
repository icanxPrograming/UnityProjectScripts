using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    Rigidbody2D rb;
    Animator anim;

    Vector2 move;
    int direction;
    int lastDirection;
    bool isMoving;
    bool lastIsMoving;

    private float minX, maxX, minY, maxY;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        direction = 1;
        anim.SetInteger("Direction", direction);
        anim.SetBool("IsMoving", false);

        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float halfWidth = sr.bounds.size.x / 2f;
        float halfHeight = sr.bounds.size.y / 2f;

        minX = cam.transform.position.x - camWidth + halfWidth;
        maxX = cam.transform.position.x + camWidth - halfWidth;
        minY = cam.transform.position.y - camHeight + halfHeight;
        maxY = cam.transform.position.y + camHeight - halfHeight;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        move = new Vector2(x, y).normalized;
        isMoving = move.magnitude > 0;

        if (x != 0 || y != 0)
        {
            if (Mathf.Abs(x) > Mathf.Abs(y))
                direction = x > 0 ? 1 : 0;
            else
                direction = y > 0 ? 2 : 3;
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
        Vector2 newPos = rb.position + move * speed * Time.fixedDeltaTime;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        rb.MovePosition(newPos);
    }
}