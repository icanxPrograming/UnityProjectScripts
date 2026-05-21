using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject healthPrefab;

    [Range(0, 100)] public int healthDropChance = 30;

    public void Drop()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);

        int rand = Random.Range(0, 100);
        if (rand < healthDropChance)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }
    }
}
