using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//予備実験で使用
public class BottomWallScriptMain : MonoBehaviour
{

    public GameObject obj,obj1;
    public SceneScript script;
    public savesystem script1;
    public Transform ball1;

    private float mesuretime;
    public string time;
    // Use this for initialization
    void Start()
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
        obj1 = GameObject.Find("SaveSystem");
        script1 = obj1.GetComponent<savesystem>();
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        //misscount();
        if (script.life > 0)
        {
            Instantiate(ball1);
            script.life--;
            //ミスした時の記録をとる，ミスは回数が少ないから書き込んでもいいかも
        }
        else if (script.life == 0)
        {
            script.breakscore = 0;
            Cursor.visible = true;
            SceneManager.LoadScene(6, LoadSceneMode.Single);
            time = "GameOvertime";
            Mesuretime(time);
        }
    }
    void Mesuretime(string name)
    {
        //測定時間記録
        mesuretime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        StreamWriter sw;
        FileInfo fi;
        //fi = new FileInfo(Application.dataPath + "/Filenametest.csv");
        fi = new FileInfo(Application.dataPath + "/filespeed" + script1.speedsetting + ".csv");
        sw = fi.AppendText();
        sw.Write(name);
        sw.WriteLine(",{0}", mesuretime);
        sw.WriteLine("score,{0}", script.score);
        sw.Flush();
        sw.Close();
    }
    void misscount()
    {
        //測定時間記録
        mesuretime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        StreamWriter sw;
        FileInfo fi;
        //fi = new FileInfo(Application.dataPath + "/Filenametest.csv");
        fi = new FileInfo(Application.dataPath + "/filespeed" + script1.speedsetting + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("miss,{0},speed,{1}", mesuretime,script1.savespeed);
        sw.Flush();
        sw.Close();
    }
}
