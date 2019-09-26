using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologuTree : MonoBehaviour {
    public static List<string> activateds = new List<string>();
    [SerializeField] GameObject BudgetGO;
    public static Color activatedColor = new Color(0.439f, 0.820f, 0.439f);
    // Use this for initialization
    void Start () {


        if (activateds.Count > 0)
        {
            Debug.Log(activateds.Count);
            foreach (var item in activateds)
            {
                foreach (var jitem in GameObject.FindObjectsOfType<TechItem>())
                {
                    if(item == jitem.name)
                    {
                        jitem.changeState(1);
                        jitem.GetComponent<BasicButton>().hasActivated = true;
                    }
                }
            }
        }
	}

    private void Update()
    {
        BudgetGO.GetComponent<Text>().text = "BUDGET\n$ " + InventoryOfPlayer.Money.ToString();
    }
}
