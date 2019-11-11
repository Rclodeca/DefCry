using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Player player;
    private float speed;
    private int health;

    public Animator animator;

    private Rigidbody2D rb;
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = 1;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        handleMovement();
    }

    private void handleMovement()
    {
        
        //dont move if player is dead
        if (player == null || health == 0)
        {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y));
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
            return;
        }
        
        //calculate new position
        Vector3 newPosition = Vector3.MoveTowards(transform.position, player.getPosition(), speed * Time.deltaTime);
        float xDirection = player.getPosition().x - transform.position.x;
        float yDirection = player.getPosition().y - transform.position.y;

        //set animation to walk towards player
        if (xDirection != 0 && Mathf.Abs(xDirection) > Mathf.Abs(yDirection))
        {
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Horizontal", xDirection);
        }
        else if (yDirection != 0)
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", yDirection);
        }

        //walk towards player
        transform.position = newPosition;

    }
    public static Enemy Create(Vector3 position, float speed)
    {
        Transform enemyTransform = Instantiate(GameAssets.instance.pfEnemy, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        enemy.setSpeed(speed);

        return enemy;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    public void damage()
    {
        health = 0;
        animator.SetBool("Dead", true);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        transform.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && health > 0)
        {
            damage();
            player.takeDamage();
        }
    }

}
