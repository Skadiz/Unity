using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Patruler : MonoBehaviour
{
    public float speed;

    public int positionOfPatrol;
    public Transform point;
    bool moveingRight;

    Transform player;
    public float stoppingDistance;

    bool chill = false;
    bool angry = false;
    bool goBack = false;
    bool attacking = false;

    bool facingRight = true;



    // Start is called before the first frame update
    void Start()
    {

         //rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //var pm = GetComponent<PlayerMovement>();
        if (transform.position.x < player.position.x && !facingRight) Flip();

        else if (transform.position.x > player.position.x && facingRight) Flip();
 
        if (Vector2.Distance(transform.position,point.position) < positionOfPatrol && angry==false)
        {
            chill = true;
        }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance )
        {
            angry = true;
            chill = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;

        }
        if (chill == true)
        {
            Chill();
            attacking = false;

        }
        else if (angry == true && attacking == false)
        {
            Angry();
        }
        else if (goBack == true )
        {
            GoBack();
        }
    }
    void Chill()
    {
        if(transform.position.x>point.position.x + positionOfPatrol) {
            moveingRight = false;
        }
        else if(transform.position.x < point.position.x - positionOfPatrol)
        {
            moveingRight = true;
        }
        if (moveingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            //rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        speed = 5;
    }
    void Angry()
    {
      
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
        speed = 12;
    }
    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        speed = 5;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
       
        if (col.gameObject.name.Equals("Player"))
        {
            angry = false;
            //GoBack();
            var health = col.gameObject.GetComponent<PlayerMovement>().health;
            health -= 1;
            
            Destroy(this.gameObject);
            Debug.Log("player");
        }
        
    }

    
}
