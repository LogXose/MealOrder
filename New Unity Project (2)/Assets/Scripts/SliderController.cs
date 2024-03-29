﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {

    public Slider sliderLeft;
    public Slider sliderMid;
    public Slider sliderRight;
    public Text textLeft;
    public Text textMid;
    public Text textRight;
    float right;
    float mid;
    float left;
    [SerializeField] bool total1 = false;
    [SerializeField] GameObject nextPage;

	void Start () {
        if (total1)
        {
            sliderLeft.value = 1;
            sliderMid.value = 0;
            sliderRight.value = 0;
        }
        else
        {
            sliderLeft.value = 1;
            sliderMid.value = 1;
            sliderRight.value = 1;
        }
        left = sliderLeft.value;
        mid =sliderMid.value;
        right = sliderRight.value;
	}
	
	void Update () {
        if (total1)
        {
            float surplus = 0;
            if (left != sliderLeft.value)
            {
                surplus += sliderLeft.value - left;
                ExecuteChange(surplus, sliderMid, sliderRight, sliderLeft);
            }
            else if (right != sliderRight.value)
            {
                surplus += sliderRight.value - right;
                ExecuteChange(surplus, sliderMid, sliderLeft, sliderRight);
            }
            else if (mid != sliderMid.value)
            {
                surplus += sliderMid.value - mid;
                ExecuteChange(surplus, sliderLeft, sliderRight, sliderMid);
            }
            left = sliderLeft.value;
            mid = sliderMid.value;
            right = sliderRight.value;
            textMid.text = "%" + (mid * 100).ToString("00.0");
            textLeft.text = "%" + (left * 100).ToString("00.0");
            textRight.text = "%" + (right * 100).ToString("00.0");
        }
        else
        {
            textMid.text = "%" + (sliderMid.value * 100).ToString("00.0");
            textLeft.text = "%" + (sliderLeft.value * 100).ToString("00.0");
            textRight.text = "%" + (sliderRight.value * 100).ToString("00.0");
        }
        Debug.Log("quant :" + sliderLeft.value);
    }
    void ExecuteChange(float sur,Slider one,Slider two,Slider main)
    {
        if(sur > 0)
        {
            if (one.value > 0 && two.value > 0)
            {
                float half = sur / 2.0000000f;
                one.value -= half;
                two.value = 1.00000000f - main.value-one.value;
            }
            else if (one.value > 0 && two.value == 0)
            {
                one.value -= sur;
                main.value = 1.000000f - one.value - two.value;
            }
            else if (one.value == 0 && two.value > 0)
            {
                two.value -= sur;
                main.value = 1.000000f - one.value - two.value;
            }
        }
        else
        {
            if (one.value != 1.0000f && two.value != 1.00000f)
            {
                float half = sur / 2.0000000f;
                one.value -= half;
                two.value = 1.00000000f - main.value - one.value;
            }
            else if (two.value == 1.0000f)
            {
                one.value -= sur;
                main.value = 1.000000f - one.value - two.value;
            }
            else if (one.value == 1.0000f)
            {
                two.value -= sur;
                main.value = 1.000000f - one.value - two.value;
            }
        }
    }
    public void SaveSegments()
    {
        PastaFeatures.quantative = sliderLeft.value*100;
        PastaFeatures.effort = sliderMid.value*100;
        PastaFeatures.quality = sliderRight.value*100;
    }
    
}
