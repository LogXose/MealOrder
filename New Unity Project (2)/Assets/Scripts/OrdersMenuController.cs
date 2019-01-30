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
    [SerializeField] GameObject content;
	

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
            GraphicRaycaster gr3 = inventory.GetComponent<GraphicRaycaster>();
            PointerEventData ped3 = new PointerEventData(null);
            ped3.position = Input.mousePosition;
            List<RaycastResult> results3 = new List<RaycastResult>();
            gr3.Raycast(ped3, results3);
            if (results.Count == 0 && results3.Count == 0)
            {
                open = false;
                atOrderStation = false;
            }
            else
            {
                open = true;
            }
        }
        if (atOrderStation)
        {
            //inventory.SetActive(true);
            //inventory.transform.SetAsLastSibling();
        }
        else
        {
            //inventory.SetActive(false);
        }
        int _orderCount = content.transform.childCount;
        orderCount.GetComponent<Text>().text = _orderCount.ToString();
    }

}
