using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedLog : MonoBehaviour {

    GameObject obj;
    Ballmain script;
    private float speed;
	void Start () {
        obj = GameObject.Find("Ball1");
        script = obj.GetComponent<Ballmain>();
    }
	
	// Update is called once per frame
	void Update () {
        speed = script.bottomspeed;
        GetComponent<Text>().text = speed.ToString("F2");
	}
}
