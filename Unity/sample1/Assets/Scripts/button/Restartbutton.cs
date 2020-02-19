using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restartbutton : MonoBehaviour {
    public GameObject obj;
    public Transition script;

    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Transition");
        script = obj.GetComponent<Transition>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick()
    {
        Debug.Log("Restartbutton click!");
    
        //スコアとライフを回復
        script.score = 0;
        script.number = 0;
        script.timestageMesure();
        SceneManager.LoadScene("Stage", LoadSceneMode.Single);
    }
}
