using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 各スコア・ライフ・ステージの表示
/// </summary>
public class ParamScript : MonoBehaviour {
    public GUIStyle style,style1;
    public GameObject obj,obj1;
    public SceneScript script;
    public SceneScript1 script1;
	// Use this for initialization
    void OnGUI()
    {

        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
        GUI.Label(new Rect(10, 10, 200, 40), "Score", style);
        GUI.Label(new Rect(10, 50, 200, 40), "" + script.score, style);
        GUI.Label(new Rect(10, 120, 200, 40), "Life", style);
        GUI.Label(new Rect(10, 160, 200, 40), "" + script.life, style);
        GUI.Label(new Rect(160, 10, 200, 40), "Stage" + (script.stage)+"/5", style1);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //パラメータ設定画面では何もしない，=!でよかったかも
        }
        else
        {
            /*基礎実験用
            obj = GameObject.Find("SceneScript");
            script = obj.GetComponent<SceneScript>();
            GUI.Label(new Rect(10, 10, 200, 40), "点数", style);
            GUI.Label(new Rect(10, 50, 200, 40), "" + script.score, style);
            GUI.Label(new Rect(10, 120, 200, 40), "残機数", style);
            GUI.Label(new Rect(10, 160, 200, 40), "" + script.life, style);
            GUI.Label(new Rect(160, 10, 200, 40), "stage" + (script.i+1),style1);
            */
        }
    }
}
