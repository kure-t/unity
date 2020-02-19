using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakbutton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick()
    {
        Debug.Log("breakbutton click!");
        //終了ステージ
       
        //コンティニュー回数・終了時間を書き込む
        //アプリケーションを終了する
        Application.Quit();
    }
}
