using System.Collections;
using System.Collections.Generic;
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
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (Vector2.Distance(startPos, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
