using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wave : MonoBehaviour
{
    public float timeBetweenSpawnTypes = 8f;

    private bool active = false;
    private float timer;
    private Spawner spawner;
    private Player player;
    private int numTypes;
    private int counter = 0;
    private int waveNumber;

    public bool waveComplete = false;
    public int wave = 1;
    void Start()
    {
        int scene = SceneManager.GetActiveScene().buildIndex - 1;
        if(scene == 2)
        {
            numTypes = 10;
        }
        else if(scene == 3)
        {
            numTypes = 11;
        }
        else
        {
            numTypes = 9;
        }
        
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
                    case 10:
                        spawner.stopSpawn();
                        break;
                    case 11:
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
        waveComplete = true;
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
