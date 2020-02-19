using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Playermain : MonoBehaviour
{

    //private float accel = 150.0f;//加える力の大きさ
    private Rigidbody rb;
    private float direction;
    public GameObject obj;
    public savesystem script2;
    public float p,P,width;
    public float paddleincliment=3.5f;
    bool Move1;
    bool Move2;

    public int starttime;
    public int now;
    public int duration;

    private float positionfield;
    private Vector3 position;
    private Vector3 screenToWorldPointPosition;

    // Use this for initialization
    void Start()
    {
        
        rb = this.GetComponent<Rigidbody>();
        obj = GameObject.Find("SaveSystem");
        script2 = obj.GetComponent<savesystem>();
        /*
        p = script2.paddlewidthsetting;
        int pset = (int)p;
        //大きさ調整
        switch (pset)
        {
            case 1:
                width = 2.6f;
                break;
            case 2:
                width = 5.2f;
                break;
            case 3:
                width = 7.8f;
                break;
        }
                Debug.Log(p);
                */
        //this.gameObject.transform.localScale = new Vector3(width,this.gameObject.transform.localScale.y,this.gameObject.transform.localScale.z);
        TimeMeasurement();
        positionfield = (39 - width) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //now = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        //duration = now - starttime;

        //キーボード操作によるplayerの移動
        //direction = Input.GetAxis("Horizontal");
        //rb.transform.Translate(direction * 40 * Time.deltaTime, 0, 0);
        //速度取得
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("速度ベクトル：" + rb.velocity);
            Debug.Log("速度" + rb.velocity.magnitude);
        }
        //マウスによるplayerの移動
        //Vector3でマウス位置座標を取得する
        position = Input.mousePosition;
        //Z軸修正
        position.z = 50f;
        //マウスの一座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        //枠外補正
        if (screenToWorldPointPosition.x > positionfield)
        {
            screenToWorldPointPosition.x = positionfield;
        }
        else if (screenToWorldPointPosition.x < -1 * positionfield)
        {
            screenToWorldPointPosition.x = -1 * positionfield;
        }
        //ワールド座標に変換されたマウス座標を代入
        rb.transform.position = new Vector3(screenToWorldPointPosition.x, 0, -10f);
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("速度ベクトル：" + rb.velocity);
            Debug.Log("速度" + rb.velocity.magnitude);
        }
        */
        //キーボード操作によるplayerの大きさ変更(デバック用)
        
        if (this.gameObject.transform.localScale.x < 39.0f)
        {
            if (Input.GetButton("Fire1"))
            {
                this.gameObject.transform.localScale = new Vector3(
                    this.gameObject.transform.localScale.x + 0.5f,
                    this.gameObject.transform.localScale.y,
                    this.gameObject.transform.localScale.z);
            }
        }
        if (this.gameObject.transform.localScale.x > 0.5f)
        {
            if (Input.GetButton("Fire2"))
            {
                this.gameObject.transform.localScale = new Vector3(
                   this.gameObject.transform.localScale.x - 0.5f,
                   this.gameObject.transform.localScale.y,
                   this.gameObject.transform.localScale.z);
            }
        }
        

    }
    void PaddleLong()
    {
        if (P < 35.0f)
        {
            P = width + paddleincliment*Time.deltaTime;
            this.gameObject.transform.localScale = new Vector3(P, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
        }
    }
    void PaddleShort()
    {
        if (P > 1.0f)
        {
            P = width - paddleincliment * Time.deltaTime;
            this.gameObject.transform.localScale = new Vector3(P, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
        }

    }
    public void TimeMeasurement()
    {
        //測定データ記録
        starttime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        StreamWriter sw;
        FileInfo fi;
        //fi = new FileInfo(Application.dataPath + "/Filenametest.csv");
        fi = new FileInfo(Application.dataPath + "/filespeed" + script2.speedsetting + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("starttime,{0}",starttime);
        sw.Flush();
        sw.Close();
    }
    /*
    void Hitcount()
    {
        //測定時間記録
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/filespeed" + script2.speedsetting + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("hit,{0},speed,{1}", now, script2.savespeed);
        sw.Flush();
        sw.Close();
    }
    */
}
