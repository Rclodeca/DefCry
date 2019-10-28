using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float timer;
    private float timeBetweenSpawns = 3f;
    private bool on = false;
    private Vector3[] spawnLocations = new Vector3[4] {
        new Vector3(-7, 0),
        new Vector3(7, 0),
        new Vector3(0, 5),
        new Vector3(0, -5)
    };

    void Start()
    {
        timer = timeBetweenSpawns;
    }

    void Update()
    {
        if(on) spawnRandomEnemy();
    }

    private void spawnRandomEnemy()
    {
        if (timer <= 0)
        {
            Enemy ork = Enemy.Create(spawnLocations[Random.Range(0, spawnLocations.Length)]);
            timer = timeBetweenSpawns;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void startSpawn()
    {
        on = true;
    }
    public void stopSpawn()
    {
        on = false;
    }

    public static Spawner Create()
    {
        Transform spawnerTransform = Instantiate(GameAssets.instance.pfSpawner, new Vector3(0, 0), Quaternion.identity);
        Spawner spawner = spawnerTransform.GetComponent<Spawner>();

        return spawner;
    }
}
