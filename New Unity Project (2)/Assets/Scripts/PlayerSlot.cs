using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour,IPointerClickHandler,IDragHandler,IEndDragHandler {
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
    public GameObject represent;
    public int Count;
    public static Order.MealOrder mealOrder;
    Vector3 normalLocalPos;

    void Start()
    {
        normalLocalPos = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OrdersMenuController.atOrderStation && represent != null)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OrdersMenuController.atOrderStation && represent != null)
        {
            GraphicRaycaster gr = GameObject.FindGameObjectWithTag("orderMenu").
                GetComponent<GraphicRaycaster>();
            PointerEventData ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);
            if (results.Count > 0)
            {
                foreach (var item in results)
                {
                    if (item.gameObject.GetComponent<OrderedMeal>() != null)
                    {
                        PlayerSlot.mealOrder = item.gameObject.GetComponent<OrderedMeal>().MealOrder;
                    }
                }
            }
            if (represent == mealOrder.meal && Count >= mealOrder.Count && !mealOrder.completed)
            {
                mealOrder.completed = true;
                for (int i = 0; i < mealOrder.Count; i++)
                {
                    InventoryOfPlayer.BackTransaction(represent);
                }
                transform.localPosition = normalLocalPos;
            }
            else
            {
                transform.localPosition = normalLocalPos;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(represent != null && !OrdersMenuController.atOrderStation && !GameObject.FindWithTag("Player").GetComponent<PlayerController>().marketInventoryOpen)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                InventoryOfPlayer.BackTransaction(represent);
                Station.Transaction(represent);
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {

            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                bool decrease = false;
                ImageCreator.TakeIn(represent, out decrease);
                if(decrease)
                InventoryOfPlayer.BackTransaction(represent);
            }
        }

    }
}
