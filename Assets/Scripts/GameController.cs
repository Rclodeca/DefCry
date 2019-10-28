using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Enemy.Create(new Vector3(-2, 3));
        Spawner spawn = Spawner.Create();
        spawn.startSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
        //Projectile.Create(new Vector3(-2, 3), new Vector3(2, 5));
    }
}
