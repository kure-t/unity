using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class Ballmain : MonoBehaviour
{

    //ランダム関連
    private float radius;
    private int sita;

    public float bottomspeed = 10.0f;
    
    public Rigidbody rb;
    public savesystem savescript;
    public GameObject obj;
    //データ読み込み
    //private float r;
    public float s;      
    private float rogic;
    private float rogicthreshold;

    //時間記録
    public int starttime;
    public int now;
    //
    public int duration;
    public float controltime;//制御をかけ始める時間
    public float controlstarttime;
    private float exptime;//指数関数用時間
    private float inclination=0.27f;
    public float expinclination;
    private float timeinclination = 0.1f;

    //private bool controlswitch=true;

    public GameObject obj1,objsave;
    public SceneScript lifescript;

    private bool b;
    private bool c;
    public bool expsetting;

    //speed調整
    //public float magnification;
    public float bottomspeedlow;
    public float bottomspeedhigh;
    /*
    public float easy = 9.1f;
    public float middle = 16.6f;
    public float hard = 30.2f;
    */
    public float memory;
    //速度設定用
    private float expspeed;
    public int speedsetting;
    //軽量化用
    private float[,] a = new float[17,2];
    private float sqrt;
    //private float nowspeed;
    private List<float> hitstock = new List<float>();
    private List<float> speedstock = new List<float>();

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        //セーブスクリプト読み込み：Parabutton⇒savesystem⇒Ballmain
        obj = GameObject.Find("SaveSystem");
        savescript = obj.GetComponent<savesystem>();
        //r = savescript.randomsetting;
        s = savescript.speedsetting;
       // Debug.Log(r);
        Debug.Log(s);
        //残機スクリプト読み込み
        obj1 = GameObject.Find("SceneScript");
        lifescript = obj1.GetComponent<SceneScript>();
        //以下を追加
        rb = this.GetComponent<Rigidbody>();
        if (lifescript.life == 3 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (lifescript.count >= 1)
            {
                gameroad();
            }
            else
            {
                gamestart();
                Debug.Log("startcheck");
            }
        } else if (lifescript.life <= 2 || SceneManager.GetActiveScene().buildIndex != 1)
        {
            gameroad();//セーブデータから読み取り
        }
        //チェッカーの初期化
        b = false;
        c = true;

        Debug.Log("rogicthreshold" + rogicthreshold);
        sqrt = 1.414f;
    }

    // Update is called once per frame
    void Update()
    {
        //現在時刻
        now = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        duration = now - starttime;
        datasave();
        //Debug.Log("speed=" + rb.velocity.magnitude);

        if (duration > 720000)
        {
            Hitcount();
            Measuretime2();
            Application.Quit();
        }
        
        if (rb.transform.position.z < -11&&c==true)
        {
            //ミスしたらヒットをまとめて書き込む
            Hitcount();
            misscount();
            c = false;
        }
        
        //Debug.Log("speed=" + rb.velocity.magnitude);
        //ボール発射
        if (Input.GetButtonDown("Jump") && rb.velocity == new Vector3(0, 0, 0))
        {
            this.GetComponent<Rigidbody>().AddForce((transform.forward + transform.right) * bottomspeed / sqrt, ForceMode.VelocityChange);
            b = true;
            //発射時間の記録
            Measuretime();
        }

        if (b == true)
        {
            controlstarttime += Time.deltaTime;
            if (expinclination != 0)
            {
                if (controlstarttime >= controltime)
                {
                    if (expsetting==true)
                    {
                        //前半
                        if (expinclination > 0)
                        {
                            AccelerationEXP();
                        }
                        else
                        {
                            AccelerationMax();
                        }
                    }
                    else
                    {
                        //後半
                        if (expinclination > 0)
                        {
                            DecelerationEXP();
                        }
                        else
                        {
                            DecelerationMax();
                        }
                    }
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (b == true)
        {
            if (rb.velocity.magnitude > bottomspeed + 0.5 || rb.velocity.magnitude < bottomspeed - 0.5)
            {
                Debug.Log("speed1=" + rb.velocity.magnitude);
                Debug.Log("speeder");
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
            hitstock.Add(now);
            speedstock.Add(rb.velocity.magnitude);
        }
    }
    //各種制御関数
    void Decelerationlin()
    {
        //減速
        if (bottomspeed > bottomspeedlow)
        {
            bottomspeed = bottomspeed - inclination * Time.deltaTime;
        }
    }
    void DecelerationEXP()
    {
        if (bottomspeed > bottomspeedlow)
        {
            exptime += Time.deltaTime;
            bottomspeed = memory - Mathf.Exp(expinclination * exptime);
        }
    }
    void DecelerationMax()
    {
        //最小値まで一気に減速
        bottomspeed = bottomspeedlow;
    }
    void Accelerationlin()
    {
        //線形加速
        if (bottomspeed < bottomspeedhigh)
        {
            bottomspeed = bottomspeed + inclination * Time.deltaTime;
        }
    }
    void AccelerationEXP()
    {
        //指数関数で加速
        if (bottomspeed < bottomspeedhigh)
        {
            exptime += Time.deltaTime;
            bottomspeed = memory + Mathf.Exp(expinclination * exptime);
        }
    }
    void AccelerationMax()
    {
        //最大値まで一気に加速
        bottomspeed = bottomspeedhigh;
    }
    void RandomUp()
    {
        //ランダム線形にアップ
        if (rogicthreshold < 1.0)
        {
            rogicthreshold += timeinclination * Time.deltaTime;
        }
    }
    void RandomMax()
    {
        //一気にランダムマックス
        rogicthreshold = 1.0f;
    }
    void RandomMin()
    {
        //一気にランダムなし
        rogicthreshold = 0.0f;
    }
    void RandomDown()
    {
        //ランダム線形にダウン
        if (rogicthreshold > 0.0f)
        {
            rogicthreshold -= timeinclination * Time.deltaTime;
        }
    }
    void gamestart()
    {
        speedsetting = (int)s;
        /*
        switch (speedsetting)
        {
            case 1:
                expspeed = easy;
                memory = expspeed;
                break;
            case 2:
                expspeed = middle;
                memory = expspeed;
                break;
            case 3:
                expspeed = hard;
                memory = expspeed;
                break;
        }
        */
        //本実験用
        //実験参加者ごとにアプリをビルドするので毎回確認しましょう
        //減速がfalse加速がtrue
        //実験10
        switch (speedsetting)
        {
            case 1:
                expinclination = -1.0f;
                expsetting = false;
                break;
            case 2:
                expinclination = 0.05668662f;
                expsetting = false;
                break;
            case 3:
                expinclination = 0.17005987f;
                expsetting = false;
                break;
            case 4:
                expinclination = 0;
                break;
            case 5:
                expinclination = 0.08502993f;
                expsetting = false;
                break;
            case 6:
                expinclination = 0.08502993f;
                expsetting = true;
                break;
            case 7:
                expinclination = 0.05668662f;
                expsetting = true;
                break;
            case 8:
                expinclination = -1.0f;
                expsetting = true;
                break;
            case 9:
                expinclination = 0.17005987f;
                expsetting = true;
                break;
                //－はMax
        }
        
        Debug.Log("expinclination=" + expinclination);
        //bottomspeed = s * 5.0f;
        //rogicthreshold = r * 0.50f;
        //時間：ミリ秒
        starttime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
    }
    void gameroad()
    {
        bottomspeed = savescript.savespeed;
        //rogicthreshold = savescript.saverogic;
        starttime = savescript.savestarttime;
        expinclination = savescript.saveinclination;
        exptime = savescript.saveexptime;
        speedsetting = (int)savescript.speedsetting;
        controlstarttime = savescript.contime;
        expsetting = savescript.saveexpsetting;
    }
    public void datasave()
    {
        savescript.savespeed = bottomspeed;
        savescript.saveinclination = expinclination;
        //savescript.saverogic = rogicthreshold;
        savescript.savestarttime = starttime;
        savescript.saveexptime = exptime;
        savescript.contime = controlstarttime;
        savescript.saveexpsetting = expsetting;
    }
    public void Hitcount()
    {
        //測定時間記録
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/filespeed" + s + ".csv");
        sw = fi.AppendText();
        for (int i = 0; i < hitstock.Count; ++i)
        {
            sw.WriteLine("hit,{0},speed,{1}",hitstock[i], speedstock[i]);
        }
        hitstock.Clear();
        speedstock.Clear();
        sw.Flush();
        sw.Close();
    }
    void Measuretime()
    {
        //測定時間記録
        StreamWriter sw;
        FileInfo fi;
        Debug.Log("speedsetting of Measuretime" + speedsetting);
        //fi = new FileInfo(Application.dataPath + "/Filenametest.csv");
        fi = new FileInfo(Application.dataPath + "/filespeed" + s + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("start,{0}", now);
        sw.Flush();
        sw.Close();
    }
    void Measuretime2()
    {
        //測定時間記録
        StreamWriter sw;
        FileInfo fi;
        //fi = new FileInfo(Application.dataPath + "/Filenametest.csv");
        fi = new FileInfo(Application.dataPath + "/filespeed" + s + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("timeup,{0}", now);
        sw.Flush();
        sw.Close();
    }
    void misscount()
    {
        //測定時間記録
        StreamWriter sw;
        FileInfo fi;
        //fi = new FileInfo(Application.dataPath + "/Filenametest.csv");
        fi = new FileInfo(Application.dataPath + "/filespeed" + s + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("miss,{0},speed,{1}", now, rb.velocity.magnitude);
        sw.Flush();
        sw.Close();
    }
}
