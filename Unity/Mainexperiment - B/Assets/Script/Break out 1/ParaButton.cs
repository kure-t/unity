using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

//予備実験で使用，スライダーなどで値を設定できるようにした
public class ParaButton : MonoBehaviour
{

    public GameObject obj1, obj2, obj3, obj4;
    public RandomSlider script1;
    public PaddleWith script2;
    public BallSpeed script3;
    public savesystem script4;
    //保存用
    public float random, width, speed;

    void Start()
    {
        //他スクリプト読み込み
        /*
        obj1 = GameObject.Find("Slider");
        script1 = obj1.GetComponent<RandomSlider>();
        obj2 = GameObject.Find("PlayerSlider");
        script2 = obj2.GetComponent<PaddleWith>();
        */
        obj3 = GameObject.Find("BallSpeedSlider");
        script3 = obj3.GetComponent<BallSpeed>();
        obj4 = GameObject.Find("SaveSystem");
        script4 = obj4.GetComponent<savesystem>();

    }
    public void OnClick()
    {
        Debug.Log("Button click!");
        //ファイル書き出し
        logsave(1, "hallo");
        //データ保存
        script4.Save();

        //シーン遷移:調整シーン→プレイシーン
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    public void logsave(int num, string txt)
    {
        //保存用
        //random = script1.randomlevel;
        //width = script2.paddlewidth;
        speed = script3.speedlevel;
        StreamWriter sw;
        FileInfo fi;
        //fi = new FileInfo(Application.dataPath + "/Filenametest.csv");
        fi = new FileInfo(Application.dataPath + "/filespeed" + script3.speedlevel + ".csv");
        sw = fi.AppendText();
        //sw.WriteLine("random,paddlewidth,ballspeed");
        //sw.WriteLine("{0},{1},{2}", script1.randomlevel, script2.paddlewidth, script3.speedlevel);
        /*
        sw.Write("raddom");
        sw.WriteLine(",{0}", script1.randomlevel);
        sw.Write("paddlewidth");
        sw.WriteLine(",{0}", script2.paddlewidth);
        */
        sw.Write("ballspeed");
        sw.WriteLine(",{0}", script3.speedlevel);
        sw.Flush();
        sw.Close();
    }
    public float randomset
    {
        get { return this.random; }
        private set { this.random = value; }
    }
    public float widthset
    {
        get { return this.width; }
        private set { this.width = value; }
    }
    public float speedset
    {
        get { return this.speed; }
        private set { this.speed = value; }
    }
}