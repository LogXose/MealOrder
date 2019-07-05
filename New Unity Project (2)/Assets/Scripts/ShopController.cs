using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {
    public static List<GameObject> items = new List<GameObject>();
    public GameObject template;
	void Awake () {
        foreach (GameObject item in items)
        {
            GameObject perk = Instantiate(template,transform);
            perk.transform.GetChild(2).GetComponent<Text>().text = item.name;
            perk.transform.GetChild(3).GetComponent<Image>().sprite = item.GetComponent<RawMaterial>().image;
            perk.transform.GetChild(4).GetComponent<BuyingButton>().represent = item;
            perk.transform.GetChild(4).GetComponent<BuyingButton>().price = item.GetComponent<RawMaterial>().Price;
            perk.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = "$"+item.GetComponent<RawMaterial>().Price.ToString();
        }
	}

    private void Update()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject item = transform.GetChild(i).gameObject;
            Slider slider = item.transform.GetChild(6).GetComponent<Slider>();
            Text text = item.transform.GetChild(5).GetChild(0).GetComponent<Text>();
            RawMaterial rawMaterial = item.transform.GetChild(4).GetComponent<BuyingButton>().represent.GetComponent<RawMaterial>();
            Text priceOfTotal = item.transform.GetChild(4).GetChild(1).GetComponent<Text>();
            BuyingButton buyingButton = item.transform.GetChild(4).GetComponent<BuyingButton>();
            if(InventoryOfPlayer.Money / (rawMaterial.Price * rawMaterial.minQuant) < (InventoryOfPlayer.tkg - InventoryOfPlayer.tkgCur)/rawMaterial.minQuant)
            {
                slider.maxValue = Mathf.Floor(InventoryOfPlayer.Money / (rawMaterial.Price * rawMaterial.minQuant));
            }
            else
            {
                slider.maxValue = (InventoryOfPlayer.tkg - InventoryOfPlayer.tkgCur)/rawMaterial.minQuant;
            }
            int textValue = (int)slider.value * rawMaterial.minQuant;
            text.text = textValue.ToString();
            float totalValue = textValue * rawMaterial.Price;
            priceOfTotal.text = totalValue.ToString(".00");
            buyingButton.price = totalValue;
            buyingButton.quantaty = textValue;
        }


    }

}
