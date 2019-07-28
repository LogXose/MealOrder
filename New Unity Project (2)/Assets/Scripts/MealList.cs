using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealList : MonoBehaviour{
    [SerializeField] GameObject _cardPrefab;
    [SerializeField] GameObject content;
    [SerializeField] GameObject pointsContent;
    [SerializeField] GameObject commentCard;
    public static List<GameObject> producedMeals = new List<GameObject>();
    public static List<Order.CommentStruct> points = new List<Order.CommentStruct>();
    public static float averagePoint = 0;
    private void Start()
    {
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
            commentCard.transform.GetChild(1).GetComponent<Text>().text = item.name;
            commentCard.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = item.point.ToString("0.00");
            commentCard.transform.GetChild(3).GetComponent<Text>().text = item.comment;
            averagePoint += item.point;
        }
        averagePoint /= points.Count;
    }   
}
