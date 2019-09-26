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
    public static GameObject playerInventory;
    public int Count;
    public static Order.MealOrder mealOrder;
    Vector3 normalLocalPos;
    public GameObject adjuster;
    int sendQuantaty = 0;
    public static List<GameObject> adjusters = new List<GameObject>();

    void Start()
    {
        normalLocalPos = transform.localPosition;
        if(adjusters.Contains(adjuster))
        adjusters.Add(adjuster);
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
            GraphicRaycaster gr = playerInventory.GetComponent<GraphicRaycaster>();
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
                    InventoryOfPlayer.BackTransaction(represent,1);
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
            if (eventData.button == PointerEventData.InputButton.Left && !adjuster.activeInHierarchy)
            {
                adjuster.SetActive(true);
                if (represent.GetComponent<Meal>())
                {
                    Meal meal = represent.GetComponent<Meal>();
                    Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                    Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                    slider.wholeNumbers = true;
                    slider.maxValue = Mathf.FloorToInt(Count / meal.minQuant);
                    slider.value = 1;
                    textComp.text = meal.minQuant.ToString();
                    sendQuantaty = meal.minQuant;
                }
                else if (represent.GetComponent<MealMaterial>())
                {
                    MealMaterial mealMaterial = represent.GetComponent<MealMaterial>();
                    if (mealMaterial.countable)
                    {
                        Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                        Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                        slider.wholeNumbers = true;
                        slider.maxValue = Count;
                        slider.value = 1;
                        textComp.text = 1.ToString();
                        sendQuantaty = 1;
                    }else if (!mealMaterial.countable)
                    {
                        MealMaterial meal = represent.GetComponent<MealMaterial>();
                        Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                        Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                        slider.wholeNumbers = true;
                        slider.maxValue = Mathf.FloorToInt(Count / meal.minQuant);
                        slider.value = 1;
                        textComp.text = meal.minQuant.ToString();
                        sendQuantaty = meal.minQuant;
                    }
                }
                else if (represent.GetComponent<RawMaterial>())
                {
                    RawMaterial raw = represent.GetComponent<RawMaterial>();
                    if (raw.countable)
                    {
                        Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                        Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                        slider.wholeNumbers = true;
                        slider.maxValue = Count;
                        slider.value = 1;
                        textComp.text = 1.ToString();
                        sendQuantaty = 1;
                    }else if (!raw.countable)
                    {
                        RawMaterial meal = represent.GetComponent<RawMaterial>();
                        Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                        Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                        slider.wholeNumbers = true;
                        slider.maxValue = Mathf.FloorToInt(Count / meal.minQuant);
                        slider.value = 1;
                        textComp.text = meal.minQuant.ToString();
                        sendQuantaty = meal.minQuant;
                    }
                }
                foreach (var item in InventoryOfPlayer.slots)
                {
                    if(item.slotui != gameObject)
                    item.slotui.GetComponent<PlayerSlot>().adjuster.SetActive(false);
                }
            }else if(eventData.button == PointerEventData.InputButton.Left && adjuster.activeInHierarchy)
            {
                adjuster.SetActive(false);
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {

            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
               /* bool decrease = false;
                ImageCreator.TakeIn(represent, out decrease);
                if(decrease)
                InventoryOfPlayer.BackTransaction(represent,1);*/
            }
        }

    }

    public void Transfer()
    {
        bool execution = false;
        InventoryOfPlayer.BackTransaction(represent, sendQuantaty,out execution);
        adjuster.SetActive(false);
        PlayerController.kralinYarra = true;        
        if(execution)
        Station.Transaction(represent, sendQuantaty);
    }

    private void Update()
    {
        if (represent == null) adjuster.SetActive(false);
        if (adjuster.activeInHierarchy)
        {
            if (represent.GetComponent<Meal>())
            {
                Meal meal = represent.GetComponent<Meal>();
                Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                sendQuantaty = (int)slider.value * meal.minQuant;
                textComp.text = sendQuantaty.ToString();
            }
            else if (represent.GetComponent<MealMaterial>())
            {
                MealMaterial mealMaterial = represent.GetComponent<MealMaterial>();
                if (mealMaterial.countable)
                {
                    Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                    Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                    sendQuantaty = (int)slider.value;
                    textComp.text = sendQuantaty.ToString();
                }
                else if (!mealMaterial.countable)
                {
                    Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                    Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                    sendQuantaty = (int)slider.value * mealMaterial.minQuant;
                    textComp.text = sendQuantaty.ToString();
                }
            }
            else if (represent.GetComponent<RawMaterial>())
            {
                RawMaterial raw = represent.GetComponent<RawMaterial>();
                if (raw.countable)
                {
                    Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                    Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                    sendQuantaty = (int)slider.value;
                    textComp.text = sendQuantaty.ToString();
                }
                else if (!raw.countable)
                {
                    Slider slider = adjuster.transform.GetChild(4).GetComponent<Slider>();
                    Text textComp = adjuster.transform.GetChild(3).GetChild(0).GetComponent<Text>();
                    sendQuantaty = (int)slider.value * raw.minQuant;
                    textComp.text = sendQuantaty.ToString();
                }
            }
        }
    }
}
