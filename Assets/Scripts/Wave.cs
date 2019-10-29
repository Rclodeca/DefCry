using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float timeBetweenSpawnTypes = 8f;

    private bool active = false;
    private float timer;
    private Spawner spawner;
    private Player player;
    private int numTypes = 9;
    private int counter = 0;
    private int waveNumber;

    public int wave = 1;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        spawner = Spawner.Create();
        timer = timeBetweenSpawnTypes;
        spawner.startSpawn();
    }

    void Update()
    {
        switch (waveNumber)
        {
            case 1:
                wave1Handle();
                break;
        }
        
    }

    private void wave1Handle()
    {
        if (active)
        {
            if (timer <= 0)
            {
                spawner.setTimeBetweenSpawns(2f / (counter + 0.7f));
                spawner.mode = "random";

                switch (counter)
                {
                    case 1:
                        spawner.spawnFromAllLocations();
                        break;
                    case 2:
                        spawner.spawnFromAllLocations();
                        spawner.spawnFromAllLocations();
                        break;
                    case 6:
                        spawner.stopSpawn();
                        break;
                    case 7:
                        spawner.startSpawn();
                        break;
                    default:
                        break;
                }

                counter++;
                timer = timeBetweenSpawnTypes;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        if (counter >= numTypes)
        {
            stop();
        }
    }

    public void start()
    {
        active = true;
        /*spawner.startSpawn();*/
    }

    public void stop()
    {
        active = false;
        spawner.stopSpawn();
    }

    private void setType(int type)
    {
        this.waveNumber = type;
    }

    public static Wave Create(int type)
    {
        Transform waveTransform = Instantiate(GameAssets.instance.pfWave, new Vector3(0, 0), Quaternion.identity);
        Wave wave = waveTransform.GetComponent<Wave>();
        wave.setType(type);

        return wave;
    }
}
