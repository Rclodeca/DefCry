using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{

    public Animator animator;
    public Rigidbody2D rb;

    private const float speed = 4f;
    private float attackDuration = 0.3f;
    private float timer;
    private string state = "neutral";



    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Input.GetKey(KeyCode.F))
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
        if (Input.GetKeyUp(KeyCode.F)){
            state = "neutral";
            animator.SetBool("ShootLeft", false);
            animator.SetBool("ShootRight", false);
        }
    }
    private void allowAttack(Vector3 direction)
    {
        if (Input.GetKey(KeyCode.F)) 
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

    public void takeDamage()
    {

        Destroy(gameObject);
    }
}
