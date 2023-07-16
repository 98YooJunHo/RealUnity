using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclesPrefab;

    private int randomIdx;
    private float timeAfterSpawn;
    private float spawnTimeMin = 2.5f;
    private float spawnTimeMax = 3.5f;
    private float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0f;
        timeAfterSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeAfterSpawn + spawnTime <= Time.time)
        {
            randomIdx = Random.Range(0, 2);
            timeAfterSpawn = Time.time;
            spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            Instantiate(obstaclesPrefab[randomIdx]);
        }
    }
}
