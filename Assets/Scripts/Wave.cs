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
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        spawner = Spawner.Create();
        timer = timeBetweenSpawnTypes;
        spawner.startSpawn();
    }

    void Update()
    {
        if (active)
        {
            if (timer <= 0)
            {   
                spawner.setTimeBetweenSpawns(2f / (counter + 0.7f));
                spawner.mode = "random";

                if(counter == 2)
                {
                    spawner.spawnFromAllLocations();
                    spawner.spawnFromAllLocations();
                }
                else if(counter == 6)
                {
                    spawner.stopSpawn();
                }
                else if(counter == 7)
                {
                    spawner.startSpawn();
                }
                

                counter++;
                timer = timeBetweenSpawnTypes; 
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        if(counter >= numTypes)
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

    public static Wave Create()
    {
        Transform waveTransform = Instantiate(GameAssets.instance.pfWave, new Vector3(0, 0), Quaternion.identity);
        Wave wave = waveTransform.GetComponent<Wave>();

        return wave;
    }
}
