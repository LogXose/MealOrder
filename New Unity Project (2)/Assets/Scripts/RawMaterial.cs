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
    public float qualityIndex;
    public enum ReqForPasta
    {
        none,
        low,
        mid,
        high,
        veryHigh
    }
    public ReqForPasta req = ReqForPasta.low;
}
