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
    public Sprite placeHolder;//arti iconu koy buraya
    bool decrease = false;
    Station station;
    public AudioClip full;
    public AudioClip empty;
    public AudioClip close;
    Color blue;

    enum State
    {
        full,empty,close
    }
    State state = State.close;
    void Start()
    {
        index = indexAccordingToParent();
        blue = transform.GetChild(1).GetComponent<Image>().color;
    }

    void Update()
    {
        station = PlayerController.current.GetComponent<Station>();
        if (8 > index)
        {
            if (station.inventory[index].typeOfItem != null && station.inventory[index].count > 0)
            {
                /*transform.GetChild(0).GetComponent<Image>().sprite = station.inventory[index].GetSprite();
                transform.GetChild(1).GetChild(0).GetComponent<Text>().text = station.inventory[index].count.ToString();
                transform.GetChild(0).localScale = Vector3.one * 0.4f;*/
                state = State.full;
            }
            else
            {
                /*transform.GetChild(0).GetComponent<Image>().sprite = placeHolder;
                transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "0";
                transform.GetChild(0).localScale = Vector3.one * 0.25f;*/
                state = State.empty;
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
                    state = State.empty;
                }
                decrease = false;
            }
        }
        SwitchState();
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

    void SwitchState()
    {
        transform.localScale = Vector3.one;
        switch (state)
        {
            case State.full:
                this.GetComponent<AudioSource>().clip = full;
                this.GetComponent<BasicButton>().close = false;
                transform.GetChild(1).GetComponent<Image>().sprite = station.inventory[index].GetSprite();
                transform.GetChild(2).GetComponent<Text>().text = station.inventory[index].count.ToString();
                transform.GetChild(3).GetComponent<Text>().text = station.inventory[index].typeOfItem.name;
                transform.GetChild(1).GetComponent<Image>().color = Color.white;
                transform.GetChild(0).GetComponent<Image>().color = Color.white;
                break;
            case State.empty:
                this.GetComponent<AudioSource>().clip = empty;
                this.GetComponent<BasicButton>().close = false;
                transform.GetChild(1).GetComponent<Image>().sprite = placeHolder;
                transform.GetChild(1).GetComponent<Image>().color = blue;
                transform.GetChild(0).GetComponent<Image>().color = Color.white;
                transform.GetChild(2).GetComponent<Text>().text = "0";
                transform.GetChild(3).GetComponent<Text>().text = "Empty";
                break;
            case State.close:
                this.GetComponent<AudioSource>().clip = close;
                this.GetComponent<BasicButton>().close = true;
                transform.GetChild(1).GetComponent<Image>().sprite = placeHolder;
                transform.GetChild(1).GetComponent<Image>().color = blue - new Color(0, 0, 0, 0.5f);
                transform.GetChild(0).GetComponent<Image>().color = Color.white - new Color(0, 0, 0, 0.5f);
                transform.GetChild(2).GetComponent<Text>().text = "";
                transform.GetChild(3).GetComponent<Text>().text = "Closed";
                break;
            default:
                break;
        }
    }
}
