using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kind : MonoBehaviour
{

    public int[] families;
    public int[] oldies;
    public int[] richies;
    public int[] students;
    public int[] whiteCollars;
    public Sprite icon;
    public string nameGO;
    public Dictionary<Segmentation.CustomerSegmentation, int[]> dict = new Dictionary<Segmentation.CustomerSegmentation, int[]>();

    private void Awake()
    {
        dict.Add(Segmentation.CustomerSegmentation.families, families);
        dict.Add(Segmentation.CustomerSegmentation.oldies, oldies);
        dict.Add(Segmentation.CustomerSegmentation.richies, richies);
        dict.Add(Segmentation.CustomerSegmentation.students, students);
        dict.Add(Segmentation.CustomerSegmentation.whiteCollars, whiteCollars);
    }
}
