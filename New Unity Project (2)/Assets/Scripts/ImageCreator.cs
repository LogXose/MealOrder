using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public static InventorySlot[] slots = new InventorySlot[6] { slot1, slot2, slot3, slot4, slot5, slot6 };

    public void triggerCreate()
    {
        Create();
    }

    public static void Create()
    {
        bool control = false;
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].typeOfItem != null)
            {
                if(slots[i].count > 0)
                {
                    control = true;
                }
                else
                {
                    control = false;
                    break;
                }
            }
        }
        if (control)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].typeOfItem != null)
                {
                    slots[i].count--;
                }
            }
            Station.Transaction(willCreate);
        }
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
            while(slots[i].count > 0)
            {
                slots[i].count--;
                Station.Transaction(slots[i].typeOfItem);
            }
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

    void Update () {
        DefiningOFSlotsGOs();
        UIArrangements();
    }

    public GameObject OutputFinder(string name)
    {
        GameObject current = PlayerController.current;
        GameObject[] outputs = current.GetComponent<Station>().Outputs;
        foreach(var item in outputs)
        {
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
        GameObject Output = OutputFinder(label.GetComponent<Text>().text);
        GameObject[] inputs= new GameObject[1];
        if (Output.GetComponent<Material>() != null)
        {
            inputs = Output.GetComponent<Material>().Inputs;
        }
        else if (Output.GetComponent<Meal>() != null)
        {
            inputs = Output.GetComponent<Meal>().Inputs;
        }
        for (int i = 0;i < inputs.Length; i++)
        {
            slots[i].typeOfItem = inputs[i];
            
        }
        for (int i = inputs.Length; i < slots.Length; i++)
        {
            slots[i].typeOfItem = null;
        }
    }

    void UIArrangements()
    {
        GameObject Output = OutputFinder(label.GetComponent<Text>().text);
        if (Output.GetComponent<Material>() != null)
        {
            avatar.transform.GetChild(0).GetComponent<Image>().sprite = Output.GetComponent<Material>().image;
            avatar.transform.GetChild(0).localScale = Vector3.one * 0.6f;
        }
        else if (Output.GetComponent<Meal>() != null)
        {
            avatar.transform.GetChild(0).GetComponent<Image>().sprite = Output.GetComponent<Meal>().image;
            avatar.transform.GetChild(0).localScale = Vector3.one * 0.6f;
        }
        else
        {
            avatar.transform.GetChild(0).GetComponent<Image>().sprite = defaultSprite;
            avatar.transform.GetChild(0).localScale = Vector3.one * 0.25f;
        }
        for (int i = 0; i < 6; i++)
        {
            if (slots[i].typeOfItem != null)
            {
                GameObject image = slotUIs[i].transform.GetChild(0).gameObject;
                GameObject label = slotUIs[i].transform.GetChild(1).GetChild(0).gameObject;
                image.GetComponent<Image>().sprite = slots[i].GetSprite();
                image.transform.localScale = Vector3.one * 0.4f;
                label.GetComponent<Text>().text = slots[i].count.ToString();
                image.GetComponent<Image>().sprite = slots[i].GetSprite();
                image.transform.localScale = Vector3.one * 0.4f;
                label.GetComponent<Text>().text = slots[i].count.ToString();
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
            }
        }
    }
}
