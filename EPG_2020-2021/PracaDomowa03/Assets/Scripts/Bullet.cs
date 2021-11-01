using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
