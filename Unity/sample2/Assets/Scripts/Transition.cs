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
    public int goal = 100;
    
    private float mesuretime;
    public string time;
    public string result;
    public int timememory;
    public int starttime, now;
    public float duration;
    public int stoptime;

  
    // Use this for initialization
    void Start()
    {
        starttime = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond / 1000;
        duration = 0;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        if (duration > stoptime)
        {
            //データ書き込みメソッド
            //timestageMesure();
            Application.Quit();
        }
    }
  
   public void StageClear()
    {
        if (number >= goal)
        {
            SceneManager.LoadScene("Clear", LoadSceneMode.Single);
        }
    }

 

}
    //public void timestageMesure()
    //{
    //    //基本的にScenescriptのstarttimeを基準とする
    //    //測定時間記録
    //    //mesuretime = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60 + DateTime.Now.Second + DateTime.Now.Millisecond / 1000 - starttime;
    //    StreamWriter sw;
    //    FileInfo fi;
    //    fi = new FileInfo(Application.dataPath + "/Progress/StageProgress" + script1.participantsnumber +
    //        script1.fileName + ".csv");
    //    sw = fi.AppendText();
    //    sw.WriteLine("{0},{1}", duration, stagecount);
    //    sw.Flush();
    //    sw.Close();

    //    //scoreも同時に書き込んでみる
    //    StreamWriter sw1;
    //    FileInfo fi1;
    //    fi1 = new FileInfo(Application.dataPath + "/Progress/ScoreProgress" + script1.participantsnumber + script1.fileName + ".csv");
    //    sw1 = fi1.AppendText();
    //    sw1.WriteLine("{0},{1}", duration, score);
    //    sw1.Flush();
    //    sw1.Close();
    //}


