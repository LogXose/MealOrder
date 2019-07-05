using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealMaterial : MonoBehaviour {
    public GameObject[] Inputs;
    public Station.StationType stationType;
    public Sprite image;
    public string Definition;
    public float[] InputCount;
    public float unitTime;
    public int minQuant = 0;
    public bool countable = false;
}
