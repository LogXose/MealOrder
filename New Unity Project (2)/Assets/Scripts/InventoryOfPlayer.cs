using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOfPlayer : MonoBehaviour {
    public static int Money = 500;
    [SerializeField] Color blue;

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

    public static int inventorySlotRight = 4;
    public static InventorySlot slot1 = new InventorySlot() { typeOfItem = null, count = 0, index = 0 };
    public static InventorySlot slot2 = new InventorySlot() { typeOfItem = null, count = 0, index = 1 };
    public static InventorySlot slot3 = new InventorySlot() { typeOfItem = null, count = 0, index = 2 };
    public static InventorySlot slot4 = new InventorySlot() { typeOfItem = null, count = 0, index = 3 };
    public static InventorySlot slot5 = new InventorySlot() { typeOfItem = null, count = 0, index = 4 };
    public static InventorySlot slot6 = new InventorySlot() { typeOfItem = null, count = 0, index = 5 };
    public static InventorySlot slot7 = new InventorySlot() { typeOfItem = null, count = 0, index = 6 };
    public static InventorySlot slot8 = new InventorySlot() { typeOfItem = null, count = 0, index = 7 };
    public static InventorySlot slot9 = new InventorySlot() { typeOfItem = null, count = 0, index = 8 };
    public static InventorySlot slot10 = new InventorySlot() { typeOfItem = null, count = 0, index = 9 };
    public static InventorySlot[] slots = new InventorySlot[10] { slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10 };

    public GameObject[] slotUIs;
    public Sprite defaultSprite;

    public static void Transaction(GameObject TypeOfItem)
    {
        foreach (var item in slots)
        {
            if(item.typeOfItem == TypeOfItem)
            {
                item.count  += 1;
                return;
            }
        }
        foreach (var item in slots)
        {
            if(item.typeOfItem == null && item.index < inventorySlotRight)
            {
                item.typeOfItem = TypeOfItem;
                item.count = 1;
                return;
            }
        }
        Debug.Log("Could not buy it.");
    }
    public static void Transaction(GameObject TypeOfItem,out bool executed)//override for declaring decrease happend
    {
        executed = true;
        foreach (var item in slots)
        {
            if (item.typeOfItem == TypeOfItem)
            {
                item.count += 1;
                return;
            }
        }
        foreach (var item in slots)
        {
            if (item.typeOfItem == null && item.index < inventorySlotRight)
            {
                item.typeOfItem = TypeOfItem;
                item.count = 1;
                return;
            }
        }
        executed = false;
        Debug.Log("Could not buy it.");
    }

    public static void BackTransaction(GameObject TypeOfItem)
    {
        foreach (var item in slots)
        {
            if (item.typeOfItem == TypeOfItem)
            {
                if(item.count <= 1)
                {
                    item.typeOfItem = null;
                    item.count = 0;
                }
                else
                {
                    item.count--;
                }
                return;
            }
        }
    }
    public GameObject nuggetKahretsin;//iby
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))//iby
        {
            Transaction(nuggetKahretsin);
        }
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].typeOfItem != null)
            {
                GameObject image = slotUIs[i].transform.GetChild(1).gameObject;
                GameObject label = slotUIs[i].transform.GetChild(2).gameObject;
                Text nameOfItem = slotUIs[i].transform.GetChild(3).GetComponent<Text>();
                image.GetComponent<Image>().sprite = slots[i].GetSprite();
                image.GetComponent<Image>().color = Color.white;
                label.GetComponent<Text>().text = slots[i].count.ToString();
                slotUIs[i].GetComponent<PlayerSlot>().represent = slots[i].typeOfItem;
                slotUIs[i].GetComponent<PlayerSlot>().Count = slots[i].count;
                nameOfItem.text = slots[i].typeOfItem.name;
                GameObject bg = slotUIs[i].transform.GetChild(0).gameObject;
                bg.GetComponent<Image>().color = Color.white;
                slotUIs[i].GetComponent<BasicButton>().close = false;
            }
            else if(i < inventorySlotRight)
            {
                
                GameObject image = slotUIs[i].transform.GetChild(1).gameObject;
                GameObject label = slotUIs[i].transform.GetChild(2).gameObject;
                image.GetComponent<Image>().sprite = defaultSprite;
                image.GetComponent<Image>().color = blue;
                label.GetComponent<Text>().text = "0";
                slotUIs[i].GetComponent<PlayerSlot>().represent = null;
                slotUIs[i].GetComponent<PlayerSlot>().Count = 0;
                Text nameOfItem = slotUIs[i].transform.GetChild(3).GetComponent<Text>();
                slotUIs[i].GetComponent<BasicButton>().close = false;
                GameObject bg = slotUIs[i].transform.GetChild(0).gameObject;
                bg.GetComponent<Image>().color = Color.white;
                nameOfItem.text = "EMPTY";
            }
            else
            {
                GameObject image = slotUIs[i].transform.GetChild(1).gameObject;
                GameObject label = slotUIs[i].transform.GetChild(2).gameObject;
                image.GetComponent<Image>().sprite = defaultSprite;
                label.GetComponent<Text>().text = "";
                slotUIs[i].GetComponent<PlayerSlot>().represent = null;
                slotUIs[i].GetComponent<PlayerSlot>().Count = 0;
                Text nameOfItem = slotUIs[i].transform.GetChild(3).GetComponent<Text>();
                GameObject bg = slotUIs[i].transform.GetChild(0).gameObject;
                Color blueLowAlpha = blue - new Color(0, 0, 0, 0.5f);
                Color whiteLowAlpha = Color.white - new Color(0, 0, 0, 0.5f);
                image.GetComponent<Image>().color = blueLowAlpha;
                bg.GetComponent<Image>().color = whiteLowAlpha;
                slotUIs[i].GetComponent<BasicButton>().close = true;
                nameOfItem.text = "CLOSE";
            }
        }
    }
}
