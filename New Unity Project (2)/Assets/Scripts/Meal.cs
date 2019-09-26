using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meal : MonoBehaviour {
    public GameObject[] Inputs;
    public GameObject[] Station;
    public Sprite image;
    public string Definition;
    public float[] InputCount;
    public float unitTime = 0;
    public float price = 99.65f;
    public int minQuant = 1;
    public float realAveragePoint = 0;
    public Dictionary<Segmentation.CustomerSegmentation, float> points;
    public float createdTime;
    public float estimatedCost;
}
