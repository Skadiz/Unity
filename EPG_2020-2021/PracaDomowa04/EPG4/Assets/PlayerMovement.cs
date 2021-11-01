using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12;
    public float jumpForce = 300;
    private Rigidbody2D rb;
    private bool isGrounded;

    float xDisplacement;

    private Animator anim;

    private bool facingRight = true;

    void Start()
    {
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        xDisplacement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xDisplacement * speed, rb.velocity.y);
        //jump
        if ((Input.GetKeyDown(KeyCode.Space)|| (Input.GetKeyDown(KeyCode.W))) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
        }

        //Animations
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if ((Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.W))){
            anim.SetTrigger("jump");
        }

        if (facingRight == false && xDisplacement > 0)
        {
            Flip();
        }
        else if(facingRight == true && xDisplacement < 0)
        {
            Flip();
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
       
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
   
}
