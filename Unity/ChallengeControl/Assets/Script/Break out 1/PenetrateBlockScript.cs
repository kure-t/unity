using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrateBlockScript : MonoBehaviour {

    public GameObject obj;
    public SceneScript script;
    
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter()
    {
        Destroy(this.gameObject);
        script.score += 10;
        script.breakscore += 1;
    }
}
