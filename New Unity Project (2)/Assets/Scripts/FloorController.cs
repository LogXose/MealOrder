using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{

    GameObject block = null;
    private void Update()
    {
        GameObject _block = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray, Mathf.Infinity);

        foreach (var item in hits)
        {
            if (item.transform.tag == "blok")
            {
                _block = item.transform.gameObject;
                item.transform.GetComponent<Floor>().onIt = true;
                break;
            }
        }
        
        if(block != _block)
        {
            if (block != null)
            {
                block.GetComponent<Floor>().onIt = false;
            }               
        }
        block = _block;
    }
    
}
