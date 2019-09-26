using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour {
    [SerializeField] GameObject iconContent;
    [SerializeField] GameObject prefab;
    [SerializeField] bool pickerItem = false;
	public void setText(string text)
    {
        transform.GetChild(0).GetComponent<Text>().text = text;
    }

    public void setTextFromStatic()
    {
        transform.GetChild(0).GetComponent<Text>().text = PlayerController.RestaurantName;
    }

    public void PickandClose()
    {
        PlayerController.RestaurantLogo = gameObject.name;
        if (iconContent.transform.childCount > 1) Destroy(iconContent.transform.GetChild(1).gameObject);
        GameObject logo = Instantiate(prefab, iconContent.transform);
        //logo.name.Remove(6);
        logo.GetComponent<Logo>().setTextFromStatic();
        transform.parent.parent.gameObject.SetActive(false);
    }

    private void Start()
    {
        setTextFromStatic();
        if (pickerItem) transform.GetChild(0).gameObject.SetActive(false);
    }
}
