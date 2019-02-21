using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kind : MonoBehaviour
{

    [SerializeField] int[] families;
    [SerializeField] int[] oldies;
    [SerializeField] int[] richies;
    [SerializeField] int[] students;
    [SerializeField] int[] whiteCollars;
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
