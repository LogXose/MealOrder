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

    public GameObject[] _kinds;
    public GameObject[] _shapes;
    public GameObject[] _flours;
    public GameObject[] _extras;
    public GameObject[] _outputs;
    public GameObject[] _rawInputs;
    public GameObject[] _matInputs;
    [SerializeField] Text mealName;
    string mealNameSave;
    string mealDefSave;
    [SerializeField] Text mealDefinition;
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
        critiker = Instantiate(_critickers[i]);
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
            listOfReels[counter] = (alfaProfile + alfaTexture) * alfaSegment * boostFactor * shapeFactor * flourFactor / 2000;
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

    

    public void ProfilePage()
    {
        page7.SetActive(true);

        page7.transform.GetChild(4).GetComponent<Text>().text = mealNameSave;
        page7.transform.GetChild(5).GetComponent<Text>().text = mealDefSave;

        GameObject[] inputs = setInputs().ToArray();
        float estimatedCost = CostEstimater(inputs);
        page7.transform.GetChild(8).GetComponent<Text>().text = estimatedCost.ToString()+"$";
        int counter = 0;
        Transform parent_ = page7.transform.GetChild(9).GetChild(1);
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
                //avatar.GetChild(2).GetComponent<Text>().text = (kind.GetComponent<Kind>().inputCountBase[counter]*(1+quantative)*100).ToString();
                avatar.GetChild(3).GetComponent<Text>().text = mealMaterial.name;
            }
            else if(item.GetComponent<RawMaterial>())
            {
                RawMaterial mealMaterial = item.GetComponent<RawMaterial>();
                avatar.GetChild(1).GetComponent<Image>().sprite = mealMaterial.image;
                avatar.GetChild(1).GetComponent<Image>().color = Color.white;
                //avatar.GetChild(2).GetComponent<Text>().text = (kind.GetComponent<Kind>().inputCountBase[counter] * quantative * 100).ToString();
                avatar.GetChild(3).GetComponent<Text>().text = mealMaterial.name;
            }
            counter++;
        }
        for (int i = counter; i < 6; i++)
        {
            GameObject slot = parent_.GetChild(i).gameObject;
            slot.SetActive(false);
        }
        DefineMeal(inputs);
    }

    float CostEstimater(GameObject[] inputs)
    {

        float estimatedCost = 0;
        foreach (GameObject item in inputs)
        {
            if (item.GetComponent<RawMaterial>())
            {
                estimatedCost += item.GetComponent<RawMaterial>().Price;
            }
            else
            {
                GameObject[] inputsOfMaterial = item.GetComponent<MealMaterial>().Inputs;
                foreach (GameObject GO in inputsOfMaterial)
                {
                    if (GO.GetComponent<RawMaterial>())
                    {
                        estimatedCost += GO.GetComponent<RawMaterial>().Price;
                    }
                    else
                    {
                        GameObject[] inputsOfMaterialOFMaterial = GO.GetComponent<MealMaterial>().Inputs;
                        foreach (GameObject GOO in inputsOfMaterialOFMaterial)
                        {
                            estimatedCost += GOO.GetComponent<RawMaterial>().Price;
                        }
                    }
                }
            }
        }
        return estimatedCost;
    }

    List<GameObject> setInputs()
    {
        List<GameObject> locInputs = new List<GameObject>();
        int shapeInt = 0;
        int flourInt = 0;

        for (int i = 0; i < _shapes.Length; i++)
        {
            if(_shapes[i] == shape)
            {
                shapeInt = i;
            }
        }

        for (int i = 0; i < _flours.Length; i++)
        {
            if (_flours[i] == flour)
            {
                flourInt = i;
            }
        }

        string mealCode = shapeInt.ToString() + flourInt.ToString();
        if(mealCode == "00")
        {
            locInputs.Add(_rawInputs[0]);
            locInputs.Add(_matInputs[1]);
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

    List<GameObject> setInputCount(List<GameObject> inputs)
    {
        int ctInpuy = inputs.Count;
        List<float> returnable = new List<float>();
        for (int i = 0; i < ctInpuy; i++)
        {
            if(i == 1)
            {

            }
            else if(i == 0)
            {

            }
            else
            {

            }
        }
        return null;
    }

    [SerializeField]GameObject producedMeal;

    void DefineMeal(GameObject[] inputList)
    {
        producedMeal = new GameObject();
        Meal meal = producedMeal.AddComponent<Meal>();
        meal.Inputs = inputList;
        int cter = 0;
        meal.InputCount = new float[inputList.Length];
        foreach (var item in inputList)
        {
            meal.InputCount[cter] = 1;//TODO: hacim sistemi ayarlandiktan sonra burayi duzelt quantatye gore #5.2
            cter++;
        }
        meal.Definition = mealDefSave;
        producedMeal.name = mealNameSave;
        meal.unitTime = 20;
    }

    public void AddToList()
    {
        Station station = new Station();
        MealList.producedMeals.Add(producedMeal);
        Station.stationOutputList[Station.StationType.Boiler].Add(producedMeal);
        DontDestroyOnLoad(producedMeal);
        MaterialAssignments(producedMeal.GetComponent<Meal>().Inputs);
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
                if (!thereIs) Station.stationOutputList[item.GetComponent<MealMaterial>().stationType].Add(item);
                foreach (GameObject mat in item.GetComponent<MealMaterial>().Inputs)
                {
                    if (mat.GetComponent<MealMaterial>())
                    {
                        MaterialAssignments(item.GetComponent<MealMaterial>().Inputs);
                    }
                    else if (mat.GetComponent<RawMaterial>())
                    {
                        AddShopList(mat);
                    }
                }
            }else if (item.GetComponent<RawMaterial>())
            {
                AddShopList(item);
            }
        }
    }

    void AddShopList(GameObject rawMaterial)
    {
        ShopController.items.Add(rawMaterial);
        DontDestroyOnLoad(rawMaterial);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl) && producedMeal)
        {
            AddToList();
            foreach (var item in GameObject.FindGameObjectsWithTag("station"))
            {
                item.GetComponent<Station>().Outputs = Station.stationOutputList[item.GetComponent<Station>().stationType].ToArray();
            }
        }
    }
}