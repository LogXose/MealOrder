using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PastaFeatures : MonoBehaviour
{
    [SerializeField] GameObject cContent;

    public static List<GameObject> Kinds;
    public static List<GameObject> Shapes;
    public static List<GameObject> FlourTypes;
    public static List<GameObject> Extras;

    public GameObject critiker;

    public GameObject kind;
    public GameObject shape;
    public GameObject flour;
    public List<GameObject> pickedExtras = new List<GameObject>();

    public GameObject[] _critickers;

    public GameObject[] _kinds;
    public GameObject[] _shapes;
    public GameObject[] _flours;
    public GameObject[] _extras;
    [SerializeField] Text mealName;
    [SerializeField] Text mealDefinition;
    [SerializeField] GameObject page2;
    [SerializeField] GameObject page1;

    public int SegmentSpecsFactor = 5;
    [SerializeField] float ReelLezzetPuaniFamily = 0;
    [SerializeField] float ReelLezzetPuaniRichies = 0;
    [SerializeField] float ReelLezzetPuaniOldies = 0;
    [SerializeField] float ReelLezzetPuaniWhite = 0;
    [SerializeField] float ReelLezzetPuaniStudents = 0;

    public static int critickerTech = 0;

    public static float juicy = 0;
    public static float crunchs = 0;
    public static float smooth = 0;
    public static float sticky = 0;

    public static float sweet = 0;
    public static float sour = 0;
    public static float bitter = 0;
    public static float salty = 0;

    public static float quantative = 0;                 
    public static float effort = 0;                     
    public static float quality = 0;                    
                                                         
    public static float oldiesBoost = 0;                //boostlar ile eklenicek
    public static float studentBoost = 0;               //boostlar ile eklenicek
    public static float whiteCBoost = 0;                //boostlar ile eklenicek
    public static float familiyBoost = 0;               //boostlar ile eklenicek
    public static float richiesBoost = 0;               //boostlar ile eklenicek
                                                        //boostlar ile eklenicek
    public static float juicyBoost = 0;                 //boostlar ile eklenicek
    public static float crunchyBoost = 0;               //boostlar ile eklenicek
    public static float smoothBoost = 0;                //boostlar ile eklenicek
    public static float stickyBoost = 0;                //boostlar ile eklenicek
                                                        //boostlar ile eklenicek
    public static float sweetBoost = 0;                 //boostlar ile eklenicek
    public static float sourBoost = 0;                  //boostlar ile eklenicek
    public static float bitterBoost = 0;                //boostlar ile eklenicek
    public static float saltyBoost = 0;                 //boostlar ile eklenicek
                                                        //boostlar ile eklenicek
    public static float quantativeBoost = 0;            //boostlar ile eklenicek
    public static float effortBoost = 0;                //boostlar ile eklenicek
    public static float qualityBoost = 0;               //boostlar ile eklenicek

    public static Dictionary<Station.StationType, int> StationTimeReducer = new Dictionary<Station.StationType, int>()
    { {Station.StationType.Boiler ,0},{Station.StationType.DoughCutter ,0},{Station.StationType.DoughKneader ,0} };
    
    public void DefinitionPageCheck()
    {
        if(!kind)
        {
            Debug.Log("kind");
        }else if(shape == null)
        {
            Debug.Log("shape");
        }
        else if(flour == null)
        {
            Debug.Log("flour");
        }
        else if(mealName.text == "")
        {
            Debug.Log("name");
        }
        else if(mealDefinition.text == "")
        {
            Debug.Log("definiton");
        }
        else
        {
            page1.SetActive(false);
            page2.SetActive(true);
        }
    }

    public void ManageCrittickers()
    {
        for (int i = 0; i < critickerTech+1; i++)
        {
            GameObject item = cContent.transform.GetChild(i).gameObject;
            item.transform.GetChild(2).gameObject.SetActive(false);
            item.GetComponent<Button>().enabled = true;
            item.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
        for (int i = critickerTech+1; i < 6; i++)
        {
            GameObject item = cContent.transform.GetChild(i).gameObject;
            item.transform.GetChild(2).gameObject.SetActive(true);
            item.GetComponent<Button>().enabled = false;
            item.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            item.transform.GetChild(1).GetComponent<Button>().enabled = false;
        }
    }

    public void PickCritiker(int i)
    {
        critiker = _critickers[i];
        Debug.Log(critiker);
    }

    public void PointEvaluation()
    {
        int[] familyID = kind.GetComponent<Kind>().families;
        int[] oldiesID = kind.GetComponent<Kind>().oldies;
        int[] richID = kind.GetComponent<Kind>().richies;
        int[] stuID = kind.GetComponent<Kind>().students;
        int[] whiteID = kind.GetComponent<Kind>().whiteCollars;
        List<int[]> list = new List<int[]> { familyID, oldiesID, richID, stuID, whiteID };
        List<float> listOfReels = new List<float> { ReelLezzetPuaniFamily,ReelLezzetPuaniOldies,ReelLezzetPuaniRichies,ReelLezzetPuaniStudents,
        ReelLezzetPuaniWhite};
        List<float> boostListForSegments = new List<float> { familiyBoost, oldiesBoost, richiesBoost, studentBoost, whiteCBoost };
        int counter = 0;
        foreach (int[] item in list)
        {
            float xSweet = Mathf.Abs(sweet - item[0]) / 50;
            float sweetBooster = (100 - sweetBoost) / 100;
            xSweet *= sweetBooster;
            float ySweet = Mathf.Exp(2 * Mathf.Log(xSweet)) * 25;
            float xSour = Mathf.Abs(sour - item[1]) / 50;
            xSour *= (100 - sourBoost) / 100;
            float ySour = Mathf.Exp(2 * Mathf.Log(xSour)) * 25;
            float xBitter = Mathf.Abs(bitter - item[2]) / 50;
            xBitter *= (100 - bitterBoost) / 100;
            float yBitter = Mathf.Exp(2 * Mathf.Log(xBitter)) * 25;
            float xSalty = Mathf.Abs(salty - item[3]) / 50;
            xSalty *= (100 - saltyBoost) / 100;
            float ySalty = Mathf.Exp(2 * Mathf.Log(xSalty)) * 25;
            float alfaProfile = 100 - (yBitter + ySalty + ySour + ySweet) / 4;

            float xJuicy = Mathf.Abs(juicy - item[4]) / 50;
            xJuicy *= (100 - juicyBoost) / 100;
            float yJuicy = Mathf.Exp(2 * Mathf.Log(xJuicy)) * 25;
            float xCrunchy = Mathf.Abs(sour - item[5]) / 50;
            xCrunchy *= (100 - crunchyBoost) / 100;
            float yCrunchy = Mathf.Exp(2 * Mathf.Log(xCrunchy)) * 25;
            float xSmooth = Mathf.Abs(smooth - item[6]) / 50;
            xSmooth *= (100 - smoothBoost) / 100;
            float ySmooth = Mathf.Exp(2 * Mathf.Log(xSmooth)) * 25;
            float xSticky = Mathf.Abs(sticky - item[7]) / 50;
            xSticky *= (100 - stickyBoost) / 100;
            float ySticky = Mathf.Exp(2 * Mathf.Log(xSticky)) * 25;
            float alfaTexture = 100 - (yJuicy + yCrunchy + ySmooth + ySticky) / 4;

            float xQuantative = (quantative - item[8]) / 50;
            float yQuantative = 0;
            if (xQuantative >= 0) { yQuantative = -1 * SegmentSpecsFactor * xQuantative; }
            else { yQuantative = Mathf.Exp(2 * Mathf.Log(-1 * xQuantative)) * 25; }
            float xEffort = (effort - item[9]) / 50;
            float yEffort = 0;
            if (xEffort >= 0) { yEffort = -1 * SegmentSpecsFactor * xEffort; }
            else { yEffort = Mathf.Exp(2 * Mathf.Log(-1 * xEffort)) * 25; }
            float xQuality = (quality - item[10]) / 50;
            float yQuality = 0;
            if (xQuality >= 0) { yQuality = -1 * SegmentSpecsFactor * xQuality; }
            else { yQuality = Mathf.Exp(2 * Mathf.Log(-1 * xQuality)) * 25; }
            float alfaSegment = 100 - (yQuality + yQuantative + yEffort) / 3;
            if (alfaSegment > 100) { alfaSegment = 100; }
            else if (alfaSegment < 0) { alfaSegment = 0; }

            float shapeFactor = 0;
            for (int i = 0; i < shape.GetComponent<Shape>().affectedKinds.Length; i++)
            {
                if (shape.GetComponent<Shape>().affectedKinds[i] == kind)
                {
                    shapeFactor = shape.GetComponent<Shape>().effectRate[i] / 100.0f + 1;
                }
            }
            float flourFactor = flour.GetComponent<FlourType>().effectRate[counter] / 100.0f + 1;
            float boostFactor = boostListForSegments[counter] / 100.0f + 1;
            listOfReels[counter] = (alfaProfile + alfaTexture) * alfaSegment * boostFactor * shapeFactor * flourFactor / 2000;
            counter++;
        }   
    }

    /*public static List<GameObject> KindsClosed;
    public static List<GameObject> ShapesClosed;
    public static List<GameObject> FlourTypesClosed;
    public static List<GameObject> ExtrasClosed;

    [SerializeField] List<GameObject> KindsLoc;
    [SerializeField] List<GameObject> ShapesLoc;
    [SerializeField] List<GameObject> FlourTypesLoc;
    [SerializeField] List<GameObject> ExtrasLoc;

    [SerializeField] List<GameObject> KindsClosedLoc;
    [SerializeField] List<GameObject> ShapesClosedLoc;
    [SerializeField] List<GameObject> FlourTypesClosedLoc;
    [SerializeField] List<GameObject> ExtrasClosedLoc;

    private void Awake()
    {
        Kinds = KindsLoc;
        Shapes = ShapesLoc;
        FlourTypes = FlourTypesLoc;
        Extras = ExtrasLoc;

        KindsClosed = KindsClosedLoc;
        ShapesClosed = ShapesClosedLoc;
        FlourTypesClosed = FlourTypesClosedLoc;
        ExtrasClosed = ExtrasClosedLoc;
    }*/

}
