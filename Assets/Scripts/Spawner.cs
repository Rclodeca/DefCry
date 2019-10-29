using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float timer;
    public float timeBetweenSpawns = 3f;
    private bool active = false;
    private Vector3[] spawnLocations = new Vector3[4] {
        new Vector3(-7, 0),
        new Vector3(7, 0),
        new Vector3(0, 5),
        new Vector3(0, -5)
    };

    public string mode = "random";

    void Start()
    {
        timer = timeBetweenSpawns;
    }

    void Update()
    {
        if (timer <= 0)
        {
            handleSpawn();
            timer = timeBetweenSpawns;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void handleSpawn()
    {
        if (active)
        {
            switch (mode)
            {
                case "random":
                    spawnRandomEnemy();
                    break;
                case "all":
                    spawnFromAllLocations();
                    break;
                default:
                    break;
            }
        }
    }

    private void spawnRandomEnemy()
    {
        Enemy ork = Enemy.Create(spawnLocations[Random.Range(0, spawnLocations.Length)]);
        timer = timeBetweenSpawns;
    }

    private void spawnFromAllLocations()
    {
        foreach(Vector3 location in spawnLocations)
        {
            Enemy ork = Enemy.Create(location);
        }
    }

    public void startSpawn()
    {
        active = true;
    }
    public void stopSpawn()
    {
        active = false;
    }

    public static Spawner Create()
    {
        Transform spawnerTransform = Instantiate(GameAssets.instance.pfSpawner, new Vector3(0, 0), Quaternion.identity);
        Spawner spawner = spawnerTransform.GetComponent<Spawner>();

        return spawner;
    }
}
