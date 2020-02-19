using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

//予備実験のみで使用
public class savesystem : MonoBehaviour {

    public GameObject obj,obj1;
    public ParaButton script;
    public Ballmain script1;
    public float randomsetting, paddlewidthsetting, speedsetting;
    public bool saveexpsetting;
    public float savespeed,saverogic,saveinclination, saveexptime,contime;
    public int savestarttime;
    //private bool m1;
    private int now;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void Save()
    {
        //パラメータの保存
        obj = GameObject.Find("Button");
        script = obj.GetComponent<ParaButton>();
        speedsetting = script.speedset;
        Debug.Log(speedsetting);
    }
    public void Measurement1()
    {
        //不使用
        //測定データ記録
        obj1 = GameObject.Find("Ball1");
        script1 = obj1.GetComponent<Ballmain>();
        now = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        StreamWriter sw;
        FileInfo fi;
        //fi = new FileInfo(Application.dataPath + "/Filenametest.csv");
        fi = new FileInfo(Application.dataPath + "/filespeed" + speedsetting + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("starttime,{0}",now);
        sw.Flush();
        sw.Close();
    }
}
