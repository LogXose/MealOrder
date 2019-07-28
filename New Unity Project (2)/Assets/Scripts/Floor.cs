using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    public UnityEngine.Material[] materials;
    public bool onIt = false;
    public bool onStation = false;
    private void Update()
    {
        if(onIt)
        {
            GetComponent<MeshRenderer>().sharedMaterial = materials[1];
        }
        else
        {
            GetComponent<MeshRenderer>().sharedMaterial = materials[0];
        }
    }
}
