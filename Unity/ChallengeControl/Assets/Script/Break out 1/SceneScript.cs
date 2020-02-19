using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    //このスクリプトはシーン遷移の監督として使う
    //ライフ・スコア
    public int life = 3;
    public int score = 0;
    //コンティニュー数・ブロックは回数
    public int count = 0;
    public float breakscore = 0;
    public float totalreturnscore = 0;
    private float stageclearcount;
    public float returnscorethreshold;
    //ステージ切換
    public int stagecount = 0;
    //速度保存
    public float speedmemory = 0;
    //CSV読み込み
    private float bt,wid;
    public int i = 0;
    
    //時間
    private float mesuretime;
    public string time;
    public string result;
    public int timememory;
    
    //スクリプト読み込み用
    public GameObject obj,obj1,ball1;
    public CsvReader script1;
    public BallScript script2;
    public Player script3;
    
    //performance
    private float misspercentage;
    private float efficiency;
    public float hitcount, misscount;
    public bool check;

    //stagechecker
    public int checker;

    //シングルトン
    //現在存在しているオブジェクト実体の記憶領域
    static SceneScript _instance = null;
    //オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static SceneScript instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<SceneScript>()); }
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
    void Start()
    {
        //Csvデータの利用
        obj = GameObject.Find("Csvreader");
        script1 = obj.GetComponent<CsvReader>();
        if (script1.csvDatas[0].Length >= 5)
        {
            returnscorethreshold = 50;
        }
        else
        {
            returnscorethreshold = 35;
        }
    }

    // Update is called once per frame
    void Update()
    {


        //ballがinstanciateで生成されるときも読み込めるように
        if (ball1 == null)
        {
            ball1 = GameObject.FindWithTag("Ball");
            if (ball1 != null)
            {
                script2 = ball1.GetComponent<BallScript>();
                if (float.TryParse(script1.csvDatas[i][1], out bt))
                {
                    script2.bottomspeed = bt;
                }
                else
                {
                    Debug.Log("string could not be parsed");
                }
            }
        }
        //Playerも同じく
        if (obj1 == null)
        {
            obj1 = GameObject.Find("Player");
            if (obj1 != null)
            {
                script3 = obj1.GetComponent<Player>();
                if (float.TryParse(script1.csvDatas[i][2], out wid))
                {
                    script3.width = wid;
                    script3.widthset();
                }
                else
                {
                    Debug.Log("string could not be parsed");
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //速度記憶
            //speedmemory = script.bottomspeed;
            if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
            {
                //時間記憶
                Debug.Log("breakscore" + breakscore);
                //クリアしたらヒットをまとめて書き込む
                //script2.Hitcount();
                //PerformanceMesurement();
                stageclearcount++;
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            //速度記憶
            //speedmemory = script.bottomspeed;
            if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
            {
                //時間記憶
                Debug.Log("breakscore" + breakscore);
                //クリアしたらヒットをまとめて書き込む
                //script2.Hitcount();
                //PerformanceMesurement();
                stageclearcount++;
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }
        if (totalreturnscore > returnscorethreshold)
        {
            i++;
            totalreturnscore = 0;
            score = 0;
            //script2.Hitcount();
            SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
            PerformanceMesurement();
            StageChecker();
        }
        if (i >= script1.csvDatas.Count)
        {
            Application.Quit();
        }
        totalreturnscore = hitcount + misscount + 1 + stageclearcount;
    }
    /*
    void finishtime(string name,string name2)
    {
        //測定時間記録
        mesuretime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/filespeed"+script.speedsetting+".csv");
        sw = fi.AppendText();
        sw.Write(name);
        sw.WriteLine(",{0}", mesuretime);
        sw.Write(result);
        sw.WriteLine(",{0}",life);
        sw.Flush();
        sw.Close();
    }
    */


    /// <summary>
    /// パフォーマンス測定メソッド
    /// パラメータごとの規定回数が終了したらbreakscore,hitcount,misscount,の値からパフォーマンスを出力
    /// </summary>
    
    public void PerformanceMesurement()
    {
        misspercentage = misscount / (hitcount+ 1 + misscount+stageclearcount);
        efficiency = breakscore/(hitcount+1+stageclearcount+misscount);
        Debug.Log(misspercentage);
        Debug.Log(efficiency);
        performancewrite();
        breakscore = 0;
        hitcount = 0;
        misscount = 0;
        stageclearcount = 0;
    }
    void performancewrite()
    {
        //パフォーマンス記録
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/Performance" + script1.fileName + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}",script1.csvDatas[i-1][0],
            script1.csvDatas[i-1][1],script1.csvDatas[i-1][2],script1.csvDatas[i-1][3],i,misspercentage,efficiency,misspercentage/efficiency);
        sw.Flush();
        sw.Close();
    }
    void StageChecker()
    {
        if (script1.csvDatas[0].Length >= 5)
        {
            //ステージをチェックする
            if (script1.csvDatas[i][4] == "2")
            {
                checker = 1;
                life = 7;
            }
            else if (script1.csvDatas[i][4] == "3")
            {
                checker = 2;
                life = 11;
            }
            else
            {
                checker = 0;
                life = 3;
            }
        }
        else
        {
            checker = 0;
            life = 3;
        }
    }
}
