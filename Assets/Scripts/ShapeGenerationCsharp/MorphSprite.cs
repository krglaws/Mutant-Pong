using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphSprite : MonoBehaviour {
    public Sprite  test;
	// Use this for initialization
	public void Start () {

        this.GetComponent<SpriteRenderer>().sprite = test; // Refresh Sprite with a new sprite shows up in drop down menu as Test under Morph Sprite component
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    public void ResetCollider ()
    {
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
    /*
    public void AttachCollider()
    {
        gameObject.AddComponent<PolygonCollider2D>();
    }
    */
}

 

