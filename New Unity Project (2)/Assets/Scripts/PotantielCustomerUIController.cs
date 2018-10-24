using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotantielCustomerUIController : MonoBehaviour {
    void Update()
    {
        transform.GetChild(1).GetComponent<Text>().text = Order.PotantielCustomerCount.ToString();
    }
}
