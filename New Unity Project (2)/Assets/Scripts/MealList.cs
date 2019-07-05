using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealList : MonoBehaviour{
    [SerializeField] GameObject _cardPrefab;
    [SerializeField] GameObject content;
    public static List<GameObject> producedMeals = new List<GameObject>();

    private void Start()
    {
        foreach (var item in producedMeals)
        {
            GameObject card = Instantiate(_cardPrefab, content.transform);
            Meal meal = item.GetComponent<Meal>();
            card.transform.GetChild(1).GetComponent<Text>().text = item.name;
            card.transform.GetChild(2).GetComponent<Image>().sprite = meal.image;
            card.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = meal.price.ToString(".00");
            card.transform.GetChild(4).GetComponent<Text>().text = meal.Definition;
        }
    }
}
