using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsoluteTime : MonoBehaviour {
    [Range(0, 100)]
    public float TimeScale = 1.0f;	
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = TimeScale;
	}
}
