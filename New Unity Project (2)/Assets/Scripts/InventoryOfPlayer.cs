using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOfPlayer : MonoBehaviour {
    public static int Money = 500;
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
            else if (typeOfItem.GetComponent<Material>() != null) return MaterialType.material;
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
                    return typeOfItem.GetComponent<Material>().image;
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

    public static int inventorySlotRight = 2;
    public static InventorySlot slot1 = new InventorySlot() { typeOfItem = null, count = 0, index = 0 };
    public static InventorySlot slot2 = new InventorySlot() { typeOfItem = null, count = 0, index = 1 };
    public static InventorySlot slot3 = new InventorySlot() { typeOfItem = null, count = 0, index = 2 };
    public static InventorySlot slot4 = new InventorySlot() { typeOfItem = null, count = 0, index = 3 };
    public static InventorySlot[] slots = new InventorySlot[4] { slot1, slot2, slot3, slot4 };

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
        for(int i = 0; i < 4; i++)
        {
            if(slots[i].typeOfItem != null)
            {
                GameObject image = slotUIs[i].transform.GetChild(0).gameObject;
                GameObject label = slotUIs[i].transform.GetChild(1).GetChild(0).gameObject;
                image.GetComponent<Image>().sprite = slots[i].GetSprite();
                image.transform.localScale = Vector3.one * 0.4f;
                label.GetComponent<Text>().text = slots[i].count.ToString();
                slotUIs[i].GetComponent<PlayerSlot>().represent = slots[i].typeOfItem;
                slotUIs[i].GetComponent<PlayerSlot>().Count = slots[i].count;
            }
            else
            {
                GameObject image = slotUIs[i].transform.GetChild(0).gameObject;
                GameObject label = slotUIs[i].transform.GetChild(1).GetChild(0).gameObject;
                image.GetComponent<Image>().sprite = defaultSprite;
                image.transform.localScale = Vector3.one * 0.25f;
                label.GetComponent<Text>().text = "0";
                image.GetComponent<Image>().sprite = defaultSprite;
                image.transform.localScale = Vector3.one * 0.25f;
                label.GetComponent<Text>().text = slots[i].count.ToString();
                if (OrdersMenuController.atOrderStation)
                {
                    image.GetComponent<Image>().sprite = defaultSprite;
                    image.transform.localScale = Vector3.one * 0.25f;
                    label.GetComponent<Text>().text = slots[i].count.ToString();
                }
                slotUIs[i].GetComponent<PlayerSlot>().represent = null;
                slotUIs[i].GetComponent<PlayerSlot>().Count = 0;
            }
        }
    }
}
