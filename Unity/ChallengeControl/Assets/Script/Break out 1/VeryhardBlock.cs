using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeryhardBlock : MonoBehaviour {

    public GameObject obj;
    public SceneScript script;
    public Material material1,material2;
    public int hp = 3;
    // Use this for initialization
    void Start()
    {
        obj = GameObject.Find("SceneScript");
        script = obj.GetComponent<SceneScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        Invoke("DelayMethod", 0.1f);
    }
    void DelayMethod()
    {
        hp--;
        if (hp == 2)
        {
            this.GetComponent<Renderer>().material = material1;
            script.breakscore += 1;
        }
        if (hp == 1)
        {
            this.GetComponent<Renderer>().material = material2;
            script.breakscore += 1;
        }
        if (hp == 0)
        {
            Destroy(gameObject);
            script.score += 10;
            script.breakscore += 1;
        }
    }
}
