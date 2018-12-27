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
            perk.transform.GetChild(1).GetComponent<Text>().text = "BUY | "+
                item.GetComponent<RawMaterial>().Price.ToString() + " €";
            perk.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<RawMaterial>().image;
            perk.transform.GetChild(0).localScale /= 1.5f;
            perk.transform.GetComponent<BuyingButton>().represent = item;
            perk.transform.GetComponent<BuyingButton>().price = item.GetComponent<RawMaterial>().Price;
            perk.transform.GetChild(2).GetComponent<Text>().text = item.name.ToUpper();
        }
	}

}
