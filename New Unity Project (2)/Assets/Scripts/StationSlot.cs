using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class StationSlot : MonoBehaviour,IPointerClickHandler {
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
    public int index;
    public int count = 0;
    public Sprite placeHolder;
    bool decrease = false;
    Station station;
    void Start()
    {
        index = indexAccordingToParent();
    }
    void Update()
    {
        station = PlayerController.current.GetComponent<Station>();
        if (8 > index)
        {
            if (station.inventory[index].typeOfItem != null)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = station.inventory[index].GetSprite();
                transform.GetChild(1).GetChild(0).GetComponent<Text>().text = station.inventory[index].count.ToString();
                transform.GetChild(0).localScale = Vector3.one * 0.4f;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = placeHolder;
                transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "0";
                transform.GetChild(0).localScale = Vector3.one * 0.25f;
            }
            if (decrease)
            {
                if (station.inventory[index].count> 1)
                {
                    station.inventory[index].count--;
                }
                else
                {
                    station.inventory[index].count = 0;
                    station.inventory[index].typeOfItem = null;
                }
                decrease = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(station.inventory[index].typeOfItem != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                InventoryOfPlayer.Transaction(station.inventory[index].typeOfItem, out decrease);
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {

            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                ImageCreator.TakeIn(station.inventory[index].typeOfItem,out decrease);
            }
        }
    }

    int indexAccordingToParent()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) == transform)
            {
                return i;
            }
        }
        return 0;
    }

}
