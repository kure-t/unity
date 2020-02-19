using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
///ステージ遷移ボタンの機能
/// </summary>
public class Nextstage : MonoBehaviour {

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
        script.stagecount++;
        script.timestageMesure();
        if (SceneManager.GetActiveScene().buildIndex == 8)
        {
            script.PerformanceProgresswrite();
            for (int i = 1; i < 6; i++)//ステージクリアしたら各成績を書き込む
            {
                script.missratestock[i] = 0;
                script.thresholdstock[i] = 0;
                script.efficiencystock[i] = 0;
            }
        }
        SceneManager.LoadScene(script.flags, LoadSceneMode.Single);
    }
}
