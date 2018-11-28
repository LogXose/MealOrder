using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OrdersMenuController : MonoBehaviour {
    public static bool open = false;
    public static bool atOrderStation = false;
    public GameObject orderCount;
    public GameObject inventory;
	

	void Update () {

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
            if(results.Count == 0)
            {
                open = false;
                atOrderStation = false;
            }
        }
        if (atOrderStation)
        {
            inventory.SetActive(true);
            inventory.transform.SetAsLastSibling();
        }
        else
        {
            inventory.SetActive(false);
        }
        int _orderCount = transform.childCount - 2;
        orderCount.GetComponent<Text>().text = _orderCount.ToString();
    }

    public void Open()
    {
        open = true;
    }
}
