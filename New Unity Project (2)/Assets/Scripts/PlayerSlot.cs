using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerSlot : MonoBehaviour,IPointerClickHandler {
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
    public GameObject represent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(represent != null)
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
