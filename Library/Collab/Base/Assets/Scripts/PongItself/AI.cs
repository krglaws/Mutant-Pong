using UnityEngine;
using System;
using System.Collections;

public class AI : MonoBehaviour
{
    public GameObject Ball;
    private Transform CurrentTransform;
    private Rigidbody2D rb2d;
    public float speed = 7.0f;
    public float topBound = 2.25f; // upper bound
    public float bottomBound = -2.25f;
    
    // Boundary upper limit 


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        InvokeRepeating("Move", .02F, .05F);
    }

    // Update is called once per frame
    void Move()
    {
        var vel = rb2d.velocity;
        Vector2 Ballp = Ball.transform.position;
        Vector2 paddle = this.transform.position;
        Vector2 movement = new Vector2(-4.025f, 1f);
        Vector2 DownMovement = new Vector2(-4.025f, -1f);
        if (Ball.transform.position.x < 0)
        {
            if (transform.position.y < Ball.transform.position.y)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (transform.position.y >= Ball.transform.position.y)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                // transform.position = new Vector2(0.0f, -speed);
            }

        }
        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, 0);
        }
        else if (transform.position.y < bottomBound)
        {
            transform.position = new Vector3(transform.position.x, bottomBound, 0);
        }
    }

}





