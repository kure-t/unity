using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ボールのミス判定のスクリプト
public class BottomWallScript : MonoBehaviour {

    public GameObject obj,obj1;
    public SceneScript script;
    public SceneScript1 script1;
    public Transform ball;

    // Use this for initialization
    void Start () {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }
	 
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        if (script.life > 0)
        {
            Instantiate(ball);
            script.misscount++;
            script.life--;
        }
        else if(script.life==0)
        {

            script.stagecount = 0;
            script.timestageMesure();
            script.PerformanceMesurement();
            //script.PerformanceProgresswrite();
            SceneManager.LoadScene(7, LoadSceneMode.Single);
        }
    }
}
