using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PastaFeatures : MonoBehaviour
{
    [SerializeField] GameObject[] _critickerList;
    [SerializeField] GameObject cContent;

    public GameObject critiker;

    public GameObject kind;
    public GameObject shape;
    public GameObject flour;
    public List<GameObject> pickedExtras = new List<GameObject>();

    public GameObject[] _critickers;

    public static List<GameObject> _kinds = new List<GameObject>();
    public static List<GameObject> _shapes = new List<GameObject>();
    public static List<GameObject> _flours = new List<GameObject>();
    public static List<GameObject> _extras = new List<GameObject>();
    public GameObject[] _outputs;
    public GameObject[] _rawInputs;
    public GameObject[] _matInputs;
    public Text mealName;
    string mealNameSave;
    string mealDefSave;
    public Text mealDefinition;
    [SerializeField] GameObject page2;
    [SerializeField] GameObject page1;
    [SerializeField] GameObject page7;

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

    public Dictionary<Segmentation.CustomerSegmentation, float> pointsDict = new Dictionary<Segmentation.CustomerSegmentation, float>();
    [SerializeField] GameObject popup;
    public void DefinitionPageCheck()
    {
        if(!kind)
        {
            Debug.Log("kind");
            popup.SetActive(true);
            popup.transform.GetChild(4).GetComponent<Text>().text = "Kind is not selected!";
            return;
        }
        else if(shape == null)
        {
            Debug.Log("shape");
            popup.SetActive(true);
            popup.transform.GetChild(4).GetComponent<Text>().text = "Shape is not selected!";
            return;
        }
        else if(flour == null)
        {
            Debug.Log("flour");
            popup.SetActive(true);
            popup.transform.GetChild(4).GetComponent<Text>().text = "flour is not selected!";
            return;
        }
        else if(mealName.text == "")
        {
            Debug.Log("name");
            popup.SetActive(true);
            popup.transform.GetChild(4).GetComponent<Text>().text = "Name is not typed!";
            return;
        }
        else if(mealDefinition.text == "")
        {
            Debug.Log("definiton");
            popup.SetActive(true);
            popup.transform.GetChild(4).GetComponent<Text>().text = "Definition is not typed!";
            return;
        }
        else
        {
            mealDefSave = mealDefinition.text;
            mealNameSave = mealName.text;
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
            item.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = _critickers[i].GetComponent<Criticker>().price.ToString();
        }
        for (int i = critickerTech+1; i < 6; i++)
        {
            GameObject item = cContent.transform.GetChild(i).gameObject;
            item.transform.GetChild(2).gameObject.SetActive(true);
            item.GetComponent<Button>().enabled = false;
            item.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            item.transform.GetChild(1).GetComponent<Button>().enabled = false;
            item.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = _critickers[i].GetComponent<Criticker>().price.ToString();
        }
    }

    public void PickCritiker(int i)
    {
        critiker = Instantiate(_critickers[i]);
        InventoryOfPlayer.Money -= _critickers[i].GetComponent<Criticker>().price;
    }

    public void PointEvaluation()
    {
        int[] familyID = kind.GetComponent<Kind>().families;
        int[] oldiesID = kind.GetComponent<Kind>().oldies;
        int[] richID = kind.GetComponent<Kind>().richies;
        int[] stuID = kind.GetComponent<Kind>().students;
        int[] whiteID = kind.GetComponent<Kind>().whiteCollars;
        List<int[]> list = new List<int[]> { familyID, oldiesID, richID, stuID, whiteID };
        List<Segmentation.CustomerSegmentation> enumList = new List<Segmentation.CustomerSegmentation> { Segmentation.CustomerSegmentation.families, Segmentation.CustomerSegmentation.oldies, Segmentation.CustomerSegmentation.richies, Segmentation.CustomerSegmentation.students, Segmentation.CustomerSegmentation.whiteCollars };
        List<float> listOfReels = new List<float> { ReelLezzetPuaniFamily,ReelLezzetPuaniOldies,ReelLezzetPuaniRichies,ReelLezzetPuaniStudents,
        ReelLezzetPuaniWhite};
        List<float> boostListForSegments = new List<float> { familiyBoost, oldiesBoost, richiesBoost, studentBoost, whiteCBoost };
        int counter = 0;
        quality += qualityBoost;
        quantative += quantativeBoost;
        effort += effortBoost;
        foreach (int[] item in list)
        {
            /*float xSweet = Mathf.Abs(sweet - item[0]) / 50;
            float sweetBooster = (100 - sweetBoost) / 100;
            xSweet *= sweetBooster;
            float ySweet = Mathf.Exp(2 * Mathf.Log10(xSweet)) * 25;
            float xSour = Mathf.Abs(sour - item[1]) / 50;
            xSour *= (100 - sourBoost) / 100;
            float ySour = Mathf.Exp(2 * Mathf.Log10(xSour)) * 25;
            float xBitter = Mathf.Abs(bitter - item[2]) / 50;
            xBitter *= (100 - bitterBoost) / 100;
            float yBitter = Mathf.Exp(2 * Mathf.Log10(xBitter)) * 25;
            float xSalty = Mathf.Abs(salty - item[3]) / 50;
            xSalty *= (100 - saltyBoost) / 100;
            float ySalty = Mathf.Exp(2 * Mathf.Log10(xSalty)) * 25;
            float alfaProfile = 100 - (yBitter + ySalty + ySour + ySweet) / 4;

            float xJuicy = Mathf.Abs(juicy - item[4]) / 50;
            xJuicy *= (100 - juicyBoost) / 100;
            float yJuicy = Mathf.Exp(2 * Mathf.Log10(xJuicy)) * 25;
            float xCrunchy = Mathf.Abs(sour - item[5]) / 50;
            xCrunchy *= (100 - crunchyBoost) / 100;
            float yCrunchy = Mathf.Exp(2 * Mathf.Log10(xCrunchy)) * 25;
            float xSmooth = Mathf.Abs(smooth - item[6]) / 50;
            xSmooth *= (100 - smoothBoost) / 100;
            float ySmooth = Mathf.Exp(2 * Mathf.Log10(xSmooth)) * 25;
            float xSticky = Mathf.Abs(sticky - item[7]) / 50;
            xSticky *= (100 - stickyBoost) / 100;
            float ySticky = Mathf.Exp(2 * Mathf.Log10(xSticky)) * 25;
            float alfaTexture = 100 - (yJuicy + yCrunchy + ySmooth + ySticky) / 4;

            float xQuantative = (quantative - item[8]) / 50;
            float yQuantative = 0;
            if (xQuantative >= 0) { yQuantative = -1 * SegmentSpecsFactor * xQuantative; }
            else { yQuantative = Mathf.Exp(2 * Mathf.Log10(-1 * xQuantative)) * 25; }
            float xEffort = (effort - item[9]) / 50;
            float yEffort = 0;
            if (xEffort >= 0) { yEffort = -1 * SegmentSpecsFactor * xEffort; }
            else { yEffort = Mathf.Exp(2 * Mathf.Log10(-1 * xEffort)) * 25; }
            float xQuality = (quality - item[10]) / 50;
            float yQuality = 0;
            if (xQuality >= 0) { yQuality = -1 * SegmentSpecsFactor * xQuality; }
            else { yQuality = Mathf.Exp(2 * Mathf.Log10(-1 * xQuality)) * 25; }
            float alfaSegment = 100 - (yQuality + yQuantative + yEffort) / 3;
            if (alfaSegment > 100) { alfaSegment = 100; }
            else if (alfaSegment < 0) { alfaSegment = 0; }

            float shapeFactor = 1;
            for (int i = 0; i < shape.GetComponent<Shape>().affectedKinds.Length; i++)
            {
                if (shape.GetComponent<Shape>().affectedKinds[i] == kind)
                {
                    shapeFactor = shape.GetComponent<Shape>().effectRate[i] / 100.0f + 1;
                }
            }
            float flourFactor = flour.GetComponent<FlourType>().effectRate[counter] / 100.0f + 1;
            float boostFactor = boostListForSegments[counter] / 100.0f + 1;
            Debug.Log(boostFactor);*/
            float sweetBooster = (100 - sweetBoost) / 100;
            float sourBooster = (100 - sourBoost) / 100;
            float saltyBooster = (100 - saltyBoost) / 100;
            float bitterBooster = (100 - bitterBoost) / 100;
            float totalDifProfile = Mathf.Abs(sweet - item[0]) * sweetBooster
                + Mathf.Abs(sour - item[1]) * sourBooster + Mathf.Abs(bitter - item[2]) * bitterBooster + Mathf.Abs(salty - item[3]) * saltyBooster;
            float profilePoint = 2 - Mathf.Log10(totalDifProfile/2);
            Debug.Log("profilePoint:" + profilePoint);
            Debug.Log("sweet" + sweet);
            Debug.Log("sour" + sour);
            Debug.Log("salty" + salty);
            Debug.Log("bitter" + bitter);
            Debug.Log("totalDifProfile" + totalDifProfile);

            float juicyBooster = (100 - juicyBoost) / 100;
            float crunchyBooster = (100 - crunchyBoost) / 100;
            float smoothBooster = (100 - smoothBoost) / 100;
            float stickyBooster = (100 - stickyBoost) / 100;
            float totalDifTexture = Mathf.Abs(juicy - item[4]) * juicyBooster + Mathf.Abs(crunchs - item[5]) * crunchyBooster +
                Mathf.Abs(smooth - item[6]) * smoothBooster + Mathf.Abs(sticky - item[7]) * stickyBooster;
            float texturePoint = 2 - Mathf.Log10(totalDifTexture/2);
            Debug.Log("texturePoint:" + texturePoint);
            Debug.Log("juicy" + juicy);
            Debug.Log("crunchy" + crunchs);
            Debug.Log("smooth" + smooth);
            Debug.Log("sticky" + sticky);
            Debug.Log("totalDifTexutre" + totalDifTexture);


            float quantativeBooster = (quantativeBoost / 100) + 1;
            float effortBooster = (effortBoost / 100) + 1;
            float qualtyBooster = (quantativeBoost / 100) + 1;
            float quantResult = quantative - item[8] >= 0 ? 0.5f + 0.17f * ((quantative - item[8]) / (100 - item[8])) : 0.5f * (quantative - item[8]);
            float effortResult = effort - item[9] >= 0 ? 0.5f + 0.17f * ((effort - item[9]) / (100 - item[9])) : 0.5f * (effort - item[9]);
            float qualityResult = quality - item[10] >= 0 ? 0.5f + 0.17f * ((quality - item[10]) / (100 - item[10])) : 0.5f * (quality - item[10]);
            quantResult = quantResult * quantativeBooster <= 0.67f ? quantResult * quantativeBooster : 0.67f;
            effortResult = effortResult * effortBooster <= 0.67f ? effortResult * effortBooster : 0.67f;
            qualityResult = qualityResult * qualtyBooster <= 0.67f ? qualityResult * qualtyBooster : 0.67f;
            float segmentPoint = quantResult + effortResult + qualityResult;
            Debug.Log("segment point: " + segmentPoint);
            Debug.Log("quantative" + quantative);
            Debug.Log("effort" + effort);
            Debug.Log("quality" + quality);
            Debug.Log("qualityResult" + qualityResult);

            float netpoint = (profilePoint + texturePoint + segmentPoint) / 6.1f;
            netpoint *= 10;
            Debug.Log("netPoint:" + netpoint);

            float shapeFactor = 1;
            for (int i = 0; i < shape.GetComponent<Shape>().affectedKinds.Length; i++)
            {
                if (shape.GetComponent<Shape>().affectedKinds[i] == kind)
                {
                    shapeFactor = shape.GetComponent<Shape>().effectRate[i] / 100.0f + 1;
                }
            }
            float flourFactor = flour.GetComponent<FlourType>().effectRate[counter] / 100.0f + 1;
            float boostFactor = boostListForSegments[counter] / 100.0f + 1;
            float factorFactor = (shapeFactor + flourFactor + boostFactor) / 3;

            float finalPoint = netpoint * factorFactor;
            if (finalPoint >= 10f) finalPoint = 10; else if (finalPoint <= 0f) finalPoint = 0;
            listOfReels[counter] = finalPoint;

            //listOfReels[counter] = (alfaProfile + alfaTexture) * alfaSegment * boostFactor * shapeFactor * flourFactor / 2000;
            Debug.Log(listOfReels[counter]);
            pointsDict.Add(enumList[counter], listOfReels[counter]);
            counter++;
        }
        for (int i = 0; i < 5; i++)
        {
            Text name = _critickerList[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
            name.text = critiker.GetComponent<Criticker>().critickerNames[i] + "(" + critiker.GetComponent<Criticker>().critickerTitle[i] + ") - "
                + critiker.GetComponent<Criticker>().critickerSegmentation[i].ToString();
            Text review = _critickerList[i].transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();
            float point = 0;
            float errorChance = critiker.GetComponent<Criticker>().deviationRange;
            switch (critiker.GetComponent<Criticker>().critickerSegmentation[i])
            {
                case Segmentation.CustomerSegmentation.oldies:
                    point = listOfReels[1] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
                case Segmentation.CustomerSegmentation.students:
                    point = listOfReels[3] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
                case Segmentation.CustomerSegmentation.whiteCollars:
                    point = listOfReels[4] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
                case Segmentation.CustomerSegmentation.families:
                    point = listOfReels[0] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
                case Segmentation.CustomerSegmentation.richies:
                    point = listOfReels[2] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
            }

            if (point > 10) point = 10;
            else if (point < 0) point = 0;
            Image pointColor = _critickerList[i].transform.GetChild(2).GetComponent<Image>();
            pointColor.color = critiker.GetComponent<Criticker>().colors[Mathf.FloorToInt(point)-1];
            Text pointText = _critickerList[i].transform.GetChild(2).GetChild(0).GetComponent<Text>();
            if(point >= 10) pointText.text = point.ToString("00.0");
            else pointText.text = point.ToString("0.0");
            Image avatar = _critickerList[i].transform.GetChild(1).GetChild(1).GetComponent<Image>();
            avatar.sprite = critiker.GetComponent<Criticker>().icon[i];
        }
        floatList = listOfReels;
    }
    public void PointEvaluationWithoutRating()
    {
        int[] familyID = kind.GetComponent<Kind>().families;
        int[] oldiesID = kind.GetComponent<Kind>().oldies;
        int[] richID = kind.GetComponent<Kind>().richies;
        int[] stuID = kind.GetComponent<Kind>().students;
        int[] whiteID = kind.GetComponent<Kind>().whiteCollars;
        List<int[]> list = new List<int[]> { familyID, oldiesID, richID, stuID, whiteID };
        List<Segmentation.CustomerSegmentation> enumList = new List<Segmentation.CustomerSegmentation> { Segmentation.CustomerSegmentation.families, Segmentation.CustomerSegmentation.oldies, Segmentation.CustomerSegmentation.richies, Segmentation.CustomerSegmentation.students, Segmentation.CustomerSegmentation.whiteCollars };
        List<float> listOfReels = new List<float> { ReelLezzetPuaniFamily,ReelLezzetPuaniOldies,ReelLezzetPuaniRichies,ReelLezzetPuaniStudents,
        ReelLezzetPuaniWhite};
        List<float> boostListForSegments = new List<float> { familiyBoost, oldiesBoost, richiesBoost, studentBoost, whiteCBoost };
        int counter = 0;
        quality += qualityBoost;
        quantative += quantativeBoost;
        effort += effortBoost;
        foreach (int[] item in list)
        {
            /*float xSweet = Mathf.Abs(sweet - item[0]) / 50;
            float sweetBooster = (100 - sweetBoost) / 100;
            xSweet *= sweetBooster;
            float ySweet = Mathf.Exp(2 * Mathf.Log10(xSweet)) * 25;
            Debug.Log("xsweet:" + xSweet);
            Debug.Log("booster " + sweetBooster);
            Debug.Log("ysweet" + ySweet);
            float xSour = Mathf.Abs(sour - item[1]) / 50;
            xSour *= (100 - sourBoost) / 100;
            float ySour = Mathf.Exp(2 * Mathf.Log10(xSour)) * 25;
            float xBitter = Mathf.Abs(bitter - item[2]) / 50;
            xBitter *= (100 - bitterBoost) / 100;
            float yBitter = Mathf.Exp(2 * Mathf.Log10(xBitter)) * 25;
            float xSalty = Mathf.Abs(salty - item[3]) / 50;
            xSalty *= (100 - saltyBoost) / 100;
            float ySalty = Mathf.Exp(2 * Mathf.Log10(xSalty)) * 25;
            float alfaProfile = 100 - (yBitter + ySalty + ySour + ySweet) / 4;

            float xJuicy = Mathf.Abs(juicy - item[4]) / 50;
            xJuicy *= (100 - juicyBoost) / 100;
            float yJuicy = Mathf.Exp(2 * Mathf.Log10(xJuicy)) * 25;
            float xCrunchy = Mathf.Abs(sour - item[5]) / 50;
            xCrunchy *= (100 - crunchyBoost) / 100;
            float yCrunchy = Mathf.Exp(2 * Mathf.Log10(xCrunchy)) * 25;
            float xSmooth = Mathf.Abs(smooth - item[6]) / 50;
            xSmooth *= (100 - smoothBoost) / 100;
            float ySmooth = Mathf.Exp(2 * Mathf.Log10(xSmooth)) * 25;
            float xSticky = Mathf.Abs(sticky - item[7]) / 50;
            xSticky *= (100 - stickyBoost) / 100;
            float ySticky = Mathf.Exp(2 * Mathf.Log10(xSticky)) * 25;
            float alfaTexture = 100 - (yJuicy + yCrunchy + ySmooth + ySticky) / 4;

            float xQuantative = (quantative - item[8]) / 50;
            float yQuantative = 0;
            if (xQuantative >= 0) { yQuantative = -1 * SegmentSpecsFactor * xQuantative; }
            else { yQuantative = Mathf.Exp(2 * Mathf.Log10(-1 * xQuantative)) * 25; }
            float xEffort = (effort - item[9]) / 50;
            float yEffort = 0;
            if (xEffort >= 0) { yEffort = -1 * SegmentSpecsFactor * xEffort; }
            else { yEffort = Mathf.Exp(2 * Mathf.Log10(-1 * xEffort)) * 25; }
            float xQuality = (quality - item[10]) / 50;
            float yQuality = 0;
            if (xQuality >= 0) { yQuality = -1 * SegmentSpecsFactor * xQuality; }
            else { yQuality = Mathf.Exp(2 * Mathf.Log10(-1 * xQuality)) * 25; }
            float alfaSegment = 100 - (yQuality + yQuantative + yEffort) / 3;
            if (alfaSegment > 100) { alfaSegment = 100; }
            else if (alfaSegment < 0) { alfaSegment = 0; }*/
            float sweetBooster = (100 - sweetBoost) / 100;
            float sourBooster = (100 - sourBoost) / 100;
            float saltyBooster = (100 - saltyBoost) / 100;
            float bitterBooster = (100 - bitterBoost) / 100;
            float totalDifProfile = Mathf.Abs(sweet - item[0])*sweetBooster
                + Mathf.Abs(sour - item[1])*sourBooster + Mathf.Abs(bitter - item[2])* bitterBooster + Mathf.Abs(salty - item[3])*saltyBooster;
            float profilePoint = 2 - Mathf.Log10(totalDifProfile);

            float juicyBooster = (100 - juicyBoost) / 100;
            float crunchyBooster = (100 - crunchyBoost) / 100;
            float smoothBooster = (100 - smoothBoost) / 100;
            float stickyBooster = (100 - stickyBoost) / 100;
            float totalDifTexture = Mathf.Abs(juicy - item[4]) * juicyBooster + Mathf.Abs(crunchs - item[5]) * crunchyBooster +
                Mathf.Abs(smooth - item[6]) * smoothBooster + Mathf.Abs(sticky - item[7]) * stickyBooster;
            float texturePoint = 2 - Mathf.Log10(totalDifTexture);

            float quantativeBooster = (quantativeBoost / 100) + 1;
            float effortBooster = (effortBoost / 100)+1;
            float qualtyBooster = (quantativeBoost / 100)+1;
            float quantResult = quantative - item[8] >= 0? 0.5f + 0.17f * ((quantative - item[8])/(100 - item[8])): 0.5f * (quantative - item[8]);
            float effortResult = effort - item[9] >= 0 ? 0.5f + 0.17f * ((effort - item[9]) / (100 - item[9])) : 0.5f * (effort - item[9]);
            float qualityResult = quality - item[10] >= 0 ? 0.5f + 0.17f * ((quality - item[10]) / (100 - item[10])) : 0.5f * (quality - item[10]);
            quantResult = quantResult * quantativeBooster <= 0.67f ? quantResult * quantativeBooster : 0.67f;
            effortResult = effortResult * effortBooster <= 0.67f ? effortResult * effortBooster : 0.67f;
            qualityResult = qualityResult * qualtyBooster <= 0.67f ? quality * qualtyBooster : 0.67f;
            float segmentPoint = quantResult + effortResult + qualityResult;

            float netpoint = (profilePoint + texturePoint + segmentPoint) / 6.1f;
            netpoint *= 10;

            float shapeFactor = 1;
            for (int i = 0; i < shape.GetComponent<Shape>().affectedKinds.Length; i++)
            {
                if (shape.GetComponent<Shape>().affectedKinds[i] == kind)
                {
                    shapeFactor = shape.GetComponent<Shape>().effectRate[i] / 100.0f + 1;
                }
            }
            float flourFactor = flour.GetComponent<FlourType>().effectRate[counter] / 100.0f + 1;
            float boostFactor = boostListForSegments[counter] / 100.0f + 1;
            float factorFactor = (shapeFactor + flourFactor + boostFactor) / 3;

            float finalPoint = netpoint * factorFactor;
            if (finalPoint >= 10f) finalPoint = 10; else if (finalPoint <= 0f) finalPoint = 0;
            listOfReels[counter] = finalPoint;             //(alfaProfile + alfaTexture) * alfaSegment * boostFactor * shapeFactor * flourFactor / 2000;
            Debug.Log(listOfReels[counter]);
            pointsDict.Add(enumList[counter], listOfReels[counter]);
            counter++;
        }
        /*for (int i = 0; i < 5; i++)
        {
            Text name = _critickerList[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
            name.text = critiker.GetComponent<Criticker>().critickerNames[i] + "(" + critiker.GetComponent<Criticker>().critickerTitle[i] + ") - "
                + critiker.GetComponent<Criticker>().critickerSegmentation[i].ToString();
            Text review = _critickerList[i].transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();
            float point = 0;
            float errorChance = critiker.GetComponent<Criticker>().deviationRange;
            switch (critiker.GetComponent<Criticker>().critickerSegmentation[i])
            {
                case Segmentation.CustomerSegmentation.oldies:
                    point = listOfReels[1] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
                case Segmentation.CustomerSegmentation.students:
                    point = listOfReels[3] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
                case Segmentation.CustomerSegmentation.whiteCollars:
                    point = listOfReels[4] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
                case Segmentation.CustomerSegmentation.families:
                    point = listOfReels[0] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
                case Segmentation.CustomerSegmentation.richies:
                    point = listOfReels[2] - Random.Range(-errorChance, errorChance) * 2 / 100;
                    break;
            }

            if (point > 10) point = 10;
            else if (point < 0) point = 0;
            Image pointColor = _critickerList[i].transform.GetChild(2).GetComponent<Image>();
            pointColor.color = critiker.GetComponent<Criticker>().colors[Mathf.FloorToInt(point) - 1];
            Text pointText = _critickerList[i].transform.GetChild(2).GetChild(0).GetComponent<Text>();
            if (point >= 10) pointText.text = point.ToString("00.0");
            else pointText.text = point.ToString("0.0");
            Image avatar = _critickerList[i].transform.GetChild(1).GetChild(1).GetComponent<Image>();
            avatar.sprite = critiker.GetComponent<Criticker>().icon[i];
        }*/
        floatList = listOfReels;
        ProfilePage();
    }
    List<float> floatList;
    void averageTaker()
    {
        float toplam = 0;
        int counter = 0;
        foreach (var item in floatList)
        {
            toplam += item;
            counter++;
        }
        producedMeal.GetComponent<Meal>().realAveragePoint = toplam / counter;
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


    [SerializeField] GameObject profileIcon;
    public void ProfilePage()
    {
        page7.SetActive(true);

        page7.transform.GetChild(4).GetComponent<Text>().text = mealNameSave;
        page7.transform.GetChild(5).GetComponent<Text>().text = mealDefSave;
        GameObject[] inputs = setInputs().ToArray();
        DefineMeal(inputs);
        float estimatedCost = CostEstimater(inputs);
        producedMeal.GetComponent<Meal>().estimatedCost = estimatedCost;
        page7.transform.GetChild(8).GetComponent<Text>().text = estimatedCost.ToString(".00")+"$";
        int counter = 0;
        Transform parent_ = page7.transform.GetChild(9).GetChild(1);
        profileIcon.GetComponent<Image>().sprite = mealImage;
        profileIcon.GetComponent<Image>().color = Color.white;
        foreach (GameObject item in inputs)
        {
            GameObject slot = parent_.GetChild(counter).gameObject;
            slot.SetActive(true);
            Transform avatar = slot.transform.GetChild(0);
            if (item.GetComponent<MealMaterial>())
            {
                MealMaterial mealMaterial = item.GetComponent<MealMaterial>();
                avatar.GetChild(1).GetComponent<Image>().sprite = mealMaterial.image;
                avatar.GetChild(1).GetComponent<Image>().color = Color.white;
                avatar.GetChild(2).GetComponent<Text>().text = producedMeal.GetComponent<Meal>().InputCount[counter].ToString() + " gr";
                avatar.GetChild(3).GetComponent<Text>().text = mealMaterial.name;
            }
            else if(item.GetComponent<RawMaterial>())
            {
                RawMaterial mealMaterial = item.GetComponent<RawMaterial>();
                avatar.GetChild(1).GetComponent<Image>().sprite = mealMaterial.image;
                avatar.GetChild(1).GetComponent<Image>().color = Color.white;
                avatar.GetChild(2).GetComponent<Text>().text = producedMeal.GetComponent<Meal>().InputCount[counter].ToString() + " gr";
                avatar.GetChild(3).GetComponent<Text>().text = mealMaterial.name;
            }
            counter++;
        }
        for (int i = counter; i < 6; i++)
        {
            GameObject slot = parent_.GetChild(i).gameObject;
            slot.SetActive(false);
        }
        averageTaker();
    }

    float CostEstimater(GameObject[] inputs)
    {

        float estimatedCost = 0;
        int ctr = 0;
        foreach (GameObject item in inputs)
        {
            if (item.GetComponent<RawMaterial>())
            {
                float inputCnt = producedMeal.GetComponent<Meal>().InputCount[ctr];
                estimatedCost += item.GetComponent<RawMaterial>().Price * inputCnt;
            }
            else
            {
                GameObject[] inputsOfMaterial = item.GetComponent<MealMaterial>().Inputs;
                float inputCnt = producedMeal.GetComponent<Meal>().InputCount[ctr];
                foreach (GameObject GO in inputsOfMaterial)
                {
                    if (GO.GetComponent<RawMaterial>())
                    {
                        estimatedCost += GO.GetComponent<RawMaterial>().Price * inputCnt;
                    }
                    else
                    {
                        GameObject[] inputsOfMaterialOFMaterial = GO.GetComponent<MealMaterial>().Inputs;
                        estimatedCost += CostEstimater(inputsOfMaterialOFMaterial, inputCnt);
                    }
                }
            }
            ctr++;
        }
        return estimatedCost;
    }

    float CostEstimater(GameObject[] inputs,float count)
    {
        float estimatedCost = 0;
        int ctr = 0;
        foreach (GameObject item in inputs)
        {
            if (item.GetComponent<RawMaterial>())
            {
                estimatedCost += item.GetComponent<RawMaterial>().Price * count;
            }
            else
            {
                GameObject[] inputsOfMaterial = item.GetComponent<MealMaterial>().Inputs;
                float inputCnt = item.GetComponent<MealMaterial>().InputCount[ctr];
                foreach (GameObject GO in inputsOfMaterial)
                {
                    if (GO.GetComponent<RawMaterial>())
                    {
                        estimatedCost += GO.GetComponent<RawMaterial>().Price * inputCnt * count;
                    }
                    else
                    {
                        GameObject[] inputsOfMaterialOFMaterial = GO.GetComponent<MealMaterial>().Inputs;
                        foreach (GameObject GOO in inputsOfMaterialOFMaterial)
                        {
                            estimatedCost += CostEstimater(inputsOfMaterialOFMaterial, inputCnt);
                        }
                    }
                }
            }
            ctr++;
        }
        return estimatedCost;
    }

    List<GameObject> setInputs()
    {
        List<GameObject> locInputs = new List<GameObject>();
        int shapeInt = 0;
        int flourInt = 0;
        int kindInt = 0;

        for (int i = 0; i < _shapes.Count; i++)
        {
            if(_shapes[i] == shape)
            {
                shapeInt = i;
            }
        }

        for (int i = 0; i < _flours.Count; i++)
        {
            if (_flours[i] == flour)
            {
                flourInt = i;
            }
        }

        for (int i = 0; i < _kinds.Count; i++)
        {
            if (_kinds[i] == kind)
            {
                kindInt = i;
            }
        }

        string mealCode = shapeInt.ToString() + flourInt.ToString() + kindInt.ToString();
        if(mealCode == "000")
        {
            locInputs.Add(_rawInputs[0]);//olive oil
            locInputs.Add(_matInputs[1]);//noddle
        }
        else if (mealCode == "001")
        {
            locInputs.Add(_rawInputs[0]);//olive oil
            locInputs.Add(_matInputs[1]);//noddle
            //locInputs.Add(_rawInputs[4]);//tomato
        }
        else if (mealCode == "101")
        {
            locInputs.Add(_rawInputs[0]);//olive oil
            locInputs.Add(_matInputs[2]);//spagetti
            //locInputs.Add(_rawInputs[4]);//tomato
        }
        else if (mealCode == "100")
        {
            locInputs.Add(_rawInputs[0]);//olive oil
            locInputs.Add(_matInputs[2]);//spagetti
        }
        foreach (GameObject item in pickedExtras)
        {
            locInputs.Add(item.GetComponent<Extra>().inputIntregident);
        }
        foreach (GameObject item in kind.GetComponent<Kind>().Inputs)
        {
            locInputs.Add(item);
        }
        return locInputs;
    }

    /// <summary>
    /// yemeklerin iceriklerinin neyden ne kadar olacaginin oransal olarak hesaplayip listeler.
    /// </summary>
    /// <param name="inputs"> icerik cesitlerini gameobject olarak tutan liste.</param>
    /// <returns></returns>
    List<float> setInputCount(GameObject[] inputs)
    { 
        List<float> returnable = new List<float>();
        float total = 0;
        foreach (var item in inputs)
        {
            Debug.Log(item.name);
            if (item.GetComponent<MealMaterial>())
            {
                switch (item.GetComponent<MealMaterial>().reqForPasta)
                {
                    case MealMaterial.ReqForPasta.none:
                        break;
                    case MealMaterial.ReqForPasta.low:
                        MealMaterial mat = item.GetComponent<MealMaterial>();
                        float necessaryQuant = 0.25f * (1f + Mathf.Pow(quality, mat.qualityIndex) / 100f);
                        total += necessaryQuant;
                        returnable.Add(necessaryQuant);
                        break;
                    case MealMaterial.ReqForPasta.mid:
                        mat = item.GetComponent<MealMaterial>();
                        necessaryQuant = 0.75f * (1f + Mathf.Pow(quality, mat.qualityIndex) / 100f);
                        total += necessaryQuant;
                        returnable.Add(necessaryQuant);
                        break;
                    case MealMaterial.ReqForPasta.high:
                        mat = item.GetComponent<MealMaterial>();
                        necessaryQuant = 1.5f * (1f + Mathf.Pow(quality, mat.qualityIndex) / 100f);
                        total += necessaryQuant;
                        returnable.Add(necessaryQuant);
                        break;
                    case MealMaterial.ReqForPasta.veryHigh:
                        mat = item.GetComponent<MealMaterial>();
                        necessaryQuant = 3f * (1f + Mathf.Pow(quality, mat.qualityIndex) / 100f);
                        total += necessaryQuant;
                        returnable.Add(necessaryQuant);
                        break;
                    default:
                        break;
                }
            }
            else if (item.GetComponent<RawMaterial>())
            {
                switch (item.GetComponent<RawMaterial>().req)
                {
                    case RawMaterial.ReqForPasta.none:
                        
                        break;
                    case RawMaterial.ReqForPasta.low:
                        RawMaterial mat = item.GetComponent<RawMaterial>();
                        float necessaryQuant = 0.25f * (1f + Mathf.Pow(quality, mat.qualityIndex) / 100f);
                        total += necessaryQuant;
                        returnable.Add(necessaryQuant);
                        break;
                    case RawMaterial.ReqForPasta.mid:
                        mat = item.GetComponent<RawMaterial>();
                        necessaryQuant = 0.75f * (1f + Mathf.Pow(quality, mat.qualityIndex) / 100f);
                        total += necessaryQuant;
                        returnable.Add(necessaryQuant);
                        break;
                    case RawMaterial.ReqForPasta.high:
                        mat = item.GetComponent<RawMaterial>();
                        necessaryQuant = 1.5f * (1f + Mathf.Pow(quality, mat.qualityIndex) / 100f);
                        total += necessaryQuant;
                        returnable.Add(necessaryQuant);
                        break;
                    case RawMaterial.ReqForPasta.veryHigh:
                        mat = item.GetComponent<RawMaterial>();
                        necessaryQuant = 3f * (1f + Mathf.Pow(quality,mat.qualityIndex) / 100f);
                        total += necessaryQuant;
                        returnable.Add(necessaryQuant);
                        break;
                    default:
                        break;
                }
            }
        }
        Debug.Log("returnab;le count: " + returnable.Count);
        for (int i = 0; i < returnable.Count; i++)
        {
            returnable[i] /= total;
        }
        return returnable;
    }

    public GameObject producedMeal;

    float[] quantatyRanger(float[] vs)
    {
        float quant = quantative / 100f;
        quant = 150 + (200 * quant);
        float[] newVs = new float[vs.Length];
        for (int i = 0; i < vs.Length; i++)
        {
            newVs[i] = Mathf.Round(vs[i] * quant); 
        }
        return newVs;
    }

    void DefineMeal(GameObject[] inputList)
    {
        producedMeal = new GameObject();
        Meal meal = producedMeal.AddComponent<Meal>();
        meal.Inputs = inputList;
        meal.points = pointsDict;
        meal.InputCount = quantatyRanger( setInputCount(inputList).ToArray());
        meal.Definition = mealDefSave;
        producedMeal.name = mealNameSave;
        meal.unitTime = 8 + (effort/100f) * 12;
        meal.image = mealImage;
    }

    public void AddToList()
    {
        Station station = new Station();
        MealList.producedMeals.Add(producedMeal);
        Station.stationOutputList[Station.StationType.Boiler].Add(producedMeal);
        DontDestroyOnLoad(producedMeal);
        MaterialAssignments(producedMeal.GetComponent<Meal>().Inputs);
        Order.availableMeals.Add(producedMeal);
    }

    void MaterialAssignments(GameObject[] Inputs)
    {
        foreach (GameObject item in Inputs)
        {
            if (item.GetComponent<MealMaterial>())
            {
                bool thereIs = false;
                foreach (GameObject mat in Station.stationOutputList[item.GetComponent<MealMaterial>().stationType])
                {
                    if (mat == item)
                    {
                        thereIs = true;
                    }
                }
                if (!thereIs) if(!isInStation(item, item.GetComponent<MealMaterial>().stationType)) Station.stationOutputList[item.GetComponent<MealMaterial>().stationType].Add(item);
                foreach (GameObject mat in item.GetComponent<MealMaterial>().Inputs)
                {
                    if (mat.GetComponent<MealMaterial>())
                    {
                        MaterialAssignments(item.GetComponent<MealMaterial>().Inputs);
                    }
                    else if (mat.GetComponent<RawMaterial>())
                    {
                        if (!isInMarket(mat))
                            AddShopList(mat);
                    }
                }
            }else if (item.GetComponent<RawMaterial>())
            {
                if (!isInMarket(item))
                    AddShopList(item);
            }
        }
    }

    void AddShopList(GameObject rawMaterial)
    {
        ShopController.items.Add(rawMaterial);
        DontDestroyOnLoad(rawMaterial);
    }

    // debug mode icin gerekli sadece
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl) && producedMeal)
        {
            averageTaker();
            AddToList();
            foreach (var item in GameObject.FindGameObjectsWithTag("station"))
            {
                item.GetComponent<Station>().Outputs = Station.stationOutputList[item.GetComponent<Station>().stationType].ToArray();
            }
        }*/
    }

   bool isInMarket(GameObject GO)
    {
        foreach(var item in ShopController.items)
        {
            if (GO.name == item.name) return true;
        }
        return false;
    }

    bool isInStation(GameObject GO,Station.StationType stationType)
    {
        foreach (var item in Station.stationOutputList[stationType])
        {
            if (item.name == GO.name) return true;
        }
        return false;
    }
    public void AddPrice(string price)
    {
        producedMeal.GetComponent<Meal>().price = float.Parse(price);
    }

    string _mealCode = "000";
    
    [SerializeField] Sprite[] imageList;
    [SerializeField] GameObject iconGameobject;
    Sprite mealImage = null;
    public void setImage()
    {
        if(kind && shape && flour)
        {
            if(pickedExtras.Count == 0)
            {
                if(kind.name == "Basic Pasta")
                {
                    if(shape.name == "Noddle")
                    {
                        mealImage = imageList[4];
                    }else if(shape.name == "Sphagetti")
                    {
                        mealImage = imageList[0];
                    }
                }else if( kind.name == "Pomodoro")
                {
                    if (shape.name == "Noddle")
                    {
                        mealImage = imageList[6];
                    }
                    else if (shape.name == "Sphagetti")
                    {
                        mealImage = imageList[2];
                    }
                }
            }else
            {
                if (kind.name == "Basic Pasta")
                {
                    if (shape.name == "Noddle")
                    {
                        mealImage = imageList[5];
                    }
                    else if (shape.name == "Sphagetti")
                    {
                        mealImage = imageList[1];
                    }
                }
                else if (kind.name == "Pomodoro")
                {
                    if (shape.name == "Noddle")
                    {
                        mealImage = imageList[7];
                    }
                    else if (shape.name == "Sphagetti")
                    {
                        mealImage = imageList[3];
                    }
                }
            }
        }else if(kind && shape)
        {
            if (pickedExtras.Count == 0)
            {
                if (kind.name == "Basic Pasta")
                {
                    if (shape.name == "Noddle")
                    {
                        mealImage = imageList[4];
                    }
                    else if (shape.name == "Sphagetti")
                    {
                        mealImage = imageList[0];
                    }
                }
                else if (kind.name == "Pomodoro")
                {
                    if (shape.name == "Noddle")
                    {
                        mealImage = imageList[6];
                    }
                    else if (shape.name == "Sphagetti")
                    {
                        mealImage = imageList[2];
                    }
                }
            }
            else
            {
                if (kind.name == "Basic Pasta")
                {
                    if (shape.name == "Noddle")
                    {
                        mealImage = imageList[5];
                    }
                    else if (shape.name == "Sphagetti")
                    {
                        mealImage = imageList[1];
                    }
                }
                else if (kind.name == "Pomodoro")
                {
                    if (shape.name == "Noddle")
                    {
                        mealImage = imageList[7];
                    }
                    else if (shape.name == "Sphagetti")
                    {
                        mealImage = imageList[3];
                    }
                }
            }
        }else if (kind)
        {
            if (pickedExtras.Count > 0)
            {
                if (kind.name == "Basic Pasta")
                {
                    mealImage = imageList[5];
                }
                else if (kind.name == "Pomodoro")
                {
                    mealImage = imageList[7];
                }
            }
            else
            {
                if (kind.name == "Basic Pasta")
                {
                    mealImage = imageList[4];
                }
                else if (kind.name == "Pomodoro")
                {
                    mealImage = imageList[6];
                }
            }
        }
        else
        {
            mealImage = defaultImage;
        }
        iconGameobject.GetComponent<Image>().sprite = mealImage;
        iconGameobject.GetComponent<Image>().color = Color.white;
    }
    [SerializeField] Sprite defaultImage;
    private void Start()
    {
        if(imageList.Length >0)
        setImage();
    }
}