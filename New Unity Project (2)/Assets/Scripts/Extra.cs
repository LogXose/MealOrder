using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extra : MonoBehaviour {

    public Sprite icon;
    public string nameGO;
    public int price;
    public GameObject inputIntregident;

    public float oldiesBoost = 0;
    public float studentBoost = 0;
    public float whiteCBoost = 0;
    public float familiyBoost = 0;
    public float richiesBoost = 0;

    public float juicyBoost = 0;
    public float crunchyBoost = 0;
    public float smoothBoost = 0;
    public float stickyBoost = 0;

    public float sweetBoost = 0;
    public float sourBoost = 0;
    public float bitterBoost = 0;
    public float saltyBoost = 0;

    public void Add()
    {
        PastaFeatures.oldiesBoost += oldiesBoost;
        PastaFeatures.studentBoost += studentBoost;
        PastaFeatures.whiteCBoost += whiteCBoost;
        PastaFeatures.richiesBoost += richiesBoost;
        PastaFeatures.familiyBoost += familiyBoost;

        PastaFeatures.juicyBoost += juicyBoost;
        PastaFeatures.crunchyBoost += crunchyBoost;
        PastaFeatures.smoothBoost += smoothBoost;
        PastaFeatures.stickyBoost += stickyBoost;

        PastaFeatures.sweetBoost += sweetBoost;
        PastaFeatures.sourBoost += sourBoost;
        PastaFeatures.saltyBoost += saltyBoost;
        PastaFeatures.bitterBoost += bitterBoost;
    }

    public void Delete()
    {
        PastaFeatures.oldiesBoost -= oldiesBoost;
        PastaFeatures.studentBoost -= studentBoost;
        PastaFeatures.whiteCBoost -= whiteCBoost;
        PastaFeatures.richiesBoost -= richiesBoost;
        PastaFeatures.familiyBoost -= familiyBoost;

        PastaFeatures.juicyBoost -= juicyBoost;
        PastaFeatures.crunchyBoost -= crunchyBoost;
        PastaFeatures.smoothBoost -= smoothBoost;
        PastaFeatures.stickyBoost -= stickyBoost;

        PastaFeatures.sweetBoost -= sweetBoost;
        PastaFeatures.sourBoost -= sourBoost;
        PastaFeatures.saltyBoost -= saltyBoost;
        PastaFeatures.bitterBoost -= bitterBoost;
    }

}
