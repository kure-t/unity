using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionnaire : MonoBehaviour {

    public GUIStyle style1;
    public GameObject obj;
    public SceneScript script;

    // Use this for initialization
    void Start()
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Answer a questionnaire about stage"+script.i, style1);
    }
}
