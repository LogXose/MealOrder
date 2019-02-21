using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour {
    [SerializeField] GameObject[] affectedKinds;
    [SerializeField] int[] effectRate;
    public Dictionary<GameObject, int> dict = new Dictionary<GameObject, int>();

    private void Awake()
    {
        for (int i = 0; i < affectedKinds.Length; i++)
        {
            dict.Add(affectedKinds[i], effectRate[i]);
        }
    }
}
