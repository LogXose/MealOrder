using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour {
    public static Order orderInstance;
    /*public static int PotantielCustomerCount = 50;
    public static float OnlineCustomerChance = 0.09f;
    public static float hungryCustomerChance = 0.05f;
    public static float influenceOfYou = 0.25f;
    public static float dailyChance = 3;
    public static float chanceRange = 10;
    public static int failSum = 0;
    public static float cooldownSave = 0;*/
    float anlikSayiA = 10;
    float potansiyelSayiP = 0;
    float artisHiziC = 0;
    [SerializeField]float incSabit = 0.02f;
    Dictionary<int, float> saatlikDegisken = new Dictionary<int, float> {{1,12 },{2,11},{ 3,9},{4,3 },{ 5,3},{6,5 },{7,8 }, { 8, 12 }, { 9, 15 },
        { 10, 18 },{11,21 },{ 12, 24 },{ 13, 27 },{ 14, 30 },
        { 15, 30 }, { 16, 30 }, { 17, 30 }, { 18, 30 }, { 19, 30 },{20, 30 },{ 21,26},{ 22,18},{ 23,13},{0,12 } };
    static int saat = 1;
    int yemekCesidi = 0;
    float reklam = 1;
    float anlikSave = 0;
    [SerializeField] Text anlikSayi;
    int saatSaniyeLog = 200;
    public static float zaman = 1700;
    static int dakika = 0;
    [SerializeField]GameObject saatGO;
    [SerializeField] float dususOrani = 10f;
    [SerializeField] float zamanBoleni = 200f;
    float dakikaAyiraci;
    [SerializeField] float siparisOlusturmaIhtimaliUstBandi = 10f;
    public struct CommentStruct
    {
        public float point;
        public string comment;
        public string name;
    }

    public GameObject orderPrefab;
    public static List<GameObject> availableMeals = new List<GameObject>();
    public static GameObject increasePotantielCustomer;
    public static GameObject decreasePotantielCustomer;
    public static GameObject orderAlarm;
    public GameObject _orderAlarm;
    public static GameObject ServiceStation;
    public class MealOrder
    {
        public GameObject uiPrefab;
        public GameObject ui;
        public GameObject meal;
        public Sprite sprite;
        public string name;
        public string adress;
        public float price;
        public float totalTime;
        public float remainingTime;
        public float spendedTime = 0;
        public int Count = 0;
        public float DistanceTime = 0;
        public bool started = true;
        public bool paused = false;
        public bool completed = false;
        private bool finisher = false;
        private bool cancelOrder = false;
        private bool countDownDistance;
        public Segmentation.CustomerSegmentation CustomerSegmentation;
        public CommentStruct commentStruct;
        public bool isItAvaiable()
        {
            int calculate = 0;
            foreach (var good in InventoryOfPlayer.slots)
            {
                if (good.typeOfItem == meal)
                {
                    calculate += good.count;
                    break;
                }
            }
            if(calculate >= Count)
            {
                return true;
            }
            return false;
        }
        public void Update()
        {
            if(!paused && started) spendedTime += Time.deltaTime;
            int sec = (int)spendedTime % 60;
            int min = (int)spendedTime / 60;
            string time = min.ToString("0") + ":" + sec.ToString("00") + "   Expected Delivery Time: 5:00";
            if(ui != null)
            ui.transform.GetChild(3).GetComponent<Text>().text = time;
            if (completed)
            {
                ui.transform.GetChild(6).gameObject.SetActive(false);
                paused = true;
                Finisher(false);
                for (int i = 0; i < Count; i++)
                {
                    ServiceStation.transform.GetChild(3).GetChild(i).gameObject.SetActive(true);
                }
                ServiceStation.GetComponent<Animator>().SetTrigger("Work");
                countDownDistance = true;
                completed = false;
            }
            if (countDownDistance)
            {
                DistanceTime -= Time.deltaTime;
            }
            if(DistanceTime < 0)
            {
                for (int i = 0; i < ServiceStation.transform.GetChild(3).childCount; i++)
                {
                    ServiceStation.transform.GetChild(3).GetChild(i).gameObject.SetActive(false);
                }
                ServiceStation.GetComponent<Animator>().SetTrigger("Stop");
            }
            if(remainingTime < 0)
            {
                paused = true;
                Finisher(true);
            }
            Color blue = ui.transform.GetChild(6).GetChild(0).GetComponent<Image>().color;
            if (isItAvaiable())
            {
                blue.a = 1;
                ui.transform.GetChild(6).GetChild(0).GetComponent<Image>().color = blue;
                ui.transform.GetChild(6).GetComponent<BasicButton>().enabled = true;
            }
            else
            {
                blue.a = 0.2f;
                ui.transform.GetChild(6).GetChild(0).GetComponent<Image>().color = blue;
                ui.transform.GetChild(6).GetComponent<BasicButton>().enabled = false;
            }
        }
        public void Initialize()
        {
            if (meal != null)
            {
                name = meal.name;
                sprite = meal.GetComponent<Meal>().image;
            }
            ui = Instantiate(uiPrefab, GameObject.FindGameObjectWithTag("orderContent").transform);
            ui.GetComponent<OrderedMeal>().MealOrder = this; 
            ui.transform.GetChild(2).GetComponent<Image>().sprite = sprite;
            ui.transform.GetChild(0).GetComponent<Text>().text = name;
            ui.transform.GetChild(4).GetComponent<Text>().text = Count.ToString();
        }
        private void Finisher(bool negative)
        {
            if (negative)
            {

            }
            else
            {
                Meal _meal = meal.GetComponent<Meal>();
                commentStruct = new CommentStruct(); 
                commentStruct.point = _meal.points[CustomerSegmentation] * ((zaman - _meal.createdTime) / _meal.createdTime) * Random.Range(0.8f, 1.2f);
                commentStruct.comment = CommentDetail(commentStruct.point);
                commentStruct.name = NameProducer();
                MealList.points.Add(commentStruct);
            }
        }
    }
    public static void CreateOrder(GameObject ui,int countative = 1,
        GameObject _meal = null, string adress = "ananin amina", float time = 50,float Distance = 200,Segmentation.CustomerSegmentation segmentasyon = Segmentation.CustomerSegmentation.families)
    {
        MealOrder mealOrder = new MealOrder()
        {
            meal = _meal,
            price = _meal.GetComponent<Meal>().price,
            adress = adress,
            totalTime = time,
            uiPrefab = ui,
            Count = countative,
            DistanceTime = Distance,
            CustomerSegmentation = segmentasyon
        };
        orders.Add(mealOrder);
        mealOrder.Initialize();
        Instantiate(orderAlarm,GameObject.Find("Canvas").transform);
    }
    public static List<MealOrder> orders = new List<MealOrder>();
    public static List<GameObject> ordersUI = new List<GameObject>();
    /*public static float orderChanceCooldown()
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
    }*/
    int dakikaSave;
    private void Update()
    {
        /*
        cooldownSave -= Time.deltaTime;
        if(cooldownSave <= 0 && availableMeals.Count > 0)
        {
            cooldownSave = orderChanceCooldown();
            if (orderTrigger())
            {
                int range = availableMeals.Count;
                GameObject meal = availableMeals[Random.Range(0, range)];
                CreateOrder(orderPrefab, _meal: meal);
            }
        }*/
        rastgeleDusus();
        zaman += Time.deltaTime;
        saat = Mathf.FloorToInt(zaman / zamanBoleni) % 24;
        dakika = Mathf.FloorToInt((zaman % zamanBoleni)  / dakikaAyiraci);
        string time = saat.ToString("00") + ":" + dakika.ToString("00");
        saatGO.GetComponent<Text>().text = time;
        potansiyelSayiP = Mathf.FloorToInt(saatlikDegisken[saat] * Mathf.Log10(yemekCesidi + 10) * reklam);
        artisHiziC = (potansiyelSayiP - anlikSayiA) * incSabit * Time.deltaTime;
        anlikSayiA += artisHiziC;
        Debug.Log(potansiyelSayiP);
        if(Mathf.FloorToInt(anlikSayiA)> Mathf.FloorToInt(anlikSave))
        {
            anlikSayiArtisi();
        }else if(Mathf.FloorToInt(anlikSave) > Mathf.FloorToInt(anlikSayiA))
        {
            anlikSayiArtisi();
        }
        foreach (var item in orders)
        {
            item.Update();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int range = availableMeals.Count;
            int count = Random.Range(1, 5);
            GameObject meal = availableMeals[Random.Range(0, range)];
            CreateOrder(orderPrefab, time:Random.Range(20,60), _meal: meal,countative:count,segmentasyon: Segmentation.CustomerSegmentation.families);
        }
        anlikSave = anlikSayiA;
        if(dakika != dakikaSave && availableMeals.Count > 0)
        {
            SiparisOlusturmaLotosu();
        }
        dakikaSave = dakika;
    }
   /*public static void IncreasePotCustomer()
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
    }*/
    private void Start()
    {
        orderAlarm = _orderAlarm;
        orderInstance = this;
        ServiceStation = GameObject.FindGameObjectWithTag("delivery");
        anlikSayi.text = Mathf.FloorToInt(anlikSayiA).ToString();
        dakikaAyiraci = zamanBoleni / 60f;
    }
    void anlikSayiArtisi()
    {
        anlikSayi.text = Mathf.FloorToInt(anlikSayiA).ToString();
        anlikSayi.transform.parent.GetComponent<Animator>().SetTrigger("Increase");
    }
    void rastgeleDusus()
    {
        float ihtimal = anlikSayiA / potansiyelSayiP;
        float rastgele = Random.Range(0f, dususOrani);
        if(rastgele < ihtimal)
        {
            anlikSayiA--;
        }
    }
    void SiparisOlusturmaLotosu()
    {
        Debug.Log("fonksiyon Calisiyor");
        for (int i = 0; i < anlikSayiA; i++)
        {
            float rastgele = Random.Range(0f, siparisOlusturmaIhtimaliUstBandi);
            float ihtimal = availableMeals.Count* reklam * Mathf.Pow( averageProfit(),3); //(* liste sayisi) gibi bir algoritma kurulmasi gerekli.
            Debug.Log("rastgele=" + rastgele + "  ihtimal=" + ihtimal);
            if(ihtimal > rastgele * 2)
            {
                float toplam = 0;
                foreach (GameObject item in availableMeals)
                {
                    toplam += item.GetComponent<Meal>().realAveragePoint;
                }
                float altRastgele = Random.Range(0f, toplam);
                toplam = 0;
                foreach (GameObject item in availableMeals)
                {
                    toplam += item.GetComponent<Meal>().realAveragePoint;
                    if(altRastgele <= toplam)
                    {
                        Segmentation.CustomerSegmentation _segmentation = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().region.segmentation(Random.Range(0, 100));
                        CreateOrder(orderPrefab, 2, item, segmentasyon: _segmentation);
                    }
                }
                anlikSayiA--;
                return;
            }
            else if (ihtimal > rastgele)
            {
                float toplam = 0;
                foreach (GameObject item in availableMeals)
                {
                    toplam += item.GetComponent<Meal>().realAveragePoint;
                }
                float altRastgele = Random.Range(0f, toplam);
                toplam = 0;
                foreach (GameObject item in availableMeals)
                {
                    toplam += item.GetComponent<Meal>().realAveragePoint;
                    Debug.Log("alt toplam= " + toplam + " alt Rastgele=" + altRastgele);
                    if (altRastgele <= toplam)
                    {
                        Segmentation.CustomerSegmentation _segmentation = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().region.segmentation(Random.Range(0, 100));
                        CreateOrder(orderPrefab, 1, item, segmentasyon: _segmentation);
                    }
                }
                anlikSayiA--;
                return;
            }
        }
    }

    float averageProfit()
    {
        float total = 0f;
        float counter = 0f;
        foreach (var item in availableMeals)
        {
            counter++;
            total += item.GetComponent<Meal>().estimatedCost / item.GetComponent<Meal>().price;
        }
        return total / counter;
    }

    public static string CommentDetail(float point)
    {
        if (point < 1)
        {
            string[] vs = new string[] { "It is a nice shit", "You dont have any reason for still opening every morning.", "Fuck. Fuck you. Fuck everything" };
            return vs[Random.Range(0, 3)];
        }
        else if (point < 2)
        {
            string[] vs = new string[] { "Maybe, just maybe, you can be last choice.", "If you have not any other food, you can eat this shit.", "I will recorrect this as 10, if you give my money back" };
            return vs[Random.Range(0, 3)];
        }
        else if (point < 3)
        {
            string[] vs = new string[] { "I'm not sure that it is not a shit.", "One day, i believe you could too make food ", "It is too close to be a food." };
            return vs[Random.Range(0, 3)];
        }
        else if (point < 4)
        {
            string[] vs = new string[] { "It is not a shit nor food.", "I like you guys! I have 13 IQ. ", "There is a tremendous thing you should learn. That is taste." };
            return vs[Random.Range(0, 3)];
        }
        else if (point < 5)
        {
            string[] vs = new string[] { "It is at least reduced my hungry. God bless you.", "I have seen star in your eyes but it is 3000 light years away.", "Change the cook and going on!" };
            return vs[Random.Range(0, 3)];
        }
        else if (point < 6)
        {
            string[] vs = new string[] { "You got the point for your potantiel.", "Maestro but in its own store.", "Eat for living." };
            return vs[Random.Range(0, 3)];
        }
        else if (point < 7)
        {
            string[] vs = new string[] { "Bets on! Next one will be better", "Something is missing but i dont know what it is.", "Nicer dicer..." };
            return vs[Random.Range(0, 3)];
        }
        else if (point < 8)
        {
            string[] vs = new string[] { "Nice guys! Bravo! But not my style...", "Tasty. Only tasty. Nothing more.", "It was awesome. However i was too hungry so im not sure..." };
            return vs[Random.Range(0, 3)];
        }
        else if (point < 9)
        {
            string[] vs = new string[] { "Good boy!", "Tasty. Not too away to perfect...", "Thanks, it was nice!" };
            return vs[Random.Range(0, 3)];
        }
        else
        {
            string[] vs = new string[] { "Best Bro Best!", "This is my place. Perfect! ", "God mode on!" ,"I am high you  must be too"};
            return vs[Random.Range(0, 4)];
        }
    }

    public static string NameProducer()
    {
        List<string> nameList = new List<string>() { "Lewis", "James", "Logan", "Daniel", "Ryan", "Aaron", "Oliver", "Liam", "Jamie", "Ethan", "Alexander", "Cameron", "Finlay", "Kyle", "Adam", "Harry", "Matthew", "Callum", "Lucas", "Nathan", "Aiden", "Dylan", "Charlie", "Connor", "Thomas", "Alfie", "Joshua", "William", "Jayden", "Andrew", "Kai", "Max", "Ben", "Samuel", "Luke", "Tyler", "Rory", "David", "Michael", "Riley", "Noah", "Cole", "Joseph", "John", "Archie", "Jacob", "Fraser", "Rhys", "Ross", "Calum", "Jay", "Josh", "Euan", "Mason", "Owen", "Sam", "Leo", "Robert", "Scott", "Leon", "Robbie", "Benjamin", "Caleb", "Oscar", "Harris", "Murray", "Sean", "Christopher", "Kieran", "Aidan", "Jake", "Evan", "Kayden", "Arran", "Angus", "Brodie", "Ewan", "Muhammad", "Alex", "Declan", "Finn", "Blair", "Ollie", "Reece", "Corey", "Kian", "Harrison", "Taylor", "Kaiden", "Kenzie", "Cody", "Craig", "Mohammed", "Calvin", "Mark", "Jude", "Luca", "Ciaran", "George", "Zak", "Zac", "Charles", "Gregor", "Hamish", "Isaac", "Harvey", "Shay", "Struan", "Lee", "Steven", "Joe", "Lennon", "Patrick", "Jason", "Louis", "Olly", "Bailey", "Marcus", "Peter", "Sebastian", "Gabriel", "Jackson", "Zack", "Ashton", "Brandon", "Reuben", "Theo", "Paul", "Conor", "Hayden", "Lachlan", "Ruaridh", "Innes", "Stuart", "Jordan", "Sonny", "Alan", "Blake", "Zachary", "Cooper", "Ellis", "Caiden", "Fergus", "Jakub", "Zach", "Findlay", "Alistair", "Elliot", "Harley", "Anthony", "Callan", "Filip", "Louie", "Lyle", "Mohammad", "Brody", "Cayden", "Cian", "Marc", "Danny", "Shaun", "Austin", "Joel", "Nicholas", "Rio", "Rocco", "Dean", "Jonathan", "Carson", "Duncan", "Mitchell", "Ruairidh", "Stephen", "Dominic", "Kerr", "Edward", "Lloyd", "Mackenzie", "Martin", "Ali", "Henry", "Kevin", "Tom", "Alasdair", "Billy", "Freddie", "Keir", "Levi", "Junior", "Allan", "Campbell", "Darren", "Drew", "Oskar", "Arron", "Ayden", "Douglas", "Frederick", "Gary", "Seth", "Bruce", "Kaleb" };
        return nameList[Random.Range(0, nameList.Count)];
    }
}
