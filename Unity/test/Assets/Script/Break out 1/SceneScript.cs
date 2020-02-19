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
    public int life;
    public int baselife;
    public int score = 0;
    public int Maxscore = 0;
    //コンティニュー数・ブロックは回数
    public float count = 0;
    public float totalreturnscore = 0;
    
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
    public int starttime,now;
    public float duration;
    public int stoptime;
    
    //スクリプト読み込み用
    public GameObject obj,obj1,ball1;
    public CsvReader script1;
    public BallScript script2;
    public Player script3;
    
    //performance　本実験用
    private float misspercentage,totalmisspercentage;
    private float efficiency,totalefficiency;
    private float stageclearcount;
    private float totalhit, totalmiss, totalbreakscore;

    public float[] missratestock = new float[7];
    public float[] efficiencystock = new float[7];
    public float[] thresholdstock = new float[7];

    public float hitcount, misscount;
    public float breakscore;

    public bool check;
    private bool pause = false;

    //stagechecker
    public int checker;
    public int flags;
    public int stage;

    //ランキング内容保存
    public string[,] ranking = new string[5, 2]
{
        {"AAA","5000" },
        {"AAA","4000" },
        {"AAA","3000" },
        {"AAA","2000" },
        {"AAA","1000" }
};

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
        starttime = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond / 1000;
        duration = 0;
        //終了時間設定
        if (script1.fileName == "practice")
        {
            stoptime = 180;
        }else
        {
            stoptime = 900;
        }
        //ブロック数の判別
        if (script1.csvDatas[0][4] == "1")
        {
            baselife = 3;
            life = 3;
        }else if (script1.csvDatas[0][4] == "2")
        {
            baselife = 7;
            life = 7;
        }
        timestageID();//ステージIDの記録
        stagecount = 1;//初めの一回だけステージカウントをしておく
        timestageMesure();//ステージ開始記録
        stage = 1;
        thresholdstock[0]=count;
        missratestock[0]=count;
        efficiencystock[0]=count;
    }

    // Update is called once per frame
    void Update()
    {
        duration += Time.deltaTime;//秒で取得
        now = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond / 1000;

        //ballがinstanciateで生成されるときもCSVから読み込めるように
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
        StageClear2();

        //ポーズシーン
        if (Input.GetKeyDown("p"))
        { 
            if (pause)
            {
                SceneManager.UnloadSceneAsync(9);
                pause = false;
                Time.timeScale = 1;
            }
            else
            {
                SceneManager.LoadSceneAsync(9, LoadSceneMode.Additive);
                pause = true;
                Time.timeScale = 0;
            }
        }
        //ゲームプレイ中はescapeで終了できるようにする．
        if (SceneManager.GetActiveScene().buildIndex != 6 &&
            SceneManager.GetActiveScene().buildIndex != 7 &&
            SceneManager.GetActiveScene().buildIndex != 8)
        {
            if (Input.GetKeyDown("q"))
            {
                //データ書き込みメソッド
                timestageMesure();
                PerformanceMesurement();
                PerformanceProgresswrite();
                script2.Hitcount();
                Application.Quit();
            }
            //自動終了機能
            //15分経過したら終了
            if (duration > stoptime)
            {
                //データ書き込みメソッド
                timestageMesure();
                PerformanceMesurement();
                PerformanceProgresswrite();
                script2.Hitcount();
                Application.Quit();
            }
        }
        
    }

    void StageClear2()
    {
        if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
        {
            //stageclear時なので書き込む
            //flagは次のステージを誘導する
            //stageは現在ステージの表示に使用している
            //B=1
            //
            if (baselife == 3)
            {
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 0:
                        break;
                    case 1:
                        SceneManager.LoadScene(6, LoadSceneMode.Single);
                        PerformanceMesurement();
                        flags = 2;
                        stage = 2;
                        //life++;
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        script2.MicroclearWrite();
                        timestageMesure();

                        break;
                    case 2:
                        SceneManager.LoadScene(6, LoadSceneMode.Single);
                        PerformanceMesurement();
                        flags = 3;
                        stage = 3;
                        //life++;
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        script2.MicroclearWrite();
                        timestageMesure();

                        break;
                    case 3:
                        SceneManager.LoadScene(6, LoadSceneMode.Single);
                        PerformanceMesurement();
                        flags = 4;
                        stage = 4;
                        //life++;
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        script2.MicroclearWrite();
                        timestageMesure();

                        break;
                    case 4:
                        SceneManager.LoadScene(6, LoadSceneMode.Single);
                        PerformanceMesurement();
                        flags = 5;
                        stage = 5;
                        //life++;
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        script2.MicroclearWrite();
                        timestageMesure();

                        break;
                    case 5:
                        //ラストステージをクリアしてもスコアUP目指して残機などは一定に
                        SceneManager.LoadScene(8, LoadSceneMode.Single);
                        PerformanceMesurement();
                        flags = 1;
                        stage = 1;
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        script2.MicroclearWrite();
                        timestageMesure();
 
                        break;
                }
            }else if (baselife == 7)
                       {
                //ブロックを2倍にするプログラム，実験では使用しなかった．
                //B=2;
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 0:
                        break;
                    case 10:
                        SceneManager.LoadScene(6, LoadSceneMode.Single);
                        flags = 11;
                        stage = 1;
                        //life = life + 2;
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        timestageMesure();
                        PerformanceMesurement();
                        break;
                    case 11:
                        SceneManager.LoadScene(6, LoadSceneMode.Single);
                        flags = 12;
                        stage = 2;
                        //life = life + 2; 
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        timestageMesure();
                        PerformanceMesurement();
                        break;
                    case 12:
                        SceneManager.LoadScene(6, LoadSceneMode.Single);
                        flags = 13;
                        stage = 3;
                        //life = life + 2;
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        timestageMesure();
                        PerformanceMesurement();
                        break;
                    case 13:
                        SceneManager.LoadScene(6, LoadSceneMode.Single);
                        flags = 14;
                        stage = 4;
                        //life = life + 2;
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        timestageMesure();
                        PerformanceMesurement();
                        break;
                    case 14:
                        //ラストステージをクリアしてもスコアUP目指して残機などは一定に
                        SceneManager.LoadScene(8, LoadSceneMode.Single);
                        flags = 11;
                        stage = 0;
                        stageclearcount++;
                        //記録
                        script2.Hitcount();
                        timestageMesure();
                        PerformanceMesurement();
                        break;
                }
            }
            
        }
    }
    /// <summary>
    /// データ書き込み時ラベル作成
    /// Stageの進捗やいつやめたかを調べる
    /// </summary>
    /// 
    public void timestageMesure()
    {
        //基本的にScenescriptのstarttimeを基準とする
        //測定時間記録
        //mesuretime = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond / 1000 - starttime;
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/Progress/StageProgress" + script1.participantsnumber +
            script1.fileName + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("{0},{1}", duration,stagecount);
        sw.Flush();
        sw.Close();

        //scoreも同時に書き込んでみる
        StreamWriter sw1;
        FileInfo fi1;
        fi1 = new FileInfo(Application.dataPath + "/Progress/ScoreProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw1 = fi1.AppendText();
        sw1.WriteLine("{0},{1}",duration,score);
        sw1.Flush();
        sw1.Close();
    }

    /// <summary>
    /// データ書き込み時ラベル作成
    /// SceneScriptの最初に1回読み込む
    /// </summary>
    public void timestageID()
    {
        //Stage進捗を書き込む
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/Progress/StageProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("Time,Stage");
        sw.Flush();
        sw.Close();

        //scoreも同時に書き込んでみる
        StreamWriter sw1;
        FileInfo fi1;
        fi1 = new FileInfo(Application.dataPath + "/Progress/ScoreProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw1 = fi1.AppendText();
        sw1.WriteLine("Time,Score");
        sw1.Flush();
        sw1.Close();

        //同時書き込みは問題なかったので、ステージごと、全体の成績用ラベルも書き込む
        StreamWriter sw2;
        FileInfo fi2;
        fi2 = new FileInfo(Application.dataPath + "/Progress/PerformanceProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw2 = fi2.AppendText();
        sw2.WriteLine("{0},Stage1,Stage2,Stage3,Stage4,Stage5,AllStage",script1.fileName);
        sw2.Flush();
        sw2.Close();

        //同時書き込みは問題なかったので、ステージごと、全体のミス用ラベルも書き込む
        StreamWriter sw3;
        FileInfo fi3;
        fi3 = new FileInfo(Application.dataPath + "/Progress/MissProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw3 = fi3.AppendText();
        sw3.WriteLine("{0},Stage1,Stage2,Stage3,Stage4,Stage5,AllStage", script1.fileName);
        sw3.Flush();
        sw3.Close();

        //同時書き込みは問題なかったので、ステージごと、全体の効率用ラベルも書き込む
        StreamWriter sw4;
        FileInfo fi4;
        fi4 = new FileInfo(Application.dataPath + "/Progress/EfficiencyProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw4 = fi4.AppendText();
        sw4.WriteLine("{0},Stage1,Stage2,Stage3,Stage4,Stage5,AllStage", script1.fileName);
        sw4.Flush();
        sw4.Close();

        //ヒットミスのミクロデータ用ラベル作成
        StreamWriter sw5;
        FileInfo fi5;
        fi5 = new FileInfo(Application.dataPath + "/hitmiss/Microdata" + script1.participantsnumber + script1.fileName + ".csv");
        sw5 = fi5.AppendText();
        sw5.WriteLine("Type,Time,AllTime,Score,Block");
        sw5.Flush();
        sw5.Close();
    }

    /// <summary>
    /// パフォーマンス測定メソッド:基礎実験で使用
    /// パラメータごとの規定回数が終了したらbreakscore,hitcount,misscount,の値からパフォーマンスを出力
    /// </summary>

    public void PerformanceMesurement()
    {
        //+1は最初の一回、以降はステージクリアごとに＋1回
        //トータルの成績計算
        totalhit +=hitcount;
        totalmiss += misscount;
        totalbreakscore += breakscore;
        totalmisspercentage = totalmiss / (totalhit + 1 + totalmiss + stageclearcount);
        totalefficiency = totalbreakscore / (totalhit + 1 + totalmiss + stageclearcount);

        misspercentage = misscount / (hitcount + 1 + misscount);
        efficiency = breakscore / (hitcount + 1 + misscount);

        //ステージごとに書き込み
        thresholdstock[stage] = misspercentage / efficiency;
        missratestock[stage] = misspercentage;
        efficiencystock[stage] = efficiency;

        //全体のパフォーマンス書き込み
        thresholdstock[6] = totalmisspercentage / totalefficiency;
        missratestock[6] = totalmisspercentage;
        efficiencystock[6] = totalefficiency;
        //記録したら初期化
        breakscore = 0;
        hitcount = 0;
        misscount = 0;
    }

    public void PerformanceProgresswrite()
    {
        //パフォーマンス記録
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/Progress/PerformanceProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw = fi.AppendText();
        sw.Write("try{0},", thresholdstock[0]);
        for (int i = 1; i < 6; i++)
        {
            sw.Write("{0},", thresholdstock[i]);
        }
        sw.WriteLine(thresholdstock[6]);
        sw.Flush();
        sw.Close();

        //ミス記録
        StreamWriter sw1;
        FileInfo fi1;
        fi1 = new FileInfo(Application.dataPath + "/Progress/MissProgress" + script1.participantsnumber + script1.fileName + ".csv");
        sw1 = fi1.AppendText();
        sw1.Write("try{0},", missratestock[0]);
        for (int i = 1; i < 6; i++)
        {
            sw1.Write("{0},", missratestock[i]);
        }
        sw1.WriteLine(missratestock[6]);
        sw1.Flush();
        sw1.Close();

        //効率記録
        StreamWriter sw2;
        FileInfo fi2;
        fi2 = new FileInfo(Application.dataPath + "/Progress/EfficiencyProgress" + script1.participantsnumber+script1.fileName + ".csv");
        sw2 = fi2.AppendText();
        sw2.Write("try{0},", efficiencystock[0]);
        for (int i = 1; i < 6; i++)
        {
            sw2.Write("{0},", efficiencystock[i]);
        }
        sw2.WriteLine(efficiencystock[6]);
        sw2.Flush();
        sw2.Close();
    }
    /// <summary>
    /// パフォーマンス書き込みメソッド
    /// 基礎実験で使用していた。
    /// </summary>
    void performancewrite()
    {
        //パフォーマンス記録
        StreamWriter sw;
        FileInfo fi;
        fi = new FileInfo(Application.dataPath + "/Performance" + script1.fileName + ".csv");
        sw = fi.AppendText();
        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}", script1.csvDatas[i - 1][0],
            script1.csvDatas[i - 1][1], script1.csvDatas[i - 1][2], script1.csvDatas[i - 1][3], i, misspercentage, efficiency, misspercentage / efficiency);
        sw.Flush();
        sw.Close();
    }
    /*
    void StageChecker()
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


    void StageClear()
    {
        //以下シーン遷移条件分岐
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
                SceneManager.LoadScene(6, LoadSceneMode.Single);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
            {
                //時間記憶
                Debug.Log("breakscore" + breakscore);
                stageclearcount++;
                SceneManager.LoadScene(6, LoadSceneMode.Single);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
            {
                //時間記憶
                Debug.Log("breakscore" + breakscore);
                stageclearcount++;
                SceneManager.LoadScene(6, LoadSceneMode.Single);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
            {
                //時間記憶
                Debug.Log("breakscore" + breakscore);
                stageclearcount++;
                SceneManager.LoadScene(6, LoadSceneMode.Single);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
            {
                //時間記憶
                Debug.Log("breakscore" + breakscore);
                stageclearcount++;
                SceneManager.LoadScene(8, LoadSceneMode.Single);
            }
        }
    }
    */

}
