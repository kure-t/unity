using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript1 : MonoBehaviour
{
    //ライフ・スコア
    public int life = 99;
    public int score = 0;
    private float starttime,now;

    //シングルトン
    //現在存在しているオブジェクト実体の記憶領域
    static SceneScript1 _instance = null;
    //オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static SceneScript1 instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<SceneScript1>()); }
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
        starttime = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60
            + DateTime.Now.Second + DateTime.Now.Millisecond / 1000;
    }
    void Update()
    {
        now = DateTime.Now.Hour * 60 * 60 + DateTime.Now.Minute * 60
            + DateTime.Now.Second + DateTime.Now.Millisecond / 1000 - starttime;
        if (now > 180)
        {
            Application.Quit();
            Debug.Log("finished");
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (GameObject.FindGameObjectsWithTag("Block").Length == 0)
            {
                //時間記憶
                //Debug.Log("breakscore" + breakscore);
                //クリアしたらヒットをまとめて書き込む
                //script2.Hitcount();
                //PerformanceMesurement();
                //stageclearcount++;
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
        }
    }
}
