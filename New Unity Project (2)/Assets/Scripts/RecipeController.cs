using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeController : MonoBehaviour {
    public Color off;
    public Color on;
    public Toggle[] toggles;
    public static Toggle toggled = null;
    public static GameObject[] recipes;
    public static int index = 0;
    public static GameObject GetRecipe()
    {
        return recipes[index];
    }
    // Use this for initialization
    public void GetToggles () {
        toggles = GetComponentsInChildren<Toggle>();
        toggles[0].isOn = true;
        toggled = toggles[0];
    }
    public void clean()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform _trans = transform.GetChild(i);
            Destroy(_trans.gameObject);
        }
    }
    // Update is called once per frame
    void Update ()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            Toggle item = toggles[i];
            if (item.isOn)
            {
                if (item != toggled)
                {
                    ColorBlock NormalColorBlock = item.colors;
                    ColorBlock colorBlock = item.colors;
                    colorBlock.pressedColor = on;
                    colorBlock.highlightedColor = off;
                    item.colors = colorBlock;
                    toggled.colors = NormalColorBlock;
                    toggled.isOn = false;
                    toggled = item;
                    index = i;
                }
                else
                {
                    ColorBlock colorBlock = item.colors;
                    colorBlock.pressedColor = on;
                    colorBlock.highlightedColor = off;
                    colorBlock.normalColor = off;
                    item.colors = colorBlock;
                }
                
            }
        }
	}
}
