using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 6f;
    public float maxDistance = 4f;

    Vector2 direction;
    Vector2 startPos;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        startPos = transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (Vector2.Distance(startPos, transform.position) >= maxDistance)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}