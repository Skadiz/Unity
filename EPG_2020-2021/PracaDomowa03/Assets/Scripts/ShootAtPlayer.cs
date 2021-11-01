using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtPlayer : MonoBehaviour
{
    public GameObject bullet;
    public float delay = 1;
    private float timer;
    private bool shoot;
    private Transform player;
    void Start()
    {
        timer = 0;
        shoot = false;
    }
    void Update()
    {
        if (shoot)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                timer = 0;
                Shoot();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        shoot = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        shoot = false;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        player = col.gameObject.transform;
        shoot = true;
    }
    void Shoot()
    {
    
        
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
