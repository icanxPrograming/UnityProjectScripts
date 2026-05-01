using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    public GameObject enemyPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public float spawnDelay = 0.5f;

    private List<Vector3> spawnPositions = new List<Vector3>();
    private int index = 0;

    void Awake()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        // ?? TITIK SPAWN TETAP
        spawnPositions.Add(new Vector3(10.26f, -2.8f, 0f));
        spawnPositions.Add(new Vector3(8.32f, -1, 0f));
        spawnPositions.Add(new Vector3(8.25f, -3.96f, 0f));
    }

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            transform.position = spawnPositions[index];

            yield return new WaitForSeconds(spawnDelay);

            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            EnemyMovement em = enemy.GetComponent<EnemyMovement>();
            if (em != null)
                em.player = player;

            index++;
            if (index >= spawnPositions.Count)
                index = 0;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Vector3[] points =
        {
            new Vector3(10.26f, -2.8f, 0f),
            new Vector3(8.32f, -1f, 0f),
            new Vector3(8.25f, -3.96f, 0f)
        };

        foreach (var p in points)
            Gizmos.DrawWireSphere(p, 0.25f);
    }
}