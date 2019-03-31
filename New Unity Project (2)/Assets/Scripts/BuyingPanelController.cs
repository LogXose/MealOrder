using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyingPanelController : MonoBehaviour {
    public static BuyingPanelController obj = new BuyingPanelController();
    public static bool open = false;
    public static bool atBuyingPanel = false;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject content;
    public GameObject[] openedStationList;
    public static bool machineSelected = false;

    void Update()
    {
        if (open)
        {
            GetComponent<Animator>().SetBool("open", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("open", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            GraphicRaycaster gr = GetComponent<GraphicRaycaster>();
            PointerEventData ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);
            if (results.Count == 0 && !machineSelected)
            {
                open = false;
                atBuyingPanel = false;
            }else if (results.Count == 0 && machineSelected)
            {
                machineSelected = false;
            }
            else
            {
                Refresh();
                open = true;
                atBuyingPanel = true;
            }
        }
    }

    void Refresh()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        foreach (GameObject item in openedStationList)
        {
            GameObject next = Instantiate(itemPrefab, content.transform);
            Station kind = item.GetComponent<Station>();
            GameObject icon = next.transform.GetChild(1).gameObject;
            GameObject name = next.transform.GetChild(2).gameObject;
            icon.GetComponent<Image>().sprite = kind.icon;
            name.GetComponent<Text>().text = item.name;
        }
    }
}
