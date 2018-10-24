using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PricePanelController : MonoBehaviour {
    public static GameObject decrease;
    public static GameObject increase;
    private void Awake()
    {
        decrease = transform.GetChild(3).gameObject;
        increase = transform.GetChild(2).gameObject;
    }
    private void Update()
    {
        transform.GetChild(1).GetComponent<Text>().text = InventoryOfPlayer.Money.ToString();
    }
    public static void DecreaseAnimation(int value)
    {
        decrease.GetComponent<Animator>().SetTrigger("Work");
        decrease.GetComponent<Text>().text = "-" + value.ToString();
    }
    public static void IncreaseAnimation(int value)
    {
        increase.GetComponent<Animator>().SetTrigger("Work");
        increase.GetComponent<Text>().text = "+" + value.ToString();
    }
}
