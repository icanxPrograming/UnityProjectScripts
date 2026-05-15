using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private BoxCollider2D lightningCollider;

    void Start()
    {
        lightningCollider = GetComponent<BoxCollider2D>();

        if (lightningCollider != null)
            lightningCollider.enabled = false;

        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            float animLength = anim.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animLength);

            Invoke("EnableHitbox", 0.2f);
        }
        else
        {
            Destroy(gameObject, 1f);
            Invoke("EnableHitbox", 0.1f);
        }
    }

    void EnableHitbox()
    {
        if (lightningCollider != null) lightningCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}