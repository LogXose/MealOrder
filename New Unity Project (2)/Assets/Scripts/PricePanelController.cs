using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PricePanelController : MonoBehaviour {
    [SerializeField] GameObject logoContent;
    [SerializeField] Text RestaurantName;
    private void Start()
    {
        Instantiate(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().getLogo(), logoContent.transform);
        RestaurantName.text = PlayerController.RestaurantName;
    }
    private void Update()
    {
        Transform pointLabel = transform.parent.parent.parent.GetChild(3).GetChild(0).GetChild(0);
        transform.GetComponent<Text>().text = InventoryOfPlayer.Money.ToString();
        if (MealList.points.Count > 0)
        {
            pointLabel.GetComponent<Text>().text = MealList.averagePoint.ToString("0.00");
        }
        else pointLabel.GetComponent<Text>().text = "-";
    }

}
