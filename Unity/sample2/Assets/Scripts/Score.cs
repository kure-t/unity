using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public GUIStyle style, style1;
    public GameObject obj;
    public Transition script;
    // Use this for initialization
    void Start()
    {

        obj = GameObject.Find("Transition");
        script = obj.GetComponent<Transition>();
        
    }
    private void OnGUI()
    {
        style.fontStyle = FontStyle.Bold;
        style.fontSize = 30;
        GUI.Label(new Rect(10, 10, 200, 40), "Score", style);
        GUI.Label(new Rect(10, 50, 200, 40), "" + script.score, style);
        GUI.Label(new Rect(10, 120, 200, 40), "消した数", style);
        GUI.Label(new Rect(10, 160, 200, 40), "" + script.number + "個", style);

    }
}
