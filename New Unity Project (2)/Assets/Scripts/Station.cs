using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Station : MonoBehaviour {

    public static Dictionary<StationType, List<GameObject>> stationOutputList = new Dictionary<StationType, List<GameObject>>();
    public static int adjusterIndex;
    public static int sendQuantaty;
    public static int indexCounter = 0;
    public int price = 150;
    public Station()
    {
        if (stationOutputList.Count == 0)
        {
            foreach (StationType item in Enum.GetValues(typeof(StationType)))
            {
                stationOutputList.Add(item, new List<GameObject>());
            }
        }
    }

    private void Start()
    {
        Outputs = stationOutputList[stationType].ToArray();
    }
    public int capacity = 10000;
    public int capacityCur = 0;
    public class InventorySlot
    {
        public enum MaterialType
        {
            meal,
            material,
            rawMaterial,
            nullable
        }
        public MaterialType GetMaterialType()
        {
            if(typeOfItem != null)
            {
                if (typeOfItem.GetComponent<Meal>() != null) return MaterialType.meal;
                else if (typeOfItem.GetComponent<MealMaterial>() != null) return MaterialType.material;
                else if (typeOfItem.GetComponent<RawMaterial>() != null) return MaterialType.rawMaterial;
            }
            return MaterialType.nullable;
        }
        public GameObject typeOfItem;
        public int count;
        public int index;
        public Sprite GetSprite()
        {
            switch (GetMaterialType())
            {
                case MaterialType.meal:
                    return typeOfItem.GetComponent<Meal>().image;
                case MaterialType.material:
                    return typeOfItem.GetComponent<MealMaterial>().image;
                case MaterialType.rawMaterial:
                    return typeOfItem.GetComponent<RawMaterial>().image;
                default:
                    return null;
            }
        }
    }
    static GameObject TimerGO;
    static GameObject CountGO;
    static GameObject ButtonGO;
    public GameObject craftingGO = null;
    public bool crafting = false;
    public float Counter = 0;
    public int InputCount;
    public GameObject[] Outputs;
    public InventorySlot slot1 = new InventorySlot() { typeOfItem = null, count = 0, index = 0 };
    public InventorySlot slot2 = new InventorySlot() { typeOfItem = null, count = 0, index = 1 };
    public InventorySlot slot3 = new InventorySlot() { typeOfItem = null, count = 0, index = 2 };
    public InventorySlot slot4 = new InventorySlot() { typeOfItem = null, count = 0, index = 3 };
    public InventorySlot slot5 = new InventorySlot() { typeOfItem = null, count = 0, index = 4 };
    public InventorySlot slot6 = new InventorySlot() { typeOfItem = null, count = 0, index = 5 };
    public InventorySlot slot7 = new InventorySlot() { typeOfItem = null, count = 0, index = 6 };
    public InventorySlot slot8 = new InventorySlot() { typeOfItem = null, count = 0, index = 7 };
    public InventorySlot[] inventory;
    public int stokRight = 8;
    public enum StationType
    {
        DoughCutter,
        DoughKneader,
        Boiler,
        Dryer
    }
    public StationType stationType;
    public Sprite icon;
    public int stationIndex;
    public GameObject stationPrefab;
    public Vector3 stationPosition;
    public int creatingQuantaty;
    public GameObject creatingOutput;
    private void Awake()
    {
        inventory = new InventorySlot[8] { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8 };
    }

    

    public static void Transaction(GameObject sended,int quantaty)
    {
        GameObject current = PlayerController.current;
        Station station = current.GetComponent<Station>();
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == sended && station.capacityCur + quantaty < station.capacity)
            {
                current.GetComponent<Station>().inventory[i].count += quantaty;
                current.GetComponent<Station>().capacityCur += quantaty;
                ImageCreator.updateStationCapacityText();
                return;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == null)
            {
                current.GetComponent<Station>().inventory[i].typeOfItem = sended;
                current.GetComponent<Station>().inventory[i].count += quantaty;
                station.capacityCur += quantaty;
                ImageCreator.updateStationCapacityText();
                return;
            }
        }
    }
    public void TransactionIE(GameObject current,GameObject sended,int quantaty)
    {
        Station station = current.GetComponent<Station>();
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == sended && station.capacityCur + quantaty < station.capacity)
            {
                current.GetComponent<Station>().inventory[i].count += quantaty;
                station.capacityCur += quantaty;
                ImageCreator.updateStationCapacityText();
                return;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == null && station.capacityCur + quantaty < station.capacity)
            {
                current.GetComponent<Station>().inventory[i].typeOfItem = sended;
                current.GetComponent<Station>().inventory[i].count += quantaty;
                station.capacityCur += quantaty;
                ImageCreator.updateStationCapacityText();
                return;
            }
        }
    }
    public static void BackTransaction(GameObject item,int quantaty)
    {
        GameObject current = PlayerController.current;
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == item)
            {
                current.GetComponent<Station>().inventory[i].count -= quantaty;
                ImageCreator.updateStationCapacityText();
                return;
            }
        }
    }

    public struct ieStruct
    {
        public GameObject Output;
        public int quant;
        public float time;
    }

    public void Create(GameObject Output,int quantaty)
    {
        TimerGO = GameObject.FindGameObjectWithTag("TimerGO");
        ButtonGO = GameObject.FindGameObjectWithTag("ButtonGO");
        creatingQuantaty = quantaty;
        ieStruct paket;
        paket.Output = Output;
        paket.quant = quantaty;
        paket.time = 0;
        StartCoroutine("animateAndCreate", paket);
    }

    IEnumerator animateAndCreate(ieStruct Paket)
    {
        int Count = int.Parse(GameObject.FindGameObjectWithTag("CountGO").transform.GetChild(1).GetComponent<Text>().text);
        int unitTimeTimesCount = 0;
        GameObject Output = Paket.Output;
        creatingOutput = Paket.Output;
        int quantaty = Paket.quant;
        if (Output.GetComponent<MealMaterial>())
        {
            unitTimeTimesCount = Mathf.FloorToInt(Output.GetComponent<MealMaterial>().unitTime * Count);
        }
        else
        {
            unitTimeTimesCount = Mathf.FloorToInt(Output.GetComponent<Meal>().unitTime * Count);
        }
        crafting = true;
        GetComponent<Animator>().SetTrigger("Work");
        GetComponent<Station>().Counter = unitTimeTimesCount;
        craftingGO = Output;
        ButtonGO.GetComponent<BasicButton>().close = true;
        Color buttonColor = ButtonGO.GetComponent<Image>().color;
        buttonColor.a = 0.3f;
        ButtonGO.GetComponent<Image>().color = buttonColor;
        yield return new WaitForSeconds(unitTimeTimesCount);
        buttonColor.a = 1;
        ButtonGO.GetComponent<Image>().color = buttonColor;
        craftingGO = null;
        ButtonGO.GetComponent<BasicButton>().close = false;
        crafting = false;
        GetComponent<Animator>().SetTrigger("Stop");
        GetComponent<Station>().TransactionIE(gameObject, Output,quantaty);
    }

    public void Create(ieStruct paket)
    {
        StartCoroutine("leftAnimateAndCrate", paket);
        Debug.Log("created");
    }

    public IEnumerator leftAnimateAndCrate(ieStruct paket)
    {
        crafting = true;
        GetComponent<Animator>().SetTrigger("Work");
        Counter = paket.time;
        craftingGO = paket.Output;
        ButtonGO.GetComponent<BasicButton>().close = true;
        Color buttonColor = ButtonGO.GetComponent<Image>().color;
        buttonColor.a = 0.3f;
        ButtonGO.GetComponent<Image>().color = buttonColor;
        yield return new WaitForSeconds(paket.time);
        buttonColor.a = 1;
        ButtonGO.GetComponent<Image>().color = buttonColor;
        craftingGO = null;
        ButtonGO.GetComponent<BasicButton>().close = false;
        crafting = false;
        GetComponent<Animator>().SetTrigger("Stop");
        GetComponent<Station>().TransactionIE(gameObject, paket.Output, paket.quant);
    }

    private void Update()
    {
        if(Counter > 0)
        {
            Counter -= Time.deltaTime;
            if(PlayerController.current == gameObject)
            {
                //TimerGO.SetActive(true);
                TimerGO = GameObject.FindGameObjectWithTag("TimerGO");
                int minute = Mathf.FloorToInt(Counter / 60);
                int second = (int)Counter % 60;
                string rest = minute.ToString("00") + ":" + second.ToString("00");
                TimerGO.transform.GetChild(0).GetComponent<Text>().text = rest;
            }
            /*else
            {
                TimerGO.SetActive(false);
            }*/
        }
        if(Outputs.Length != stationOutputList[stationType].Count)
        {
            Outputs = stationOutputList[stationType].ToArray();
        }
        /*else if(PlayerController.current == gameObject)
        {
            TimerGO.SetActive(false);
        }*/
    }
    public static void Transfer()
    {
        bool execution = false;
        InventoryOfPlayer.Transaction(PlayerController.current.GetComponent<Station>().inventory[adjusterIndex].typeOfItem,out execution, sendQuantaty);
        Debug.Log(InventoryOfPlayer.tkgCur);
        if (execution) {
            BackTransaction(PlayerController.current.GetComponent<Station>().inventory[adjusterIndex].typeOfItem, sendQuantaty);
            Debug.Log(execution);
        }
    }
}
