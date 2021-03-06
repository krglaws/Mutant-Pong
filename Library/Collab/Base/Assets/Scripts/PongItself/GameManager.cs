﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;
    public static string result = "";

    public GUISkin layout;

    GameObject theBall;

    // Use this for initialization
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
    }

    public static void Score(string wallID)
    {
        if (wallID == "RightWall")
        {
            PlayerScore1++;
        }
        else
        {
            PlayerScore2++;
        }
    }

    void OnGUI()
    {
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore1);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + PlayerScore2);

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            PlayerScore1 = 0;
            PlayerScore2 = 0;
            theBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        if (PlayerScore1 == 5)
        {
			GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER ONE WINS\nrestarting...");
			PlayerScore1 = 0;
			PlayerScore2 = 0;
            GA.DrawNewPaddle("0 0 50 100 100 0");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (PlayerScore2 == 5)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER TWO WINS\nrestarting...");
			PlayerScore1 = 0;
			PlayerScore2 = 0;
            GA.DrawNewPaddle("25 25 25 75 75 75 75 25");
            theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }
}