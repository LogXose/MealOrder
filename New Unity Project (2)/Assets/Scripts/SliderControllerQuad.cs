using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControllerQuad : MonoBehaviour {
    public Slider sliderLeft;
    public Slider sliderMid;
    public Slider sliderMid2;
    public Slider sliderRight;
    public Text textLeft;
    public Text textMid;
    public Text textMid2;
    public Text textRight;
    [SerializeField] GameObject nextPage;
    float right;
    float mid;
    float mid2;
    float left;
    Slider[] sliders;
    float[] floats;
    void Start()
    {
        sliderLeft.value = 1;
        sliderMid.value = 0;
        sliderMid2.value = 0;
        sliderRight.value = 0;
        left = sliderLeft.value;
        mid = sliderMid.value;
        mid2 = sliderMid2.value;
        right = sliderRight.value;
        sliders = new Slider[] { sliderLeft, sliderMid, sliderMid2, sliderRight };
        floats = new float[] { right, left, mid, mid2 };
    }
    void Update()
    {
        float surplus = 0;
        if (left != sliderLeft.value)
        {
            surplus += sliderLeft.value - left;
            ExecuteChange(surplus, sliderMid, sliderRight,sliderMid2, sliderLeft);
        }
        else if (right != sliderRight.value)
        {
            surplus += sliderRight.value - right;
            ExecuteChange(surplus, sliderMid, sliderLeft,sliderMid2, sliderRight);
        }
        else if (mid != sliderMid.value)
        {
            surplus += sliderMid.value - mid;
            ExecuteChange(surplus, sliderLeft, sliderRight,sliderMid2, sliderMid);
        }
        else if (mid2 != sliderMid2.value)
        {
            surplus += sliderMid2.value - mid2;
            ExecuteChange(surplus, sliderLeft, sliderRight, sliderMid, sliderMid2);
        }
        left = sliderLeft.value;
        mid = sliderMid.value;
        mid2 = sliderMid2.value;
        right = sliderRight.value;
        textMid.text = "%" + (mid * 100).ToString("00.0");
        textMid2.text = "%" + (mid2 * 100).ToString("00.0");
        textLeft.text = "%" + (left * 100).ToString("00.0");
        textRight.text = "%" + (right * 100).ToString("00.0");
    }
    void ExecuteChange(float sur, Slider one, Slider two,Slider three, Slider main)
    {
        if (sur > 0)
        {
            if (one.value > 0 && two.value > 0 && three.value > 0)
            {
                float half = sur / 3.0000000f;
                one.value -= half;
                three.value -= half;
                two.value = 1.00000000f - main.value - one.value - three.value;
            }
            else if (one.value > 0 && two.value == 0 && three.value > 0)
            {
                one.value -= sur/2;
                three.value = 1.000000f - one.value - two.value- main.value;
            }
            else if (one.value == 0 && two.value > 0 && three.value >0)
            {
                two.value -= sur/2;
                three.value = 1.000000f - one.value - two.value- main.value;
            }
            else if (one.value > 0 && two.value > 0 && three.value == 0)
            {
                two.value -= sur / 2;
                one.value = 1.000000f - three.value - two.value - main.value;
            }
            else if (one.value == 0 && two.value == 0 && three.value > 0)
            {
                three.value -= sur;
                main.value = 1.000000f - one.value - two.value - three.value;
            }
            else if (one.value > 0 && two.value == 0 && three.value == 0)
            {
                one.value -= sur;
                main.value = 1.000000f - one.value - two.value - three.value;
            }
            else if (one.value == 0 && two.value > 0 && three.value == 0)
            {
                two.value -= sur;
                main.value = 1.000000f - one.value - two.value - three.value;
            }
        }
        else
        {
            if (one.value != 1.0000f && two.value != 1.00000f && three.value != 1.00000f)
            {
                float half = sur / 3.0000000f;
                one.value -= half;
                three.value -= half;
                two.value = 1.00000000f - main.value - one.value - three.value;
            }
            else if (two.value == 1.0000f)
            {
                one.value -= sur/2;
                three.value -= sur / 2;
                main.value = 1.000000f - one.value - two.value- three.value;
            }
            else if (one.value == 1.0000f)
            {
                two.value -= sur / 2;
                three.value -= sur / 2;
                main.value = 1.000000f - one.value - two.value - three.value;
            }
            else if(three.value == 1.000f)
            {
                one.value -= sur / 2;
                two.value -= sur / 2;
                main.value = 1.000000f - one.value - two.value - three.value;
            }
        }

    }
    public void SaveTextures()
    {
        PastaFeatures.smooth = left*100;
        PastaFeatures.crunchs = mid*100;
        PastaFeatures.juicy = mid2*100;
        PastaFeatures.sticky = right*100;
        nextPage.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
    public void SaveProfiles()
    {
        PastaFeatures.sweet = left*100;
        PastaFeatures.sour = mid*100;
        PastaFeatures.bitter = mid2*100;
        PastaFeatures.salty = right*100;
        nextPage.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
