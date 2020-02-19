using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallSpeed : MonoBehaviour {

    public Slider ballspeed;
    public float speedlevel;
	// Use this for initialization
	void Start () {
        ballspeed.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ValueChangeCheck()
    {
        Debug.Log(ballspeed.value);
        speedlevel = ballspeed.value;
    }
}
