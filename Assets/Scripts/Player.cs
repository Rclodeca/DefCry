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
    public AudioSource audio;

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

    private float powerUpTimer;
    private float powerUpDuration = 7f;
    private bool powerUpActive = false;
    private int powerUpType;




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
        powerUpTimer = powerUpDuration;
        audio = GetComponent<AudioSource>();
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

        rb.velocity = new Vector2(horizontal, vertical).normalized * speed;

    }

    void Update()
    {
        handleAttack();
        handlePlayerMovement();
        handleDamageAnimation();
        handlePowerUp();
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
            if (powerUpActive)
            {
                if (powerUpType == 0)
                {
                    xAttack();
                }
                else
                {
                    shotGunAttack(direction);
                }
            }
            else
            {
                Projectile.Create(getPosition(), direction);
            }
        } 
    }

    private void xAttack()
    {
        Projectile.Create(getPosition(), new Vector3(1, 1).normalized);
        Projectile.Create(getPosition(), new Vector3(1, -1).normalized);
        Projectile.Create(getPosition(), new Vector3(-1, -1).normalized);
        Projectile.Create(getPosition(), new Vector3(-1, 1).normalized);
        Projectile.Create(getPosition(), new Vector3(1, 0).normalized);
        Projectile.Create(getPosition(), new Vector3(0, -1).normalized);
        Projectile.Create(getPosition(), new Vector3(-1, 0).normalized);
        Projectile.Create(getPosition(), new Vector3(0, 1).normalized);
    }

    private void shotGunAttack(Vector3 direction)
    {
        if(Mathf.Abs(Mathf.Abs(direction.x) - Mathf.Abs(direction.y)) < 0.7)
        {
            if(direction.x > 0)
            {
                if(direction.y > 0)
                {
                    Projectile.Create(getPosition(), new Vector3(1, 1).normalized);
                    Projectile.Create(getPosition(), new Vector3(1, 2f).normalized);
                    Projectile.Create(getPosition(), new Vector3(1, 0.5f).normalized);
                }
                else
                {
                    Projectile.Create(getPosition(), new Vector3(1, -1).normalized);
                    Projectile.Create(getPosition(), new Vector3(1, -2f).normalized);
                    Projectile.Create(getPosition(), new Vector3(1, -0.5f).normalized);
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    Projectile.Create(getPosition(), new Vector3(-1, 1).normalized);
                    Projectile.Create(getPosition(), new Vector3(-1, 2f).normalized);
                    Projectile.Create(getPosition(), new Vector3(-1, 0.5f).normalized);
                }
                else
                {
                    Projectile.Create(getPosition(), new Vector3(-1, -1).normalized);
                    Projectile.Create(getPosition(), new Vector3(-1, -2f).normalized);
                    Projectile.Create(getPosition(), new Vector3(-1, -0.5f).normalized);
                }
            }
            
        }
        else if (direction.x != 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                Projectile.Create(getPosition(), new Vector3(1, 0));
                Projectile.Create(getPosition(), new Vector3(1, .3f).normalized);
                Projectile.Create(getPosition(), new Vector3(1, -0.3f).normalized);
            }
            else
            {
                Projectile.Create(getPosition(), new Vector3(-1, 0));
                Projectile.Create(getPosition(), new Vector3(-1, .3f).normalized);
                Projectile.Create(getPosition(), new Vector3(-1, -0.3f).normalized);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                Projectile.Create(getPosition(), new Vector3(0, 1));
                Projectile.Create(getPosition(), new Vector3(.3f, 1).normalized);
                Projectile.Create(getPosition(), new Vector3(-.3f, 1).normalized);
            }
            else
            {
                Projectile.Create(getPosition(), new Vector3(0, -1));
                Projectile.Create(getPosition(), new Vector3(.3f, -1).normalized);
                Projectile.Create(getPosition(), new Vector3(-.3f, -1).normalized);
            }
        }
    }

    private static Vector3 mousePosition()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        return mouse;
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
            SceneManager.LoadScene(5);
        } else
        {
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }
        }
    }
    public void powerUp()
    {
        
        powerUpActive = true;
        int choice = Random.Range(0, 5);
        if(choice > 1)
        {
            powerUpTimer = 12f;
            powerUpType = 1;
        }
        else
        {
            powerUpType = 0;
        }

        powerUpAudio();
    }

    private void handlePowerUp()
    {
        if (powerUpActive)
        {
            if(powerUpTimer <= 0)
            {
                powerUpActive = false;
                powerUpTimer = powerUpDuration;
            }
            else
            {
                powerUpTimer -= Time.deltaTime;
            }
        }
    }

    private void powerUpAudio()
    {
        audio.Play();
    }
}
