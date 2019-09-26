using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealList : MonoBehaviour{
    [SerializeField] GameObject _cardPrefab;
    [SerializeField] GameObject content;
    [SerializeField] GameObject pointsContent;
    [SerializeField] GameObject commentCard;
    [SerializeField] GameObject Name;
    [SerializeField] GameObject iconContent;
    [SerializeField] GameObject pointText;
    [SerializeField] GameObject[] logos;
    public static List<GameObject> producedMeals = new List<GameObject>();
    public static List<Order.CommentStruct> points = new List<Order.CommentStruct>();
    public static float averagePoint = 0;
    [SerializeField] GameObject sliderLabel;
    public int secs = 0;
    public float power = 2;
    public float cost = 0;
    [SerializeField] GameObject costLabel;
    [SerializeField] GameObject AdsButton;
    [SerializeField] GameObject moneyLabel;
    private void Start()
    {
        stateOfAdsButton();
        changeValueOfSliderLabel(2);
        secs = 0;
        power = 2;
        averagePoint = 0;
        foreach (var item in producedMeals)
        {
            GameObject card = Instantiate(_cardPrefab, content.transform);
            Meal meal = item.GetComponent<Meal>();
            card.transform.GetChild(1).GetComponent<Text>().text = item.name;
            card.transform.GetChild(2).GetComponent<Image>().sprite = meal.image;
            card.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "$ " + meal.price.ToString("0.00");
            card.transform.GetChild(4).GetComponent<Text>().text = meal.Definition;
        }
        foreach (var item in points)
        {
            GameObject cCard = Instantiate(commentCard, pointsContent.transform);
            cCard.transform.GetChild(1).GetComponent<Text>().text = item.name;
            cCard.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = item.point.ToString("0.00");
            cCard.transform.GetChild(3).GetComponent<Text>().text = item.comment;
            averagePoint += item.point;
        }
        averagePoint /= points.Count;
        Name.GetComponent<Text>().text = PlayerController.RestaurantName;
        foreach (var item in logos)
        {
            if(item.name == PlayerController.RestaurantLogo)
            {
                Instantiate(item, iconContent.transform);
                break;
            }
        }
        
        pointText.GetComponent<Text>().text = points.Count > 0? averagePoint.ToString("0.00") : "-";
    }  
    
    public void StartAdvertisement()
    {
        if(secs > 0 && cost <= InventoryOfPlayer.Money)
        {
            Order.advertiseStartTime = Order.zaman;
            Order.advertiseTime = secs;
            Order.advertisementsOn = true;
            Order.adsPower = power;
            InventoryOfPlayer.Money -= cost;
            stateOfAdsButton();
        } 
    }

    public void changeValueOfSliderLabel(float a)
    {
        sliderLabel.GetComponent<Text>().text = a.ToString("0.00");
        power = a;
        changeCost();
    }

    public void changeTimerValue(string a)
    {
        secs = int.Parse(a);
        changeCost();
    }

    void changeCost()
    {
        float a = 0.1f * secs * power;
        costLabel.GetComponent<Text>().text = a.ToString("0.00");
        cost = a;
    }

    void stateOfAdsButton()
    {
        if (Order.advertisementsOn)
        {
            AdsButton.GetComponent<BasicButton>().enabled = false;
            Color color = AdsButton.GetComponent<Image>().color;
            color.a = 0.5f;
            AdsButton.GetComponent<Image>().color = color;
        }
        else
        {
            AdsButton.GetComponent<BasicButton>().enabled = true;
            AdsButton.GetComponent<Image>().color = Color.white;
        }
    }

    public void SetMoneyQuantaty()
    {
        moneyLabel.GetComponent<Text>().text = InventoryOfPlayer.Money.ToString("0.00");
    }
}
