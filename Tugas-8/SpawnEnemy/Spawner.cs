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
    public int maxSpawn = 3;

    private List<Vector3> spawnPositions = new List<Vector3>();
    private List<GameObject> aliveEnemies = new List<GameObject>();

    private int index = 0;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        spawnPositions.Add(new Vector3(10.26f, -5.71f, 0f));
        spawnPositions.Add(new Vector3(8.32f, -4f, 0f));
        spawnPositions.Add(new Vector3(8.25f, -8.14f, 0f));
    }

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            aliveEnemies.RemoveAll(e => e == null);

            if (aliveEnemies.Count < maxSpawn)
            {
                if (sr != null) sr.enabled = true;

                if (index >= spawnPositions.Count) index = 0;
                transform.position = spawnPositions[index];

                yield return new WaitForSeconds(spawnDelay);

                GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                aliveEnemies.Add(enemy);

                EnemyMovement em = enemy.GetComponent<EnemyMovement>();
                if (em != null)
                    em.player = player;

                index++;
            }
            else
            {
                if (sr != null) sr.enabled = false;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3[] points =
        {
            new Vector3(10.26f, -5.71f, 0f),
            new Vector3(8.32f, -4f, 0f),
            new Vector3(8.25f, -8.14f, 0f)
        };

        foreach (var p in points)
            Gizmos.DrawWireSphere(p, 0.25f);
    }
}