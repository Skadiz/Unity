
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
     public bool OnGround;
    private float runModifier;
    //private Transform enemy;

    public int health;
   
    [HideInInspector]
    public bool hasKey;


    void Start()
    {
       // enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        rb = GetComponent<Rigidbody2D>();
        OnGround = true;
        runModifier = 1;
        hasKey = false;
        sr = GetComponent<SpriteRenderer>();
    }
   
    
    void Update()
    {
        var axis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(axis * speed * runModifier, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            rb.AddForce(new Vector2(0, 500));
            OnGround = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.AddForce(new Vector2(axis * 1000, 0));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && OnGround)
        {
            runModifier = 5;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)){
            runModifier = 1;
        }
        //health
        /*
        if (health > numberOfLives)
        {
            health = numberOfLives;
        }
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < health)
            {
                lives[i].sprite = fullLive;
            }
            else
            {
                lives[i].sprite = emptyLive;
            }
            if (i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
        */
        if(health == 1)
        {
            sr.color = Color.red;
        }
        if (health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            health -= 1;
        }
        OnGround = true;
        if (col.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
            
        }
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            
            Debug.Log("Player collected a key");
        }
        if(col.gameObject.CompareTag("Door") && hasKey)
        {
            hasKey = false;
            Destroy(col.gameObject);
        }
        
    }
}
