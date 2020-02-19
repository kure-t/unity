using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

    public GUIStyle style1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Game Over !!", style1);
    }
}
