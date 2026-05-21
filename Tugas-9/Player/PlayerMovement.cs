using UnityEngine;
using UnityEngine.InputSystem;

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

    Vector2 minBound;
    Vector2 maxBound;
    bool boundsReady = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        direction = 1;
        anim.SetInteger("Direction", direction);
        anim.SetBool("IsMoving", false);
    }

    void Start()
    {
        GameObject boundsObj = GameObject.FindGameObjectWithTag("MapBounds");
        if (boundsObj != null)
        {
            BoxCollider2D bc = boundsObj.GetComponent<BoxCollider2D>();
            if (bc != null)
            {
                Bounds b = bc.bounds;

                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                float halfW = sr.bounds.extents.x;
                float halfH = sr.bounds.extents.y;

                minBound = new Vector2(b.min.x + halfW, b.min.y + halfH);
                maxBound = new Vector2(b.max.x - halfW, b.max.y - halfH);

                boundsReady = true;
            }
        }

        if (PlayerPrefs.GetInt("HasLoadedGame", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");

            rb.position = new Vector2(x, y);
        }
    }

    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>().normalized;
        isMoving = move.magnitude > 0;

        if (isMoving)
        {
            if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
                direction = move.x > 0 ? 1 : 0;
            else
                direction = move.y > 0 ? 2 : 3;
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

        if (boundsReady)
        {
            newPos.x = Mathf.Clamp(newPos.x, minBound.x, maxBound.x);
            newPos.y = Mathf.Clamp(newPos.y, minBound.y, maxBound.y);
        }

        rb.MovePosition(newPos);
    }
}