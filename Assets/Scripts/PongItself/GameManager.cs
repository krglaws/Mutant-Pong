using UnityEngine;

public class GameManager : MonoBehaviour
{
    static int currIndividual = 0;
    const int MAX_SCORE = 25;
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;
    static GUISkin layout;
    GameObject theBall;

    void Start()
    {
        theBall = GameObject.FindGameObjectWithTag("Ball");
        GA.Init();
        NextPaddle();
    }

    // setup next paddle at beginning of each game
    void NextPaddle()
    {
        // load next paddle sprite
        SpriteRenderer renderer = GameObject.Find("MutantPaddle").GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>(""+currIndividual);

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
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 30;
        myStyle.normal.textColor = Color.white;
        GUI.skin = layout;
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore1, myStyle);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + PlayerScore2, myStyle);

        GUI.Label(new Rect(Screen.width / 2 - 75, 20, 100, 100), "Gen: "+GA.genNum + ", Ind: " + currIndividual, myStyle);

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
        GA.SendScores(PlayerScore1, PlayerScore2, MAX_SCORE, currIndividual);
        PlayerScore1 = 0;
        PlayerScore2 = 0;

        currIndividual++;

        if (currIndividual >= GA.POP_SIZE)
        {
            currIndividual = 0;
            GA.NextGen();
        }

        NextPaddle();
        theBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
    }
}