using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Fireball : MonoBehaviour
{


    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 50;
    private bool bounce = true;
    // Start is called before the first frame update
   
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        enemy enemy = hitInfo.GetComponent<enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (hitInfo.gameObject.CompareTag ("Wall"))
        {
            if (bounce)
            {
                rb.velocity = -1f*  rb.velocity;
                transform.Rotate(0f, 180, 0f);
                bounce = false;
            }
            else
            {
                Destroy(gameObject);
            }


        }
    }
  
}
