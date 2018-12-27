using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestoranList : MonoBehaviour {

    public GameObject restoranName;

    private void Start()
    {
        restoranName.GetComponent<Text>().text = "Adiyaman Cigkoftecisi";
    }
}
