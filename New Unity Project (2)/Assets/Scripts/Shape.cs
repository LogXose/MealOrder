using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {
    public GameObject[] affectedKinds;
    public int[] effectRate;
    public string nameGO;
    public Dictionary<GameObject, int> dict = new Dictionary<GameObject, int>();
    public Sprite icon;
    private void Awake()
    {
        for (int i = 0; i < affectedKinds.Length; i++)
        {
            dict.Add(affectedKinds[i], effectRate[i]);
        }
    }
}
