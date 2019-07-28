using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ImageCreator : MonoBehaviour {
    bool inventoryOpen = false;
    public GameObject avatar;
    public GameObject label;
    public GameObject Player;
    public Sprite defaultSprite;
    public GameObject[] slotUIs;
    public static GameObject willCreate = null;
    public static InventorySlot slot1 = new InventorySlot() { typeOfItem = null, count = 0, index = 0 };
    public static InventorySlot slot2 = new InventorySlot() { typeOfItem = null, count = 0, index = 1 };
    public static InventorySlot slot3 = new InventorySlot() { typeOfItem = null, count = 0, index = 2 };
    public static InventorySlot slot4 = new InventorySlot() { typeOfItem = null, count = 0, index = 3 };
    public static InventorySlot slot5 = new InventorySlot() { typeOfItem = null, count = 0, index = 4 };
    public static InventorySlot slot6 = new InventorySlot() { typeOfItem = null, count = 0, index = 5 };
    public static InventorySlot slot7 = new InventorySlot() { typeOfItem = null, count = 0, index = 6 };
    public static InventorySlot slot8 = new InventorySlot() { typeOfItem = null, count = 0, index = 7 };
    public static InventorySlot[] slots = new InventorySlot[8] { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8 };
    [SerializeField]
    GameObject slotPrefab;
    int Count = 0;
    public static GameObject stationCap;
    [SerializeField] GameObject _count;
    bool changed = true;
    
    public void GetQuantaty()
    {
        Count = int.Parse(_count.GetComponent<InputField>().text);
    }
    public void Create()
    {
        GetQuantaty();
        GameObject Output = RecipeController.GetRecipe();
        int[] takeFromInventory = new int[8];
        int[] takeFromStation = new int[8];
        for (int i = 0; i < 8; i++)
        {
            if (slots[i].typeOfItem != null)
            {
                if (slots[i].unitCount <= supplyCalculator(slots[i].typeOfItem,out takeFromInventory[i],out takeFromStation[i]))
                {

                }
                else
                {
                    Debug.Log("eksik" + slots[i].typeOfItem);
                    return;
                }
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (slots[i].unitCount * Count > takeFromStation[i])
            {
                Station.BackTransaction(slots[i].typeOfItem, takeFromStation[i]);
                int surplus = (int)(slots[i].unitCount * Count - takeFromStation[i]);
                InventoryOfPlayer.BackTransaction(slots[i].typeOfItem, surplus);
            }
            else
            {
                Station.BackTransaction(slots[i].typeOfItem, (int)slots[i].unitCount*Count);
            }

            
        }
        PlayerController.current.GetComponent<Station>().Create(Output,Count);
        //GameObject[] outputAndCurrent = new GameObject[2] { Output, PlayerController.current };
        //StartCoroutine("animateAndCreate", outputAndCurrent);
    }

    public static void TakeIn(GameObject sended,out bool decrease)
    {
        decrease = true;
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].typeOfItem == sended)
            {
                slots[i].count++;
                return;
            }
        }
        decrease = false;
    }

    public static void Closure()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Station.Transaction(slots[i].typeOfItem, slots[i].count);
            slots[i].count = 0;
        }
    }

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
            if (typeOfItem.GetComponent<Meal>() != null) return MaterialType.meal;
            else if (typeOfItem.GetComponent<MealMaterial>() != null) return MaterialType.material;
            else if (typeOfItem.GetComponent<RawMaterial>() != null) return MaterialType.rawMaterial;
            else return MaterialType.nullable;
        }
        public GameObject typeOfItem;
        public int count;
        public int index;
        public float unitCount;
        public Sprite GetSprite()
        {
            switch (GetMaterialType())
            {
                case MaterialType.meal:
                    return typeOfItem.GetComponent<Meal>().image;
                    break;
                case MaterialType.material:
                    return typeOfItem.GetComponent<MealMaterial>().image;
                    break;
                case MaterialType.rawMaterial:
                    return typeOfItem.GetComponent<RawMaterial>().image;
                    break;
                default:
                    return null;
                    break;
            }
        }
    }

    void Update () {
        DefiningOFSlotsGOs();
        UIArrangements();
        updateStationCapacityText();
    }
    public GameObject OutputFinder(string name)
    {
        GameObject current = PlayerController.current;
        GameObject[] outputs = current.GetComponent<Station>().Outputs;
        foreach(var item in outputs)
        {
            Debug.Log("Ouyput Finder:" + item.name);
            if(item.name == name)
            {
                willCreate = item;
                return item;
            }
        }
        return null;
    }
    

    public void DefiningOFSlotsGOs()
    {
        GameObject Output = RecipeController.GetRecipe();
        GameObject[] inputs= new GameObject[1];
        float [] counts = new float[5];
        if(Output != null)
        {
            if (Output.GetComponent<MealMaterial>() != null)
            {
                inputs = Output.GetComponent<MealMaterial>().Inputs;
                counts = Output.GetComponent<MealMaterial>().InputCount;
            }
            else if (Output.GetComponent<Meal>() != null)
            {
                inputs = Output.GetComponent<Meal>().Inputs;
                counts = Output.GetComponent<Meal>().InputCount;
            }
            for (int i = 0; i < inputs.Length; i++)
            {
                slots[i].typeOfItem = inputs[i];
                slots[i].unitCount = counts[i];
            }
            for (int i = inputs.Length; i < slots.Length; i++)
            {
                slots[i].typeOfItem = null;
            }
        }
    }

    void UIArrangements()
    {
        
        GameObject Output = RecipeController.GetRecipe();
        if (!PlayerController.current.GetComponent<Station>().crafting)
        {
            if(Output != null)
            {
                if (Output.GetComponent<MealMaterial>() != null)
                {
                    avatar.GetComponent<Image>().sprite = Output.GetComponent<MealMaterial>().image;
                    avatar.GetComponent<Image>().color = Color.white;
                    label.GetComponent<Text>().text = Output.name;
                    GameObject definition = transform.parent.GetChild(3).gameObject;
                    definition.GetComponent<Text>().text = Output.GetComponent<MealMaterial>().Definition;
                    GameObject Time = transform.parent.GetChild(10).GetChild(0).gameObject;
                    int unitTimeTimesCount = Mathf.FloorToInt(Output.GetComponent<MealMaterial>().unitTime * Count);
                    int minute = Mathf.FloorToInt(unitTimeTimesCount / 60);
                    int second = unitTimeTimesCount % 60;
                    string _time = minute.ToString("00") + ":" + second.ToString("00");
                    Time.GetComponent<Text>().text = _time;
                    transform.parent.GetChild(11).GetComponent<Text>().text = "gr";
                    transform.parent.GetChild(11).GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
                }
                else if (Output.GetComponent<Meal>() != null)
                {
                    avatar.GetComponent<Image>().sprite = Output.GetComponent<Meal>().image;
                    avatar.GetComponent<Image>().color = Color.white;
                    label.GetComponent<Text>().text = Output.name;
                    GameObject definition = transform.parent.GetChild(3).gameObject;
                    definition.GetComponent<Text>().text = Output.GetComponent<Meal>().Definition;
                    GameObject Time = transform.parent.GetChild(10).GetChild(0).gameObject;
                    int unitTimeTimesCount = Mathf.FloorToInt(Output.GetComponent<Meal>().unitTime * Count);
                    int minute = Mathf.FloorToInt(unitTimeTimesCount / 60);
                    int second = unitTimeTimesCount % 60;
                    string _time = minute.ToString("00") + ":" + second.ToString("00");
                    Time.GetComponent<Text>().text = _time;
                    transform.parent.GetChild(11).GetComponent<Text>().text = "Por.";
                    transform.parent.GetChild(11).GetComponent<Text>().alignment = TextAnchor.MiddleRight;
                }
                else
                {
                    avatar.GetComponent<Image>().sprite = defaultSprite;
                }
            }
        }
       

        for (int i = 0; i < 8; i++)
        {
            if (slots[i].typeOfItem != null)
            {
                slotUIs[i].SetActive(true);
                GameObject image = slotUIs[i].transform.GetChild(1).gameObject;
                GameObject label = slotUIs[i].transform.GetChild(2).gameObject;
                Text text = slotUIs[i].transform.GetChild(3).GetComponent<Text>();
                text.text = slots[i].typeOfItem.name;
                image.GetComponent<Image>().sprite = slots[i].GetSprite();
                image.GetComponent<Image>().color = Color.white;
                label.GetComponent<Text>().text = supplyCalculator(slots[i].typeOfItem) +"/"+slots[i].unitCount*Count;//buraya station deposundaki + inventorydeki malzeme miktarinin toplami yazilacaak
            }
            else
            {
                slotUIs[i].SetActive(false);
                /*
                GameObject image = slotUIs[i].transform.GetChild(1).gameObject;
                GameObject label = slotUIs[i].transform.GetChild(2).gameObject;
                image.GetComponent<Image>().sprite = defaultSprite;
                label.GetComponent<Text>().text = "0";*/
            }
        }
    }
    int supplyCalculator(GameObject item)
    {
        int calculate = 0;
        foreach (var good in InventoryOfPlayer.slots)
        {
            if(good.typeOfItem == item)
            {
                calculate += good.count;
                break;
            }
        }
        foreach(var good in PlayerController.current.GetComponent<Station>().inventory)
        {
            if (good.typeOfItem == item)
            {
                calculate += good.count;
                break;
            }
        }
        return calculate;
    }
    int supplyCalculator(GameObject item,out int fromInventory,out int fromStation )
    {
        int calculate = 0;
        fromInventory = 0;
        fromStation = 0;
        foreach (var good in InventoryOfPlayer.slots)
        {
            if (good.typeOfItem == item)
            {
                calculate += good.count;
                fromInventory = good.count;
                break;
            }
        }
        foreach (var good in PlayerController.current.GetComponent<Station>().inventory)
        {
            if (good.typeOfItem == item)
            {
                calculate += good.count;
                fromStation = good.count;
                break;
            }
        }
        return calculate;
    }
    private void Start()
    {
        stationCap = GameObject.FindGameObjectWithTag("StationCapacity");
    }

    public static void updateStationCapacityText()
    {
        Text text = stationCap.transform.GetChild(0).GetComponent<Text>();
        if(PlayerController.current != null)
        text.text = PlayerController.current.GetComponent<Station>().capacityCur.ToString() + "/" + PlayerController.current.GetComponent<Station>().capacity.ToString();
    }
}
