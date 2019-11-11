using System.Collections;
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
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
                if(SceneManager.GetActiveScene().buildIndex == 4)
                {
                    Destroy(player);
                    SceneManager.LoadScene(6);
                } else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }             
            } else
            {
                timer -= Time.deltaTime;
            }
            
        }
    }

}
