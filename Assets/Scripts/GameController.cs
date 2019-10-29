using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private float timer;
    private float waveTime;
    private float breakTime;

    private Player player;
    private Spawner spawner;
   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        /*spawner = Spawner.Create();
        spawner.mode = "all";
        spawner.startSpawn();*/
        Wave wave1 = Wave.Create();
        wave1.start();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(player == null)
        {
            spawner.stopSpawn();
        }
        handleWave();*/
    }

    private void handleWave()
    {
        if (timer <= 0)
        {
            spawner.stopSpawn();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void waveType1(float timeLength, float timeBetweenSpawns)
    {
        spawner.mode = "random";
        spawner.setTimeBetweenSpawns(2f);
        timer = timeLength;
        spawner.startSpawn();
    }
}
