using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    
    Rigidbody2D rb2d;
    Vector2 vel;
   // float waitTime = 3f;
    float currentXposition = 0f;
    float currentYposition = 2.51f;

    int stuckTimerX = 0;
    int stuckTimerY = 0;
    int goalTimer = 0;
    int player1Scorecheck = 0;
    int player2Scorecheck = 0;

    void Update()
    {
        stuckTimerX++;
        if (stuckTimerX % 50 == 0)
           if (DeltaXY(transform.position.x, currentXposition) < .02)
            {
                Debug.Log("Ball stuck, resetting...");
                ResetBall();
                GoBall();
                stuckTimerX = 0;
            }
            else currentXposition = transform.position.x;

        stuckTimerY++;
        if (stuckTimerY % 50 == 0)
            if (DeltaXY(transform.position.y, currentYposition) < .02)
            {
                Debug.Log("Ball stuck, resetting...");
                ResetBall();
                GoBall();
                stuckTimerY = 0;
            }
            else currentYposition = transform.position.y;

        goalTimer++;
        if (goalTimer >= 350)
        {
            if (player1Scorecheck == GameManager.PlayerScore1 && player2Scorecheck == GameManager.PlayerScore2)
            {
                Debug.Log("Too long since goal scored, resetting...");
                ResetBall();
                GoBall();
                goalTimer = 0;
            }
            else
            {
                player1Scorecheck = GameManager.PlayerScore1;
                player2Scorecheck = GameManager.PlayerScore2;
                goalTimer = 0;
            }
        }
    }

    void GoBall()
    {
        float rand = UnityEngine.Random.Range(0, 2);
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

    void BallCheck()
    {
        if (vel.x == 0)
        {
            ResetBall();
        }
    }
    
    static double DeltaXY(float x1, float x2)
    {
        return Math.Abs(x1 - x2);
    }

}