using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 12;
    public float jumpForce = 300;
    private Rigidbody2D rb;
    private bool isGrounded;

    public float DashForce = 5000;
    public float StartDashTimer = 0.25f;

    float CurrentDashTimer;
    float DashDirection;
    float xDisplacement;
    bool isDashing;



    void Start()
    {
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //move
        xDisplacement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xDisplacement * speed, rb.velocity.y);
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
        }
        //run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 32;
        }
        else { speed = 12; }
        //dash
        if (Input.GetKeyDown(KeyCode.E) && xDisplacement != 0)
        {
            isDashing = true;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
            DashDirection = (int)xDisplacement;
        }
        if (isDashing)
        {
            rb.velocity = transform.right * DashDirection * DashForce;
            CurrentDashTimer -= Time.deltaTime;
            if (CurrentDashTimer <= 0)
            {
                isDashing = false;

            }
        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
        if (col.gameObject.name.Equals("MovingPlatform"))
        {
            this.transform.parent = col.transform;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("MovingPlatform"))
        {
            this.transform.parent = null;
        }
    }
}
