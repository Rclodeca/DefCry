using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Player player;
    private const float speed = 1f;

    private Rigidbody2D rb;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if(player == null)
        {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y));
        } else
        {
            transform.position = (Vector3.MoveTowards(transform.position, player.getPosition(), speed * Time.deltaTime));
        }
        //rb.MovePosition(Vector3.MoveTowards(transform.position, player.getPosition(), speed * Time.deltaTime));
        /*Vector3 position = player.getPosition();
        rb.velocity = new Vector2(position.x, position.y);*/
    }
    public static Enemy Create(Vector3 position)
    {
        Transform enemyTransform = Instantiate(GameAssets.instance.pfEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();

        return enemy;
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hi");
        if(collision.tag == "Player")
        {
            player.takeDamage();
        }
        //Destroy(gameObject);
        //transform.position = (Vector3.MoveTowards(transform.position, new , speed * Time.deltaTime));
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            player.takeDamage();
        }
    }

}
