using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour

{

    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public HealthSystem hs;

    private const float speed = 4f;
    private float attackDuration = 0.3f;
    private float timer;
    private string state = "neutral";
    private int health;

    private bool damageAnimation = false;
    private float damageAnimationDuration = 3f;
    private float damageAnimationTimer;
    private float damageFlickerDuration = 0.1f;
    private float damageFlickerTimer;
    private bool damageFlicker = false;


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        damageAnimationTimer = damageAnimationDuration;
        damageFlickerTimer = damageFlickerDuration;
    }

    // Update is called once per frame
    

    private void handlePlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float useSpeed = speed;

        if (state == "attack")
        {
            useSpeed = useSpeed / 1.5f;
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
        else if (horizontal == 0 && state == "neutral")
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", vertical);
        }
        else if(state == "neutral")
        {
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Horizontal", horizontal);
        }

       // Debug.Log(vertical);
        //Vector3 movementDirection = new Vector3(horizontal, vertical).normalized;
        rb.velocity = new Vector2(horizontal, vertical).normalized * speed;
        //transform.position += movementDirection * Time.deltaTime * useSpeed;


        /*//Check if valid movement
        if (isValidMovement(transform.position, movementDirection, useSpeed))
        {
            Debug.Log("valid");
            transform.position += movementDirection * Time.deltaTime * useSpeed;
        }
        //If not, check if valid horizontal movement
        else if (isValidMovement(transform.position, new Vector3(horizontal, 0f).normalized, useSpeed))
        {
            transform.position += new Vector3(horizontal, 0f).normalized * Time.deltaTime * useSpeed;
        }
        //If not, check if valid vertical movement
        else if (isValidMovement(transform.position, new Vector3(0f, vertical).normalized, useSpeed))
        {
            transform.position += new Vector3(0f, vertical).normalized * Time.deltaTime * useSpeed;
        }*/
    }

   /* private bool isValidMovement(Vector3 position, Vector3 direction, float speed)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(position, direction, speed * Time.deltaTime);
        //Debug.Log(raycastHit.collider.tag);
        return raycastHit.collider == null || raycastHit.collider.tag != "Wall";
    }*/

    void Update()
    {
        handleAttack();
        handlePlayerMovement();
        handleDamageAnimation();
    }
    private void handleAttack()
    {
        Vector3 direction = (mousePosition() - getPosition()).normalized;
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            allowAttack(direction);
            timer = attackDuration;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            state = "attack";
            if(direction.x > 0)
            {
                animator.SetBool("ShootLeft", false);
                animator.SetBool("ShootRight", true);
            } else
            {
                animator.SetBool("ShootRight", false);
                animator.SetBool("ShootLeft", true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0)){
            state = "neutral";
            animator.SetBool("ShootLeft", false);
            animator.SetBool("ShootRight", false);
        }
    }
    private void allowAttack(Vector3 direction)
    {
        if (Input.GetKey(KeyCode.Mouse0)) 
        {      
            Projectile.Create(getPosition(), direction);
        } 
    }

    private static Vector3 mousePosition()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        return mouse;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public Vector3 getPosition()
    {
        return transform.position;
    }

    private void handleDamageAnimation()
    {
        if (damageAnimation)
        {
            if (damageAnimationTimer <= 0)
            {
                damageAnimation = false;
                sr.color = new Color(1f, 1f, 1f, 1f);
                damageAnimationTimer = damageAnimationDuration;
            }
            else
            {
                damageAnimationTimer -= Time.deltaTime;

                if (damageFlickerTimer <= 0)
                {
                    damageFlicker = !damageFlicker;
                    sr.color = damageFlicker ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, .5f);
                    damageFlickerTimer = damageFlickerDuration;
                }
                else
                {
                    damageFlickerTimer -= Time.deltaTime;

                }
            }
        }
    }

    public void takeDamage()
    {
        
        health--;
        hs.reduceHealth();
        sr.color = new Color(1f, 1f, 1f, .5f);
        damageAnimation = true;
        damageAnimationTimer = damageAnimationDuration;
        damageFlickerTimer = damageFlickerDuration;

        if (health == 0)
        {
            Destroy(gameObject);
        } else
        {
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }
    }
}
