using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorPanel : MonoBehaviour {
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject content;
    [SerializeField] GameObject mainText;
    PastaFeatures pasta;
    private void Awake()
    {
        pasta = GameObject.FindGameObjectWithTag("PastaFeature").GetComponent<PastaFeatures>();
    }
    void Start()
    {
        for (int i = 1; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        List<GameObject> extras = PastaFeatures._extras;
        foreach (GameObject item in extras)
        {
            GameObject next = Instantiate(prefab, content.transform);
            GameObject icon = next.transform.GetChild(0).gameObject;
            GameObject text = next.transform.GetChild(1).gameObject;
            icon.GetComponent<Image>().sprite = item.GetComponent<Extra>().icon;
            text.GetComponent<Text>().text = item.GetComponent<Extra>().nameGO;
            Instantiate(item, next.transform);
        }

    }

    public void SendSelecteds()
    {
        string newText = "";
        List<GameObject> extras = new List<GameObject>();
        for (int i = 1; i < content.transform.childCount; i++)
        {
            GameObject item = content.transform.GetChild(i).gameObject;
            bool isOn = item.GetComponent<Toggle>().isOn;
            if (isOn)
            {
                Extra extra = item.transform.GetChild(3).GetComponent<Extra>();
                newText += extra.nameGO + "\t $" +extra.price+  "\r\n";
                extras.Add(item.transform.GetChild(3).gameObject);
            }
        }
        mainText.GetComponent<Text>().text = newText;
        pasta.pickedExtras = extras;
        gameObject.SetActive(false);
    }
}
