using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restartbutton : MonoBehaviour {
    public GameObject obj;
    public SceneScript script;


    void Start()
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }
    public void OnClick()
    {
        Debug.Log("Restartbutton click!");
        //コンティニュー回数を記録する
        script.count += 1;
        //もう一度スクリプトを読み込む
        //script.check = true;
        //ライフ等を元に戻す
        script.score = 0;
        script.life = 3;
        
        //シーン遷移
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
