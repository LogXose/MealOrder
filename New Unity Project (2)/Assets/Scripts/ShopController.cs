using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {
    public GameObject[] items;
    public GameObject template;
	void Awake () {
        foreach (GameObject item in items)
        {
            GameObject perk = Instantiate(template,transform);
            perk.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = item.GetComponent<RawMaterial>().Price.ToString();
            perk.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<RawMaterial>().image;
            perk.transform.GetChild(1).GetComponent<BuyingButton>().represent = item;
        }
	}

}
