using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 通常のブロック
/// </summary>
public class Block : MonoBehaviour {

    public GameObject obj,obj1;
    public SceneScript script;
    public SceneScript1 script1;
	// Use this for initialization
	void Start () {
        //基礎実験用
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
        
        //練習用
        /*
        obj1 = GameObject.Find("SceneScript");
        script1 = obj1.GetComponent<SceneScript1>();
        */
    }
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter(Collision collision){
        Invoke("DelayMethod", 0.1f);
        script.score += 10;
        script.breakscore += 1;
    }    
    void DelayMethod()
    {
        Destroy(this.gameObject);
        Debug.Log("Delay call");
    }
}
