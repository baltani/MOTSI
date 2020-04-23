using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class playercontroller : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    Collider2D mainCollider;
    // Check every collider except Player and Ignore Raycast
    LayerMask layerMask = ~(1 << 2 | 1 << 8);
    Transform t;

    private Animator anim;
    private bool is_crouching;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        gameObject.layer = 8;

        if (mainCamera)
            cameraPos = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        is_crouching = Input.GetKey(KeyCode.S);



        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || r2d.velocity.x > 0.01f))
        {
            if(!is_crouching)
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                transform.Rotate(0f, 180f, 0f);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                transform.Rotate(0f, 180f, 0f);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Camera follow
        if (mainCamera)
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        
        
        
        
        //Animations 

        //RUN_animation
        anim.SetBool("isrunning", moveDirection != 0);

        //JUMP_animation
        anim.SetBool("intheair", !isGrounded);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("jumping"); 
        }




        //ATTACK_animation
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("attack");
        }


<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.S) && isGrounded )
=======
        //CROUCH_animations
        if (isGrounded && (moveDirection == 0))
>>>>>>> 37b3343f25fabe5dce168c700776c74deb043251
        {
            anim.SetBool("is_crouching", Input.GetKey(KeyCode.S));
        }
        else if (Input.GetKeyDown(KeyCode.S) && isGrounded && !(moveDirection == 0))
        {
            anim.SetTrigger("crouchdash");
        }
    }



        void FixedUpdate()
        {
            Bounds colliderBounds = mainCollider.bounds;
            Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, 0.1f, 0);
            // Check if player is grounded
            isGrounded = Physics2D.OverlapCircle(groundCheckPos, 0.23f, layerMask);

            // Apply movement velocity
            r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

            // Simple debug
            Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, 0.23f, 0), isGrounded ? Color.green : Color.red);
        }
 }