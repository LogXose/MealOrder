using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterial : MonoBehaviour {
    public int Price;
    public enum unit
    {
        kilogram,
        litre,
        piece
    }
    public unit Unit;
    public Sprite image;
}
