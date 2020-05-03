using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public Rigidbody2D r2d;
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 50;
    private bool bounce = true;
    private Vector2 s;
    private float pos_n;
    private float pos_d;
    private float lastDistance_n;
    private float lastDistance_d;
    // Start is called before the first frame update

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        //gets pos of Ninja
        pos_n = GameObject.Find("SNinja_idle 10").transform.position.x;
        pos_d = GameObject.Find("Dragon").transform.position.x;

        //checks if fireball travel towards ninja
        if (lastDistance_n > Mathf.Abs(pos_n - rb.position.x))
        {
            //SloMo Circle
            if (Mathf.Abs(pos_n - rb.position.x) < 7)
            {
                rb.velocity = transform.right * speed / 5;
            }
            else
            {
                rb.velocity = transform.right * speed;
            }
        }


        //checks if fireball travel towards dragon
        if (lastDistance_d > Mathf.Abs(pos_d - rb.position.x))
        {
            //SloMo Circle
            if (Mathf.Abs(pos_d - rb.position.x) < 7)
            {
                rb.velocity = transform.right * speed / 5;
            }
            else
            {
                rb.velocity = transform.right * speed;
            }
        }

        lastDistance_d = Mathf.Abs(pos_n - rb.position.x);
        lastDistance_n = Mathf.Abs(pos_n - rb.position.x);
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
            } else
            {
                Destroy(gameObject);
            }

           

        }

        /*
        if(hitInfo.gameObject.CompareTag("Player"))
        {
            //playercontroller ninja = hitInfo.GetComponent<playercontroller>()
            //playercontroller ninja = hitInfo.GetComponent<playercontroller>();

            slomotion_Ninja ninja = hitInfo.GetComponent<slomotion_Ninja>();
            //r2d = ninja.GetComponent<Rigidbody2D>();
            
            if (Mathf.Abs(ninja.PosY() - rb.position.y) < 5)
            {
                rb.velocity = transform.right * speed / 3;
            }
            else
            {
                rb.velocity = transform.right * speed;
            }
            /*
            if (slomo)
            {
                slomo = false;
                rb.velocity = rb.velocity / 10;
            }
        }*/
    }

}
