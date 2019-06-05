using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criticker : MonoBehaviour {

    public string name;
    public int deviationRange;
    public int price;
    public Sprite[] icon = new Sprite[5];
    public Color[] colors = new Color[10];
    public Segmentation.CustomerSegmentation[] critickerSegmentation;
    public string[] critickerNames;
    public string[] critickerTitle;

}
