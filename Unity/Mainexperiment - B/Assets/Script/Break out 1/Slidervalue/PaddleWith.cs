using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaddleWith : MonoBehaviour {

    public Slider playerslider;
    public float paddlewidth;

	// Use this for initialization
	void Start () {
        playerslider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });		
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void ValueChangeCheck()
    {
        Debug.Log(playerslider.value);
        paddlewidth = playerslider.value;
    }
}
