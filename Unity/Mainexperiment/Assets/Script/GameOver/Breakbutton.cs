using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

//ゲーム終了ボタン

public class Breakbutton : MonoBehaviour{

    public GameObject obj;
    public SceneScript script;
    private float mesuretime;
    void Start()
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }
    public void OnClick()
    {
        Debug.Log("breakbutton click!");
        //終了ステージ
        script.timestageMesure();
        //成績を書き込む
        script.PerformanceProgresswrite();
        //コンティニュー回数・終了時間を書き込む
        //アプリケーションを終了する
        Application.Quit();
    }
    /*
    void Mesurebreak()
    {
        //測定時間記録
        mesuretime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        StreamWriter sw;
        FileInfo fi;
        //fi = new FileInfo(Application.dataPath + "/Filenametest.csv");
        fi = new FileInfo(Application.dataPath + "/filespeed.csv");
        sw = fi.AppendText();
        sw.Write(name);
        sw.WriteLine(",{0}", script.count);
        sw.Write("quittime,");
        sw.WriteLine(mesuretime);
        if (SceneManager.GetActiveScene().name != "GameOver")
        {
            sw.WriteLine("score,{0}", script.score);
        }
        sw.Flush();
        sw.Close();
    }
    */
}