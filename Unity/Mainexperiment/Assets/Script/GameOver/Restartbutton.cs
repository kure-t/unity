using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 再開ボタンの機能実装
/// </summary>
public class Restartbutton : MonoBehaviour {
    public GameObject obj,obj1;
    public SceneScript script;
    public SceneScript1 script1;

    void Start()
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }
    public void OnClick()
    {
        Debug.Log("Restartbutton click!");
        script.PerformanceProgresswrite();
        //スコアとライフを回復
        script.score = 0;
        script.life = script.baselife;
        //コンティニュー回数を記録
        script.count++;
        script.missratestock[0] = script.count;
        script.thresholdstock[0] = script.count;
        script.efficiencystock[0] = script.count;
        for (int i= 1;i < 7; i++){
            script.missratestock[i] = 0;
            script.thresholdstock[i] = 0;
            script.efficiencystock[i] = 0;
        }
        //シーン遷移
        script.stage = 1;
        script.stagecount = 1;
        script.timestageMesure();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
