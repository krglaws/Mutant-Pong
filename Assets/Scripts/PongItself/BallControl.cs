using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector2 vel;

    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            rb2d.AddForce(new Vector2(20, -15));
        }
        else
        {
            rb2d.AddForce(new Vector2(-20, -15));
        }
    }

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Faster", .02F, .25F);
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 2);
    }
    void Faster()
    {
        vel.y += vel.y*.7f;
        vel.x += vel.x*.7f;
        rb2d.velocity += vel;
    }
    void ResetBall()
    {
        vel = new Vector2(0, 0);
        rb2d.velocity = vel;
        Vector2 BallPosition = new Vector4(0.1025458f, 2.550819f);
        transform.position = BallPosition;
    }

    void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2.0f) + (coll.collider.attachedRigidbody.velocity.y / 3.0f);
            rb2d.velocity = vel;
        }
    }

    


}