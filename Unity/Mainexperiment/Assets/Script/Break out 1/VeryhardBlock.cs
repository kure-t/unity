using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//使用してないが2倍の硬さのブロック
public class VeryhardBlock : MonoBehaviour {

    public GameObject obj;
    public SceneScript script;
    public Material material1,material2,material3;
    public int hp = 4;
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
        if (hp == 3)
        {
            this.GetComponent<Renderer>().material = material1;
            script.breakscore += 1;
        }
        if (hp == 2)
        {
            this.GetComponent<Renderer>().material = material2;
            script.breakscore += 1;
        }
        if (hp == 1)
        {
            this.GetComponent<Renderer>().material = material3;
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
