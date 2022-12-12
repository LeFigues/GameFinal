using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> enemyPrefabs;
    public float timeBtwSpawn = 0.2f;
    float timer = 0;
    public Transform target;
    public float speed = 50;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        timer += Time.deltaTime;
        if (timer >= timeBtwSpawn)
        {
            timer = 0;
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
                spawnPoints[Random.Range(0, spawnPoints.Count)].position,
                Quaternion.identity);
        }
    }
}
