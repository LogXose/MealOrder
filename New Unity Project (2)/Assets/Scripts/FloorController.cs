using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{

    GameObject block = null;
    GameObject station;
    static List<GameObject> savedStations = new List<GameObject>();
    bool getIt = false;
    Floor floor;
    [SerializeField] GameObject confirmationGO;
    bool onConfirm = false;
    [SerializeField] GameObject instructions;
    private void Update()
    {
        if (BuyingPanelController.machineSelected)
        {
            GameObject _block = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray, Mathf.Infinity);
            foreach (var item in hits)
            {
                if (item.transform.tag == "blok")
                {
                    if (!item.transform.GetComponent<Floor>().onStation && !onConfirm)
                    {
                        Debug.Log("notHasStation");
                        _block = item.transform.gameObject;
                        if (!station)station = Instantiate(BuyingPanelController.selected);
                        else if (station.GetComponent<Station>().stationType != BuyingPanelController.selected.GetComponent<Station>().stationType)
                            station = Instantiate(BuyingPanelController.selected);
                        station.transform.parent = _block.transform;
                        station.transform.localPosition = new Vector3(0, 0, 5.13f);
                        item.transform.GetComponent<Floor>().onIt = true;
                        if (Input.GetMouseButtonDown(0) && getIt)
                        {
                            floor = item.transform.GetComponent<Floor>();
                            ConfirmationBalloon();
                            onConfirm = true;
                            instructions.SetActive(false);
                            /* station.transform.parent = null;
                             BuyingPanelController.machineSelected = false;
                             item.transform.GetComponent<Floor>().onStation = true;
                             NavMeshSurface navMeshSurface = GameObject.FindGameObjectWithTag("NavMesh").GetComponent<NavMeshSurface>();
                             navMeshSurface.BuildNavMesh();
                             station.GetComponent<Station>().stationIndex = Station.indexCounter;
                             Station.indexCounter++;
                             DontDestroyOnLoad(station);
                             station = null; */
                        }
                        else if (Input.GetMouseButtonDown(0))
                        {
                            getIt = true;
                            instructions.SetActive(true);
                        }
                       /*else
                        {
                            Destroy(station);
                            BuyingPanelController.machineSelected = false;
                        }*/
                        break;
                    }
                }
            }
            if (station)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    station.transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    station.transform.Rotate(new Vector3(0, 0, -90));
                }
            }
            if (block != _block)
            {
                if (block != null)
                {
                    block.GetComponent<Floor>().onIt = false;
                    if (!station)
                    {
                        //Destroy(station);
                    }
                    
                }
            }
            block = _block;
        }
        else
        {
            if(block != null)
            {
                block.GetComponent<Floor>().onIt = false;
                block = null;
            }
        }
    }
    public void Confirm()
    {
        station.transform.parent = null;
        BuyingPanelController.machineSelected = false;
        floor.onStation = true;  // item.transform.GetComponent<Floor>() => floor
        NavMeshSurface navMeshSurface = GameObject.FindGameObjectWithTag("NavMesh").GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
        station.GetComponent<Station>().stationIndex = Station.indexCounter;
        Station.indexCounter++;
        InventoryOfPlayer.Money -= station.GetComponent<Station>().price;
        DontDestroyOnLoad(station);
        station = null;
        confirmationGO.SetActive(false);
        onConfirm = false;
        getIt = false;

    }

    void ConfirmationBalloon()
    {
        confirmationGO.SetActive(true);
    }
    public void Cancel()
    {
        //do something
        confirmationGO.SetActive(false);
        onConfirm = false;
        getIt = false;
        Destroy(station);
    }
}
