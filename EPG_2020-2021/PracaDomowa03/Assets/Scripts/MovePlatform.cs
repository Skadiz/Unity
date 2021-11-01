using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public bool moveUp;

    public float speed;
    public Transform target;
    public GameObject player;
    void Update()
    {
        var platformPosition = this.transform.position;
        float step = speed * Time.deltaTime;
        if (platformPosition == target.position)
        {
            moveUp = false;
        }
        if (moveUp)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, step);
            player.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        moveUp = true;

    }
}
