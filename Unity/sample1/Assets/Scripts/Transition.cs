using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transition : MonoBehaviour
{
    public int score = 0;
    public int number = 0;
    public int goal=400 ;

    public GameObject obj1;
    public Csvreader script1;

    private float mesuretime;
    public string time;
    public string result;
    public int timememory;
    public int starttime, now;
    public float duration;
    public int stoptime;

    static Transition _instance = null;
    //オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static Transition instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<Transition>()); }
    }
    //x??y xがnull以外の場合はxを返します。nullの場合はyを返します。
    void Awake()
    {
        //Awakeはダメoverrideするとダメらしいが、overrideは多分しないのでとりあえずこれで
        //※オブジェクトが重複していたらここで破棄される
        //自身がインスタンスでなければ自滅
        if (this != instance)
        {
            Destroy(gameObject);
            return;
        }
        //以降破棄しない
        DontDestroyOnLoad(gameObject);
    }
    void OnDestroy()
    {
        //※破棄時に、登録した実体の解除を行っている
        //自身がインスタンスなら登録を解除
        if (this == instance) _instance = null;

    }
    // Use this for initialization

    void Start()
    {
        starttime = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond / 1000;
        duration = 0;
        obj1 = GameObject.Find("Csvreader");
        script1 = obj1.GetComponent<Csvreader>();
        if (script1.fileName == "practice")
        {
            stoptime = 180;
        }
        else
        {
            stoptime = 900;
        }
        timestageID();
        timestageMesure();
       
    }

    // Update is called once per frame
    void Update()
    {

        duration += Time.deltaTime;//秒で取得
        now = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond / 1000;

        if (Input.GetKeyDown("escape"))
        {
            timestageMesure();
            Application.Quit();
        }
        if (duration > stoptime)
        {
            //データ書き込みメソッド
            timestageMesure();
            Application.Quit();
        }
    }

    public void StageClear()
    {
        if (number >= goal)
        {
            timestageMesure();
            SceneManager.LoadScene("Clear", LoadSceneMode.Single);
        }
    }

    public void timestageMesure()
    {
        //基本的にScenescriptのstarttimeを基準とする
        //測定時間記録
        //mesuretime = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond / 1000 - starttime;
     

        //scoreも同時に書き込んでみる
        StreamWriter sw1;
        FileInfo fi1;
        fi1 = new FileInfo(Application.dataPath + "/Progress/ScoreProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw1 = fi1.AppendText();
        sw1.WriteLine("{0},{1}", duration, score);
        sw1.Flush();
        sw1.Close();
    }
    /// </summary>
    public void timestageID()
    {
        //scoreも同時に書き込んでみる
        StreamWriter sw1;
        FileInfo fi1;
        fi1 = new FileInfo(Application.dataPath + "/Progress/ScoreProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw1 = fi1.AppendText();
        sw1.WriteLine("Time,Score");
        sw1.Flush();
        sw1.Close();

        
    }
}