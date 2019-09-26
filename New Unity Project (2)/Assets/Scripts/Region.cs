using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Region : MonoBehaviour {
    public float family;
    public float old;
    public float whiteCollars;
    public float student;
    public float richies;
    public List<float> vs = new List<float>();
    public Color oldColor;
    public Color activeColor;
    public bool active = false;
    public Region()
    {
        vs.Add(family);
        vs.Add(old);
        vs.Add(whiteCollars);
        vs.Add(student);
        vs.Add(richies);
    }

    public Segmentation.CustomerSegmentation segmentation(float chanceNumber)
    {
        float total = family;
        if(total > chanceNumber)
        {
            return Segmentation.CustomerSegmentation.families;
        }
        total += old;
        if (total > chanceNumber)
        {
            return Segmentation.CustomerSegmentation.oldies;
        }
        total += whiteCollars;
        if (total > chanceNumber)
        {
            return Segmentation.CustomerSegmentation.whiteCollars;
        }
        total += student;
        if (total > chanceNumber)
        {
            return Segmentation.CustomerSegmentation.students;
        }
        return Segmentation.CustomerSegmentation.richies;
    }

    public void changeColor()
    {
        PlayerController._Region = name;
        GetComponent<Image>().color = activeColor;
        active = true;
        for (int i = 1; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) != transform)
            {
                transform.parent.GetChild(i).GetComponent<Image>().color = transform.parent.GetChild(i).GetComponent<Region>().oldColor;
                transform.parent.GetChild(i).GetComponent<Region>().active = false;
            }
        }
    }

    public void SaveFromDestroy()
    {
    }
}
