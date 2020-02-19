using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour {

    public GameObject obj;
    public Transition script;
    //gameover
    public GUIStyle style1;
    //ranking
    public GUIStyle style2;
    public GUIStyle style3;
    // Use this for initialization
    void Start()
    {
        obj = GameObject.Find("Transition");
        script = obj.GetComponent<Transition>();
    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Clear!!", style1);
        GUI.Label(new Rect(250, 100, 100, 50), "Score " + script.score , style3);
    }
}
