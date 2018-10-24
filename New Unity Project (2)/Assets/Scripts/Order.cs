using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour {
    public static Order orderInstance;
    public static int PotantielCustomerCount = 50;
    public static float OnlineCustomerChance = 0.09f;
    public static float hungryCustomerChance = 0.05f;
    public static float influenceOfYou = 0.25f;
    public static float dailyChance = 3;
    public static float chanceRange = 10;
    public static int failSum = 0;
    public static float cooldownSave = 0;
    public GameObject orderPrefab;
    public GameObject[] availableMeals;
    public static GameObject increasePotantielCustomer;
    public static GameObject decreasePotantielCustomer;
    public static GameObject orderAlarm;
    public GameObject _orderAlarm;
    public class MealOrder
    {
        public GameObject uiPrefab;
        public GameObject ui;
        public GameObject meal;
        public Transform Transform;
        public Sprite sprite;
        public string name;
        public string adress;
        public float price;
        public float totalTime;
        public float remainingTime;
        public float spendedTime = 0;
        public int Count = 0;
        public bool started = true;
        public bool paused = false;
        public bool completed = false;
        private bool finisher = false;
        private bool cancelOrder = false;
        public void Update()
        {
            if(!paused && started) spendedTime += Time.deltaTime;
            remainingTime = totalTime - spendedTime;
            int sec = (int)remainingTime % 60;
            int min = (int)remainingTime / 60;
            string time = min.ToString("0") + ":" + sec.ToString("00");
            ui.transform.GetChild(1).GetComponent<Text>().text = time;
            if (completed)
            {
                ui.transform.GetChild(2).gameObject.SetActive(true);
                paused = true;
                Finisher(false);
            }
            if(remainingTime < 0)
            {
                paused = true;
                Finisher(true);
            }
        }
        public void Initialize()
        {
            if (meal != null)
            {
                name = meal.name;
                sprite = meal.GetComponent<Meal>().image;
            }
            ui = Instantiate(uiPrefab, Transform);
            ui.GetComponent<OrderedMeal>().MealOrder = this; 
            ui.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = sprite;
            ui.transform.GetChild(0).GetChild(0).localScale = Vector3.one * 0.4f;
            ui.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Count.ToString();
        }
        private void Finisher(bool negative)
        {
            if (!finisher)
            {
                if (negative)
                {
                    finisher = true;
                    DecreasePotCustomer();
                }
                else
                {
                    finisher = true;
                    IncreasePotCustomer();
                }
            }
        }
    }
    public static void CreateOrder(GameObject ui,Transform transform,int countative = 1,
        GameObject _meal = null, float price = 0, string adress = "ananin amina", float time = 50)
    {
        MealOrder mealOrder = new MealOrder()
        {
            meal = _meal,
            price = price,
            adress = adress,
            totalTime = time,
            uiPrefab = ui,
            Transform = transform,
            Count = countative
        };
        orders.Add(mealOrder);
        mealOrder.Initialize();
        Instantiate(orderAlarm,GameObject.Find("Canvas").transform);
    }
    public static List<MealOrder> orders = new List<MealOrder>();
    public static List<GameObject> ordersUI = new List<GameObject>();
    public static float orderChanceCooldown()
    {
        float constantFactor = PotantielCustomerCount * OnlineCustomerChance * hungryCustomerChance * influenceOfYou;
        float chanceFactor = Random.Range(-chanceRange + dailyChance, dailyChance + chanceRange);
        constantFactor = 1 / constantFactor;
        return constantFactor - chanceFactor;
    }
    public static bool orderTrigger()
    {
        int random = Random.Range(-50, 100) + failSum;
        if (random > 80)
        {
            failSum = 0;
            return true;
        }
        failSum++;
        return false;
    }
    private void Update()
    {
        cooldownSave -= Time.deltaTime;
        if(cooldownSave <= 0)
        {
            cooldownSave = orderChanceCooldown();
            if (orderTrigger())
            {
                Debug.Log("created");
                int range = availableMeals.Length;
                GameObject meal = availableMeals[Random.Range(0, range)];
                CreateOrder(orderPrefab, transform, _meal: meal);
            }
        }
        foreach (var item in orders)
        {
            item.Update();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int range = availableMeals.Length;
            int count = Random.Range(1, 6);
            GameObject meal = availableMeals[Random.Range(0, range)];
            CreateOrder(orderPrefab, transform, time:Random.Range(20,60), _meal: meal,countative:count);
        }
    }
    public static void IncreasePotCustomer()
    {
        PotantielCustomerCount += 20;
        increasePotantielCustomer.GetComponent<Animator>().SetTrigger("Work");
        increasePotantielCustomer.GetComponent<Text>().text = "+20";
    }
    public static void DecreasePotCustomer()
    {
        PotantielCustomerCount -= 20;
        decreasePotantielCustomer.GetComponent<Animator>().SetTrigger("Work");
        decreasePotantielCustomer.GetComponent<Text>().text = "-20";
    }
    private void Start()
    {
        orderAlarm = _orderAlarm;
        increasePotantielCustomer = GameObject.FindGameObjectWithTag("potantielCustomerUI").transform.
            GetChild(2).gameObject;
        decreasePotantielCustomer = GameObject.FindGameObjectWithTag("potantielCustomerUI").transform.
            GetChild(3).gameObject;
        orderInstance = this;
    }
}
