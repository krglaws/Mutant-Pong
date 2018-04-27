using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMutantPaddle : MonoBehaviour
{
    GameObject mutantPaddle;

    public void onStart() {

        // create mutant paddle and components
        mutantPaddle = new GameObject();
        mutantPaddle.AddComponent<Transform>();
        mutantPaddle.AddComponent<SpriteRenderer>();
        mutantPaddle.AddComponent<PolygonCollider2D>();
        mutantPaddle.AddComponent<MorphSprite>();

        // set paddle position
        mutantPaddle.GetComponent<Transform>().position.Set(0f, 0f, 0f);

        // add sprite renderer
        //mutantPaddle.GetComponent<SpriteRenderer>().sprite = new Sprite().;
    }

}
