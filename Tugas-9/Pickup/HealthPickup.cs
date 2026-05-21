using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float hpRestoreAmount = 20f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerHealth ph = collision.GetComponent<PlayerHealth>();
        if (ph != null)
            ph.RestoreHealth(hpRestoreAmount);

        Destroy(gameObject);
    }
}
