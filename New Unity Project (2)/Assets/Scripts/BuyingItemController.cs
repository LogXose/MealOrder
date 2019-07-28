using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingItemController : MonoBehaviour {

    public GameObject prefab;

	public void selectMachine()
    {
        BuyingPanelController.machineSelected = true;
        BuyingPanelController.selected = prefab;
    }
}
