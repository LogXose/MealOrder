using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverScript : MonoBehaviour {

    public static string RestourantName;
    public static Sprite Logo;
    public static List<Sprite> sprites;
    [SerializeField] List<Sprite> _sprites;
    [SerializeField]List<Region> regions = new List<Region>();
	// Use this for initialization
	void Start () {
        sprites = _sprites;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRestourantName(string name)
    {
        RestourantName = name;
    }

    public void SetRegion(string name)
    {
        foreach (var item in regions)
        {
            if(item.name == name)
            {
                PlayerController._Region = item;
            }
        }
    }
}
