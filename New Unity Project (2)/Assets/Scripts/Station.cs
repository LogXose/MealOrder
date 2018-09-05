using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Furnace,
        Bench,
        Grille,
        DeepFryer,
        DoughKneader,
        Oven
    }

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

}
