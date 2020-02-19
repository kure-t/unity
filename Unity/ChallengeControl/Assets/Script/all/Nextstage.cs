using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextstage : MonoBehaviour {

    public GameObject obj;
    public SceneScript script;
    void Start()
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }
    public void OnClick()
    {
        switch (script.checker)
        {
            case 0:
                SceneManager.LoadScene(1, LoadSceneMode.Single);
                break;
            case 1:
                SceneManager.LoadScene(5, LoadSceneMode.Single);
                break;
            case 2:
                SceneManager.LoadScene(6, LoadSceneMode.Single);
                break;
        }
    }
}
