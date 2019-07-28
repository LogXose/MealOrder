using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickerPanel : MonoBehaviour {

    public enum PickerType
    {
        Kind,
        Shape,
        Flour
    }
    public PickerType pickerType = PickerType.Kind;
    [SerializeField]GameObject content;
    [SerializeField]GameObject prefab;
    PastaFeatures pastaFeatures;

    private void Awake()
    {
        pastaFeatures = GameObject.FindGameObjectWithTag("PastaFeature").GetComponent<PastaFeatures>();
    }

    public void SelectType(string name)
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
       if(name == "kind")
        {
            foreach (GameObject item in PastaFeatures._kinds)
            {
                GameObject next = Instantiate(prefab, content.transform);
                GameObject image = next.transform.GetChild(0).gameObject;
                GameObject text = next.transform.GetChild(1).gameObject;
                Instantiate(item, next.transform);
                image.GetComponent<Image>().sprite = item.GetComponent<Kind>().icon;
                text.GetComponent<Text>().text = item.name;
                pickerType = PickerType.Kind;
            }
        }else if(name == "shape")
        {
            foreach (GameObject item in PastaFeatures._shapes)
            {
                GameObject next = Instantiate(prefab, content.transform);
                GameObject image = next.transform.GetChild(0).gameObject;
                GameObject text = next.transform.GetChild(1).gameObject;
                Instantiate(item, next.transform);
                image.GetComponent<Image>().sprite = item.GetComponent<Shape>().icon;
                text.GetComponent<Text>().text = item.name;
                pickerType = PickerType.Shape;
            }
        }
        else if (name == "flour")
        {
            foreach (GameObject item in PastaFeatures._flours)
            {
                GameObject next = Instantiate(prefab, content.transform);
                GameObject image = next.transform.GetChild(0).gameObject;
                GameObject text = next.transform.GetChild(1).gameObject;
                Instantiate(item, next.transform);
                image.GetComponent<Image>().sprite = item.GetComponent<FlourType>().icon;
                text.GetComponent<Text>().text = item.name;
                pickerType = PickerType.Flour;
            }
        }else if(name == "logo")
        {
            foreach (GameObject item in PastaFeatures._flours)
            {
                GameObject next = Instantiate(prefab, content.transform);
                GameObject image = next.transform.GetChild(0).gameObject;
                GameObject text = next.transform.GetChild(1).gameObject;
                Instantiate(item, next.transform);
                image.GetComponent<Image>().sprite = item.GetComponent<FlourType>().icon;
                text.GetComponent<Text>().text = item.name;
                pickerType = PickerType.Flour;
            }
        }
    }

    public void pickedOne(GameObject GO)
    {
        switch (pickerType)
        {
            case PickerType.Kind:
                GameObject kindButton = GameObject.FindGameObjectWithTag("kind").transform.GetChild(1).gameObject;
                kindButton.GetComponent<Text>().text = GO.GetComponent<Kind>().nameGO;
                foreach (GameObject item in PastaFeatures._kinds)
                {
                    if(item.GetComponent<Kind>().nameGO == GO.GetComponent<Kind>().nameGO)
                    {
                        pastaFeatures.kind = item;
                    }
                }
                gameObject.SetActive(false);
                break;
            case PickerType.Shape:
                GameObject shapeButton = GameObject.FindGameObjectWithTag("shape").transform.GetChild(1).gameObject;
                shapeButton.GetComponent<Text>().text = GO.GetComponent<Shape>().nameGO;
                foreach (GameObject item in PastaFeatures._shapes)
                {
                    if (item.GetComponent<Shape>().nameGO == GO.GetComponent<Shape>().nameGO)
                    {
                        pastaFeatures.shape = item;
                    }
                }
                gameObject.SetActive(false);
                break;
            case PickerType.Flour:
                GameObject flourButton = GameObject.FindGameObjectWithTag("flour").transform.GetChild(1).gameObject;
                flourButton.GetComponent<Text>().text = GO.GetComponent<FlourType>().nameGO;
                foreach (GameObject item in PastaFeatures._flours)
                {
                    if (item.GetComponent<FlourType>().nameGO == GO.GetComponent<FlourType>().nameGO)
                    {
                        pastaFeatures.flour = item;
                    }
                }
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
