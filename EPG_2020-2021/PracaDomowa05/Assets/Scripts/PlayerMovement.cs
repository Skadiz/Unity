using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Text score;
    public float runModifier;
    public float totalScore;
    private Rigidbody2D rb;
    private bool OnGround;
    // Start is called before the first frame update
    void Start()
    {
        runModifier = 1;
        totalScore = 0;
        OnGround = true;
        rb = GetComponent<Rigidbody2D>();
        score = GameObject.Find("Score").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        var axis = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(axis * speed * runModifier, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            rb.AddForce(new Vector2(0, 500));
            OnGround = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            runModifier = 5;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            runModifier = 1;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        OnGround = true;
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Collected coin");
            totalScore += 1;
            score.text = "Score: " + totalScore;
        }
        if (col.gameObject.CompareTag("Food"))
        {
            Debug.Log("Collected food");
            totalScore += 5;
            score.text = "Score: " + totalScore;
        }
    }
    public void ResetScore()
    {
        totalScore = 0;
        score.text = "Score: " + totalScore;
    }
}
