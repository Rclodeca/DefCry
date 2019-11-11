using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    private float timer;
    private float timeBetweenSpawns = 3f;
    private bool active = false;
    private Vector3[] spawnLocations = new Vector3[4] {
        new Vector3(-7, 0),
        new Vector3(7, 0),
        new Vector3(0, 5),
        new Vector3(0, -5)
    };
    private float superSpeed;

    public string mode = "random";

    void Start()
    {
        timer = getTimeBetweenSpawns();
        int level = SceneManager.GetActiveScene().buildIndex - 1;
        if (level == 2)
        {
            superSpeed = 2f;
        }
        else if (level == 3)
        {
            superSpeed = 3f;
        }
        else
        {
            superSpeed = 1f;
        }
    }

    void Update()
    {
        if (timer <= 0)
        {
            handleSpawn();
            timer = getTimeBetweenSpawns();
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

    public void setTimeBetweenSpawns(float time)
    {
        timeBetweenSpawns = time;
    }

    private float getTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public void spawnRandomEnemy()
    {
        int randomSpeed = Random.Range(0, 5);
        float speed = 1f;
        if(randomSpeed == 4)
        {
            speed = superSpeed;
        }
        Enemy ork = Enemy.Create(getDynamicLocation(Random.Range(0, 4)), speed);
    }

    public void spawnFromAllLocations()
    {
        foreach(Vector3 location in spawnLocations)
        {
            Enemy ork = Enemy.Create(location, 1f);
        }
    }

    public void powerUp()
    {
        if(GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
        {
            PowerUp pu = PowerUp.Create(new Vector3(0, 0));
        } 
    }

    private Vector3 getDynamicLocation(int side)
    {
        Vector3 location = spawnLocations[side];

        switch (side)
        {
            case 0:
                location.y = Random.Range(-5, 5);
                break;
            case 1:
                location.y = Random.Range(-5, 5);
                break;
            case 2:
                location.x = Random.Range(-7, 7);
                break;
            case 3:
                location.x = Random.Range(-7, 7);
                break;
        }

        return location;
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
