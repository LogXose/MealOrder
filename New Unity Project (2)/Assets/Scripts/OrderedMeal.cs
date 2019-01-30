using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderedMeal : MonoBehaviour {

    public Order.MealOrder MealOrder;
    public void finish()
    {
        MealOrder.completed = true;
    }
}
