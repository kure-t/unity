using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSlider: MonoBehaviour {

    public Slider randomslider;
    public float randomlevel;

	// Use this for initialization
	void Start () {
        //slider = GetComponent<Slider>();
        randomslider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}
    public void ValueChangeCheck()
    {
        Debug.Log(randomslider.value);
        randomlevel = randomslider.value;
    }
}
