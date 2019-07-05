using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterial : MonoBehaviour {
    public float Price;
    public enum unit
    {
        kilogram,
        litre,
        piece
    }
    public unit Unit;
    public Sprite image;
    public bool countable = true;
    public int minQuant = 50;
}
