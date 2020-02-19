using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class BallScript : MonoBehaviour {

    public float bottomspeed;
    public Rigidbody rb;
    private int now,starttime;

    private List<float> hitstock = new List<float>();
    private bool b,c;

    public GameObject obj,obj1;
    public CsvReader script1;
    public SceneScript script2;

    // Use this for initialization
    void Start () {

        //以下を追加
        //Csvデータの利用
        rb = GetComponent<Rigidbody>();
        obj = GameObject.Find("Csvreader");
        script1 = obj.GetComponent<CsvReader>();
        obj1 = GameObject.Find("SceneScript");
        script2 = obj1.GetComponent<SceneScript>();
        b = false;
        c = true;
        starttime= DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60
            + DateTime.Now.Second + DateTime.Now.Millisecond/1000;
    }
	
	// Update is called once per frame
	void Update () {
        now = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60
            + DateTime.Now.Second + DateTime.Now.Millisecond / 1000 - starttime;

        if (Input.GetButtonDown("Jump") && rb.velocity == new Vector3(0, 0, 0)){
            this.GetComponent<Rigidbody>().AddForce((transform.forward + transform.right) * bottomspeed,ForceMode.VelocityChange);
            b = true;
        }

        if (rb.transform.position.z < -11 && c == true)
        {
            //ミスしたらヒットとミスを書き込む
            //Hitcount();
            //misscount();
            
            c = false;
        }
    }
    void FixedUpdate()
    {
        if (b == true)
        {
            if (rb.velocity.magnitude > bottomspeed + 1 || rb.velocity.magnitude < bottomspeed - 1)
            {
                Debug.Log("speed1=" + rb.velocity.magnitude);
                SpeedControl2();
                Debug.Log("speed1=" + rb.velocity.magnitude);
            }

            //垂直・水平にしか跳ねなくなったときの調整
            if (Mathf.Abs(rb.velocity.z) < 1)
            {
                Debug.Log("zspeed=" + rb.velocity.z);
                Debug.Log("speed2=" + rb.velocity.magnitude);
                Debug.Log("speeder2");
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z + 5);
                Debug.Log("zspeed=" + rb.velocity.z);
                Debug.Log("speed2=" + rb.velocity.magnitude);
            }
            if (Mathf.Abs(rb.velocity.x) < 1)
            {
                Debug.Log("speeder3");
                Debug.Log("xspeed=" + rb.velocity.x);
                Debug.Log("speed3=" + rb.velocity.magnitude);
                rb.velocity = new Vector3(rb.velocity.x + 5, rb.velocity.y, rb.velocity.z);
                Debug.Log("xspeed=" + rb.velocity.x);
                Debug.Log("speed3=" + rb.velocity.magnitude);
            }
        }
    }
    void SpeedControl2()
    {
        rb.velocity = rb.velocity.normalized * bottomspeed;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //hitstock.Add(now);//ヒットするたびに時間を書き込んでおく
            script2.hitcount++;
        }
    }
    public void Hitcount()
    {
        //パフォーマンス記録
        //hit=1,miss=0としてみる。
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath+"/hitcount" + script1.fileName  + ".csv");//usernameとlabelを書き込む
        sw = fi.AppendText();
        for (int i = 0; i < hitstock.Count; ++i)
        {
            sw.WriteLine("1,{0}", hitstock[i]);
        }
        hitstock.Clear();
        sw.Flush();
        sw.Close();
    }
    void misscount()
    {
        //パフォーマンス記録
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/hitcount" + script1.fileName + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("0,{0}", now);
        sw.Flush();
        sw.Close();
    }
}
