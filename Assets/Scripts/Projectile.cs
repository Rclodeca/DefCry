using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private const float speed = 8f;
    private Vector3 direction;


    private void Update()
    {
        //Vector2 curPosition = 
        transform.position += direction * speed * Time.deltaTime;
    }

    private void setDirection(Vector3 dir)
    {
        this.direction = dir;
        transform.eulerAngles = new Vector3(0, 0, getAngleFromVectorFloat(dir));
    }

    private static float getAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public static Projectile Create(Vector3 position, Vector3 direction)
    {
        Transform projectileTransform = Instantiate(GameAssets.instance.pfProjectile, position, Quaternion.identity);
        Projectile projectile = projectileTransform.GetComponent<Projectile>();
        projectile.setDirection(direction);

        return projectile;
    }
}