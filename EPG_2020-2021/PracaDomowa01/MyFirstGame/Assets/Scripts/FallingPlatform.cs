using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 currentPosition;
    bool movingBack;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player")&&movingBack==false)
        {
            Invoke("FallPlatform", 1f);

        }
    }
    void FallPlatform()
    {
        rb.isKinematic = false;
        Invoke("BackPlatform", 1f);
    }
    void BackPlatform()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        movingBack = true;
    }
    private void Update() {
        if (movingBack == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPosition, 20f * Time.deltaTime);
        }
        if (transform.position.y == currentPosition.y)
        {
            movingBack = false;
        }
    }
}
