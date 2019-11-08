using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public SpriteRenderer health1;
    public SpriteRenderer health2;
    public SpriteRenderer health3;

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reduceHealth()
    {
        health--;
        if(health == 2)
        {
            health3.color = new Color(1f, 1f, 1f, 0f);
        }
        else if(health == 1)
        {
            health3.color = new Color(1f, 1f, 1f, 0f);
            health2.color = new Color(1f, 1f, 1f, 0f);
        }
        else if(health == 0)
        {
            health3.color = new Color(1f, 1f, 1f, 0f);
            health2.color = new Color(1f, 1f, 1f, 0f);
            health1.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            health3.color = new Color(1f, 1f, 1f, 1f);
            health2.color = new Color(1f, 1f, 1f, 1f);
            health1.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
