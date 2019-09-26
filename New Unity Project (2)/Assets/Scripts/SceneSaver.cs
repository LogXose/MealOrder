﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SceneSaver : MonoBehaviour {
    public static Dictionary<int,creatingStation> savedStations = new Dictionary<int,creatingStation>();
    public static List<creatingStation> notFinished = new List<creatingStation>();
    public static List<GameObject> stations = new List<GameObject>();


    bool debug = false;
    public struct creatingStation
    {
        public Transform stationGO;
        public int index;
        public Station.InventorySlot[] inventorySlots;
        public bool crafting;
        public float leftTime;
        public int quantaty;
        public GameObject creating;
    }

    private void Update()
    {
        Application.targetFrameRate = 300;
    }

    public void saveScene()
    {
        #region DebugMod
        GameObject[] stations = GameObject.FindGameObjectsWithTag("station");
        Debug.Log(stations.Length);
        #endregion
        foreach (var item in stations)
        {
            Debug.Log(item.name);
            Station station = item.GetComponent<Station>();
            creatingStation cS = new creatingStation { stationGO = station.transform, index = station.stationIndex, inventorySlots = station.inventory, crafting = false };
            if(station.Counter > 0)
            {
                Debug.Log("craftingOne");
                cS.crafting = true;
                cS.leftTime = station.Counter;
                cS.quantaty = station.creatingQuantaty;
                cS.creating = station.creatingOutput;
            }
            notFinished.Add(cS);
            savedStations.Add(cS.index, cS);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            NavMeshSurface navMeshSurface = GameObject.FindGameObjectWithTag("NavMesh").GetComponent<NavMeshSurface>();
            navMeshSurface.BuildNavMesh();
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("station");
            if(savedStations.Count > 0)
            {
                foreach (var item in gameObjects)
                {
                    Station station = item.GetComponent<Station>();
                    creatingStation cs;
                    if (savedStations.TryGetValue(station.stationIndex, out cs))
                    {
                        station.inventory = cs.inventorySlots;
                        if (cs.crafting)
                        {
                            Station.ieStruct ieStruct = new Station.ieStruct { Output = cs.creating, quant = cs.quantaty, time = cs.leftTime };
                            station.Create(ieStruct);
                        }
                    }
                }
            }
            savedStations.Clear();
        }
    }
}
