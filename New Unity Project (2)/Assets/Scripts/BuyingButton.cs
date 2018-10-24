using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BuyingButton : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
    public GameObject represent;
    public int price = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(InventoryOfPlayer.Money >= price)
            {
                bool execution = false;
                InventoryOfPlayer.Transaction(represent,out execution);
                if (execution)
                {
                    InventoryOfPlayer.Money -= price;
                    PricePanelController.DecreaseAnimation(price);
                }
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {

        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {

        }
    }
}
