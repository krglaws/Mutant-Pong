using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int round = 0;
    const int MAX_SCORE = 5;
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;
    public GUISkin layout;
    GameObject theBall;
    //SpriteRenderer render;

    // Use this for initialization
    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        GA.Init();
        //render = GameObject.Find("MutantPaddle").GetComponent<SpriteRenderer>();
        NextPaddle();
    }

    void NextPaddle()
    {
        SpriteRenderer renderer = GameObject.Find("MutantPaddle").GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>(""+round);
        // reset MutantPaddle collider
        MorphSprite instanceofMorphSprite = GameObject.Find("MutantPaddle").GetComponent<MorphSprite>();
        instanceofMorphSprite.ResetCollider();
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

        if (PlayerScore1 == MAX_SCORE)
        {
            NewGame();
        }
        else if (PlayerScore2 == MAX_SCORE)
        {
            NewGame();
        }
    }

    void NewGame()
    {
        PlayerScore1 = 0;
        PlayerScore2 = 0;
        round++;
        NextPaddle();
        theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
    }

}