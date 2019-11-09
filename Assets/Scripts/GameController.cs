﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float timer;
    private float finishDelay = 3f;

    private Player player;
    private Spawner spawner;
    private Wave wave1;

    
    
   

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        /*spawner = Spawner.Create();
        spawner.mode = "all";
        spawner.startSpawn();*/
        wave1 = Wave.Create(1);
        wave1.start();
        timer = finishDelay;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wave1.waveComplete && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if(timer <= 0)
            {
                SceneManager.LoadScene(3);
            } else
            {
                timer -= Time.deltaTime;
            }
            
        }
    }

    /*private void handleWave()
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
    }*/
}
