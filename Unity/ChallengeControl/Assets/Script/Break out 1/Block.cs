using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour {

    public GameObject obj,obj1;
    public SceneScript script;
    public SceneScript1 script1;
	// Use this for initialization
	void Start () {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter(Collision collision){
            Invoke("DelayMethod", 0.1f);
	}    
    void DelayMethod()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(this.gameObject);
            script1.score += 10;
            Debug.Log("Delay call");
        }
        else
        {
            Destroy(this.gameObject);
            script.score += 10;
            script.breakscore += 1;
            Debug.Log("Delay call");
        }
    }
}
