using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PricePanelController : MonoBehaviour {

    private void Update()
    {
        transform.GetComponent<Text>().text = InventoryOfPlayer.Money.ToString();
    }

}
