using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeController : MonoBehaviour {
    public Color off;
    public Color pressed;
    public Color highlighted;
    public Color on;
    public Color disabled;
    public Toggle[] toggles;
    public static Toggle toggled = null;
    public static GameObject[] recipes;
    public static int index = 0;
    public bool crafting = false;
    public static GameObject GetRecipe()
    {
        return recipes[index];
    }
    // Use this for initialization
    public void GetToggles () {
        toggles = GetComponentsInChildren<Toggle>();
        if (PlayerController.current.GetComponent<Station>().crafting)
        {
            for (int i = 0; i < recipes.Length; i++)
            {
                if(recipes[i] == PlayerController.current.GetComponent<Station>().craftingGO)
                {
                    index = i;
                    toggles[i].isOn = true;
                    toggled = toggles[i];
                    return;
                }
            }
        }
        else
        {
            toggles[0].isOn = true;
            toggled = toggles[0];

        }
    }
    public void clean()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform _trans = transform.GetChild(i);
            Destroy(_trans.gameObject);
        }
        index = 0;
    }
    // Update is called once per frame
    void Update ()
    {
        crafting = PlayerController.current.GetComponent<Station>().crafting;
        for (int i = 0; i < toggles.Length; i++)
        {
            Toggle item = toggles[i];
            item.enabled = true;
            if (item.isOn && !crafting)
            {
                if (item != toggled)
                {
                    /*ColorBlock NormalColorBlock = item.colors;
                    ColorBlock colorBlock = item.colors;
                    colorBlock.pressedColor = off;
                    colorBlock.highlightedColor = off;
                    colorBlock.normalColor = off;
                    item.colors = colorBlock;
                    toggled.colors = NormalColorBlock;*/
                    toggled.isOn = false;
                    toggled = item;
                    item.interactable = true;
                }
                else
                {
                    index = i;
                    /*ColorBlock colorBlock = item.colors;
                    colorBlock.pressedColor = on;
                    colorBlock.highlightedColor = on;
                    colorBlock.normalColor = on;
                    item.colors = colorBlock;*/
                }

            }
            else if (crafting && !item.isOn)
            {
                if (item != toggled)
                {
                    /*ColorBlock colorBlock = item.colors;
                    colorBlock.disabledColor = disabled; 
                    item.colors = colorBlock;*/
                    item.interactable = false;
                }
            }
            else
            {
                /*ColorBlock NormalColorBlock = item.colors;
                ColorBlock colorBlock = item.colors;
                colorBlock.pressedColor = off;
                colorBlock.highlightedColor = off;
                colorBlock.normalColor = off;
                item.colors = colorBlock;
                toggled.colors = NormalColorBlock;*/
                item.interactable = true;
            }
            
        }
    }
}
