using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Fireball : MonoBehaviour
{

    //public Rigidbody2D r2d;
    public Vector2 speed = new Vector2(20,20);
    public Rigidbody2D rb;
    public int damage = 50;
    private bool bounce = true;
    private Vector2 s;
    private float pos_n;
    private float pos_d;
    private float lastDistance_n;
    private float lastDistance_d;
    private float norm;
    private Vector2 oldVelocity;
    // Start is called before the first frame update

    void Start()
    {
        rb.velocity = transform.right * speed;



        //Vector2 mouse = Input.mousePosition;
        //Ray castPoint = Camera.main.ScreenPointToRay(mousePosition);
        //RaycastHit hit;
        //norm = (Mathf.Abs(mousePosition - rb.position));
        //if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        // {
        //s = (((Vector2) hit.point) - rb.position);

        //rb.transform.position = hit.point;
        // }

        //Code for aiming with mouse(100pro)
        /*
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pz.z = 0;
        s = (((Vector2)pz) - rb.position);
        s.Normalize();
        rb.velocity = s * speed;
        GetComponent<Rigidbody>().freezeRotation = true;
        //command rotate fireball proper position
        */

    }

    void FixedUpdate()
    {

        //Code for aiming with mouse (50pro)
        // because we want the velocity after physics, we put this in fixed update
        //oldVelocity = GetComponent<Rigidbody>().velocity;
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
                //Code for aiming with mouse
                //rb.velocity = s * speed/5;
            }
            else
            {
                rb.velocity = transform.right * speed;
                //Code for aiming with mouse
                //rb.velocity = s * speed;
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
        if (hitInfo.gameObject.tag == "Enemy")
        {
            enemy enemy = hitInfo.gameObject.GetComponent<enemy>();
            //hitInfo.gameObject.tag == "Enemy"
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }



        if (hitInfo.gameObject.CompareTag("Wall"))
        {
            if (bounce)
            {
                transform.Rotate(0f, 180, 0f);
                rb.velocity = -1f * rb.velocity;
                bounce = false;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D hitInfo)
    {


        //Everything Code for aiming with mouse (almost complete)
        /*
        if (hitInfo.gameObject.tag == "Enemy")
        {
            enemy enemy = hitInfo.gameObject.GetComponent<enemy>();
            //hitInfo.gameObject.tag == "Enemy"
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    
        //reflection = hitInfo.ClosestPoint(rb.position);
        if (hitInfo.gameObject.CompareTag ("Wall"))
        {
            ContactPoint2D contact = hitInfo.contacts[0];
            if (bounce)
            {
                //Vector2 reflection = hitInfo.contact[0].normal;
                Vector2 reflectedVelocity = Vector2.Reflect(s, contact.normal);
                rb.velocity = speed*(reflectedVelocity);
                //rb.velocity = -1f*  rb.velocity;
                //transform.Rotate(0f, 180, 0f);
                bounce = false;


                Quaternion rotation = Quaternion.FromToRotation(s * speed, reflectedVelocity);
                transform.rotation = rotation * transform.rotation;

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
