using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine;


//ゲームオーバー時に使用
public class GameOverScript : MonoBehaviour {

    public GameObject obj;
    public SceneScript script;
    //gameover
    public GUIStyle style1;
    //ranking
    public GUIStyle style2;
    public GUIStyle style3;
    int j,k;
    void Start () {
        //シーンスクリプト読み込み
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
        ranking();
    }
	
	// Update is called once per frame
	void Update () {

		
	}
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Game Over !!", style1);
        
        //ランキングの表示
        GUI.Label(new Rect(160, 120, 300, 50), "Top Lists", style2);

        GUI.Label(new Rect(250, 200, 100, 50), "1 " + script.ranking[0, 0], style3);
        GUI.Label(new Rect(250, 275, 100, 50), "2 " + script.ranking[1, 0], style3);
        GUI.Label(new Rect(250, 350, 100, 50), "3 " + script.ranking[2, 0], style3);
        GUI.Label(new Rect(250, 425, 100, 50), "4 " + script.ranking[3, 0], style3);
        GUI.Label(new Rect(250, 500, 100, 50), "5 " + script.ranking[4, 0], style3);

        GUI.Label(new Rect(600, 200, 100, 50), script.ranking[0, 1], style3);
        GUI.Label(new Rect(600, 275, 100, 50), script.ranking[1, 1], style3);
        GUI.Label(new Rect(600, 350, 100, 50), script.ranking[2, 1], style3);
        GUI.Label(new Rect(600, 425, 100, 50), script.ranking[3, 1], style3);
        GUI.Label(new Rect(600, 500, 100, 50), script.ranking[4, 1], style3);

    }
    //スコアがランキングを越えていた時に表示するスクリプト
    void ranking()
    {
        k = 5;
        for (int i = 0; i < 5; i++)
        {
            if (Int32.TryParse(script.ranking[i, 1], out j))
            {
                Debug.Log(j);
            }
            else
            {
                Debug.Log("String could not be parsed");
            }
            if (script.score > j)
            {
                k = i;
                break;
            }
        }
        if (k < 5)
        {
            for (int l = 4; l > k; l--)
            {
                script.ranking[l, 1] = script.ranking[l - 1, 1];
                script.ranking[l, 0] = script.ranking[l - 1, 0];
            }
            script.ranking[k, 1] = script.score.ToString();
            script.ranking[k, 0] = "YOU";
        }
    }
}
