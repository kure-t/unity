using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour {
    public GameObject obj;
    public Transition script;
    //gameover
    public GUIStyle style1;
    //ranking
    public GUIStyle style2;
    public GUIStyle style3;
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Transition");
        script = obj.GetComponent<Transition>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Game Over !!", style1);
        GUI.Label(new Rect(250, 100, 100, 50), "残り " + (script.goal - script.number)+"個…", style3);
    }
}
