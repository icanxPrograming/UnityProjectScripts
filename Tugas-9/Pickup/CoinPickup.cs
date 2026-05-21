using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerCoin pc = collision.GetComponent<PlayerCoin>();
            if (pc != null)
                pc.AddCoin(coinValue);

            Destroy(gameObject);
        }
    }
}
