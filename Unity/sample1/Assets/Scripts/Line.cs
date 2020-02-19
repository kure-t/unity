using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject newLine = new GameObject("Line");
        LineRenderer lRend = newLine.AddComponent<LineRenderer>();
        lRend.SetVertexCount(2);
        lRend.SetWidth(0.2f, 0.2f);
        Vector3 startVec = new Vector3(-1.0f, 8.5f, 0.0f);
        Vector3 endVec = new Vector3(6.0f, 8.5f, 0.0f);
        lRend.SetPosition(0, startVec);
        lRend.SetPosition(1, endVec);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
