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

        spawner = Spawner.Create();
        spawner.mode = "all";
        spawner.startSpawn();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            spawner.stopSpawn();
        }
    }
}
