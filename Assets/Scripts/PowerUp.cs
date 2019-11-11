using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private float timer;
    private float duration = 15f;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        timer = duration;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }

    public static PowerUp Create(Vector3 position)
    {
        Transform powerUpTransform = Instantiate(GameAssets.instance.pfPowerUp, position, Quaternion.identity);
        PowerUp powerUp = powerUpTransform.GetComponent<PowerUp>();

        return powerUp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            player.powerUp();
        }
    }
}
