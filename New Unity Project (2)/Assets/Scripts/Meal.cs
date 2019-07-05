﻿using System.Collections;
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
    public int minQuant = 0;
}
