using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingNotification : MonoBehaviour {
    public float lifeTime;
    float lifeSeed;
    public float startRate = 0;
    public bool parent = true;
    Color color;
    public enum interfaceType
    {
        image,
        text
    }
    public interfaceType InterfaceType;
	// Use this for initialization
	void Start () {
        lifeSeed = lifeTime;
        if (GetComponent<Image>())
        {
            InterfaceType = interfaceType.image;
            startRate = GetComponent<Image>().color.a;
            color = GetComponent<Image>().color;
        }
        else
        {
            InterfaceType = interfaceType.text;
            startRate = GetComponent<Text>().color.a;
            color = GetComponent<Text>().color;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<FadingNotification>();
            transform.GetChild(i).gameObject.GetComponent<FadingNotification>().parent = false;
            transform.GetChild(i).gameObject.GetComponent<FadingNotification>().lifeTime = lifeTime;
        }
    }
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        switch (InterfaceType)
        {
            case interfaceType.image:
                color.a = Mathf.Lerp(0, startRate, lifeTime / lifeSeed);
                GetComponent<Image>().color = color;
                break;
            case interfaceType.text:
                color.a = Mathf.Lerp(0, startRate, lifeTime / lifeSeed);
                GetComponent<Text>().color = color;
                break;
        }
        if(parent && lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
