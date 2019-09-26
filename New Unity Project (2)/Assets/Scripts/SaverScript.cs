using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaverScript : MonoBehaviour {

    public static Sprite Logo;
    public static List<Sprite> sprites;
    [SerializeField] List<Sprite> _sprites;
    [SerializeField]List<Region> regions = new List<Region>();
    [SerializeField] GameObject icon;
	// Use this for initialization
	void Start () {
        sprites = _sprites;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRestourantName(string name)
    {
        PlayerController.RestaurantName = name;
        icon.transform.GetChild(1).GetComponent<Logo>().setTextFromStatic();
    }

    public void SetRegion(string name)
    {
        foreach (var item in regions)
        {
            if(item.name == name)
            {
                PlayerController._Region = item.name;
            }
        }
    }

    public void setLogoIfNotPicked()
    {
       /* if(PlayerController.RestaurantLogo == "")
        PlayerController.RestaurantLogo = icon.transform.GetChild(1).gameObject.name;*/
    }
    [SerializeField] GameObject popup;
    public void CheckLackings()
    {
        if(PlayerController._Region == "")
        {
            popup.SetActive(true);
            popup.transform.GetChild(4).GetComponent<Text>().text = "Region is not selected!";
            Debug.Log("region secmemis");
            return;
        }if(PlayerController.RestaurantLogo == "")
        {
            popup.SetActive(true);
            popup.transform.GetChild(4).GetComponent<Text>().text = "Logo is not picked!";
            Debug.Log("logo secmemis");
            return;
        }if (PlayerController.RestaurantName == "")
        {
            popup.SetActive(true);
            popup.transform.GetChild(4).GetComponent<Text>().text = "Name is not typed!";
            Debug.Log("isim yazmamis");
            return;
        }
        GetComponent<SceneTransition>().PerformTransition();
    }
}
