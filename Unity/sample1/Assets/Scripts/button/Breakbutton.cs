using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakbutton : MonoBehaviour {
    public GameObject obj;
    public Transition script;
    // Use this for initialization
    void Start ()
    {
        obj = GameObject.Find("Transition");
        script = obj.GetComponent<Transition>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick()
    {
        Debug.Log("breakbutton click!");
        //終了ステージ
        script.timestageMesure();
        //コンティニュー回数・終了時間を書き込む
        //アプリケーションを終了する
        Application.Quit();
    }
}
