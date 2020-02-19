using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomWallScript : MonoBehaviour {

    public GameObject obj;
    public SceneScript script;
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
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                //script.PerformanceMesurement();
                //script.script2.Hitcount();
                SceneManager.LoadScene(3, LoadSceneMode.Single);
            }
        }
    }
}
