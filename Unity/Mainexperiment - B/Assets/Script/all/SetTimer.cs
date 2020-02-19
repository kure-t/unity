using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//予備実験で使用，画面に時間を表示するのを試すのに使用した
public class SetTimer : MonoBehaviour {

    private float second;
	void Start () {
        second = 0;
	}

	void Update () {
        if (Time.timeScale > 0)
        {
            second += Time.deltaTime;
            GetComponent<Text>().text = second.ToString("F2");//小数2桁にして表示
        }		
	}
}
