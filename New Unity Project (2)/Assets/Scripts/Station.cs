using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Station : MonoBehaviour {
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
                else if (typeOfItem.GetComponent<Material>() != null) return MaterialType.material;
                else if (typeOfItem.GetComponent<RawMaterial>() != null) return MaterialType.rawMaterial;
            }
            return MaterialType.nullable;
        }
        public GameObject typeOfItem;
        public int count;
        public int index;
        public Sprite GetSprite()
        {
            Debug.Log(typeOfItem);
            switch (GetMaterialType())
            {
                case MaterialType.meal:
                    return typeOfItem.GetComponent<Meal>().image;
                case MaterialType.material:
                    return typeOfItem.GetComponent<Material>().image;
                case MaterialType.rawMaterial:
                    return typeOfItem.GetComponent<RawMaterial>().image;
                default:
                    return null;
            }
        }
    }
    [SerializeField] GameObject TimerGO;
    [SerializeField] GameObject CountGO;
    [SerializeField] GameObject ButtonGO;
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
        Boiler
    }
    public StationType stationType;

    private void Awake()
    {
        inventory = new InventorySlot[8] { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8 };
    }

    public static void Transaction(GameObject sended)
    {
        GameObject current = PlayerController.current;
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == sended)
            {
                current.GetComponent<Station>().inventory[i].count++;
                return;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == null)
            {
                current.GetComponent<Station>().inventory[i].typeOfItem = sended;
                current.GetComponent<Station>().inventory[i].count = 1;
                return;
            }
        }
    }
    public void TransactionIE(GameObject current,GameObject sended)
    {
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == sended)
            {
                current.GetComponent<Station>().inventory[i].count++;
                return;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == null)
            {
                current.GetComponent<Station>().inventory[i].typeOfItem = sended;
                current.GetComponent<Station>().inventory[i].count = 1;
                return;
            }
        }
    }
    public static void BackTransaction(GameObject item)
    {
        GameObject current = PlayerController.current;
        for (int i = 0; i < 8; i++)
        {
            if (current.GetComponent<Station>().inventory[i].typeOfItem == item)
            {
                current.GetComponent<Station>().inventory[i].count--;
                return;
            }
        }
    }
    public void Create(GameObject Output)
    {
        StartCoroutine("animateAndCreate", Output);
    }

    IEnumerator animateAndCreate(GameObject Output)
    {
        int Count = int.Parse(CountGO.transform.GetChild(0).GetComponent<Text>().text);
        int unitTimeTimesCount = 0;
        if (Output.GetComponent<Material>())
        {
            unitTimeTimesCount = Output.GetComponent<Material>().unitTime * Count;
        }
        else
        {
            unitTimeTimesCount = Output.GetComponent<Meal>().unitTime * Count;
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
        for (int i = 0; i < Count; i++)
        {
            GetComponent<Station>().TransactionIE(gameObject, Output);

        }
    }

    private void Update()
    {
        if(Counter > 0)
        {
            Counter -= Time.deltaTime;
            if(PlayerController.current == gameObject)
            {
                TimerGO.SetActive(true);
                int minute = Mathf.FloorToInt(Counter / 60);
                int second = (int)Counter % 60;
                string rest = minute.ToString("00") + ":" + second.ToString("00");
                TimerGO.transform.GetChild(0).GetComponent<Text>().text = rest;
            }
            else
            {
                TimerGO.SetActive(false);
            }
        }
        else if(PlayerController.current == gameObject)
        {
            TimerGO.SetActive(false);
        }
    }
}
