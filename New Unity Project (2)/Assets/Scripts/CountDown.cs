using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
    public float Timer = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer -= Time.deltaTime;
        int minute = Mathf.FloorToInt(Timer / 60);
        int second =(int) Timer % 60;
        string rest = minute.ToString("00") + ":" + second.ToString("00");
        transform.GetChild(0).GetComponent<Text>().text = rest;
	}
}
