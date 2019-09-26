using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public static int questNo = 0;
    [SerializeField] GameObject[] _quests;
    GameObject[][] questInQuest;
    bool open = true;
    [SerializeField] GameObject[] quest0;
    [SerializeField] GameObject[] quest1;
    [SerializeField] GameObject[] quest2;
    [SerializeField] GameObject[] quest3;
    [SerializeField] GameObject[] quest4;
    [SerializeField] GameObject[] quest5;
    [SerializeField] GameObject[] quest6;
    [SerializeField] GameObject[] quest7;
    public static Dictionary<int, bool> boolList;
    public static bool refreshed = false;

    private void Start()
    {
        RefreshBooleans();
        InitializeQuest();
        SetLevelNoText();
        AccomplishQuests();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl))
        {
            CloseAndOpen();
        }*/
        AccomplishQuests();
    }

    public void CloseAndOpen()
    {
        if (open)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            open = false;
            return;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            SetLevelNoText();
            open = true;
            return;
        }
    }
    public static bool TutorialRequest = true;
    void AccomplishQuests()
    {
        if(questNo == 0)
        {
            if (closeButon) BlinkAnimation(closeButon,true);
            HideNextButton();
            if(PlayerController.RestaurantName != "" && !boolList[0])
            {
                boolList[0] = true;
                quest0[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
            }
            if (PlayerController.RestaurantLogo != "" && !boolList[1])
            {
                boolList[1] = true;
                quest0[1].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
                Debug.Log(PlayerController.RestaurantLogo);
            }
            if (PlayerController._Region != "" && !boolList[2])
            {
                boolList[2] = true;
                quest0[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
            }
            if (boolList[0] && boolList[1] && boolList[2])
            {
                ReleaseNextButton();
                BlinkAnimation(nextButton,true);
            }
        }else if(questNo == 1)
        {
            SetCanvasOne(closeButon);
            HideNextButton();
            if (SceneManager.GetActiveScene().name == "RestoranDefinementPage" && GameObject.Find("Start")) BlinkAnimation(GameObject.Find("Start"),false);
            if (!boolList[0] && GameObject.Find("Tech Page")) BlinkAnimation(GameObject.Find("Tech Page"),false);
            if (SceneManager.GetActiveScene().name == "TechnologyTree" && !boolList[0])
            {
                boolList[0] = true;
                quest1[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            }
            if(TechnologuTree.activateds.Contains("Machine Research") && !boolList[1])
            {
                boolList[1] = true;
                quest1[1].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
            }
            if (TechnologuTree.activateds.Contains("Basic Pasta Tech") && !boolList[2])
            {
                boolList[2] = true;
                quest1[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
            }
            if (TechnologuTree.activateds.Contains("White Basic Flour Tech") && !boolList[3])
            {
                boolList[3] = true;
                quest1[3].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
            }
            if (TechnologuTree.activateds.Contains("Noddle Tech") && !boolList[4])
            {
                boolList[4] = true;
                quest1[4].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
            }
            if (boolList[0] && boolList[1] && boolList[2] && boolList[4] && boolList[3] )
            {
                ReleaseNextButton();
            }
        }else if(questNo == 2)
        {
            HideNextButton();
            if (SceneManager.GetActiveScene().name == "TechnologyTree" && GameObject.Find("HomePage")) BlinkAnimation(GameObject.Find("HomePage"),false);
            if (SceneManager.GetActiveScene().name == "GameScene" && !boolList[0])
            {
                boolList[0] = true;
                quest2[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            }
            if (SceneManager.GetActiveScene().name == "GameScene" && GameObject.Find("Plus Icon") && boolList[0]) BlinkAnimation(GameObject.Find("Plus Icon"), false);
            if (SceneManager.GetActiveScene().name == "GameScene" && TutorialRequest)
            {
                Station[] stations = FindObjectsOfType<Station>();
                bool kneader = false;
                bool boiler = false;
                bool cutter = false;
                foreach (var item in stations)
                {
                    if (item.stationType == Station.StationType.Boiler) boiler = true;
                    else if (item.stationType == Station.StationType.DoughKneader) kneader = true;
                    else if (item.stationType == Station.StationType.DoughCutter) cutter = true;
                }
                if (kneader && !boolList[1])
                {
                    boolList[1] = true;
                    quest2[1].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
                if (boiler && !boolList[2])
                {
                    boolList[2] = true;
                    quest2[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
                if (cutter && !boolList[3])
                {
                    boolList[3] = true;
                    quest2[3].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
            }
            if (boolList[0] && boolList[1] && boolList[2] && boolList[3])
            {
                ReleaseNextButton();
            }
        }else if(questNo == 3)
        {
            HideNextButton();
            if (SceneManager.GetActiveScene().name == "GameScene" && GameObject.Find("List Page (1)")) BlinkAnimation(GameObject.Find("List Page (1)"), false);
            if (SceneManager.GetActiveScene().name == "List Page" && GameObject.Find("List Background (1)")) BlinkAnimation(GameObject.Find("List Background (1)"), false);
            if(SceneManager.GetActiveScene().name == "mealDevelopment" && !boolList[0])
            {
                boolList[0] = true;
                quest3[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            }
            if (SceneManager.GetActiveScene().name == "mealDevelopment")
            {
                PastaFeatures pasta = GameObject.FindGameObjectWithTag("PastaFeature").GetComponent<PastaFeatures>();
                if(pasta.kind && pasta.shape && pasta.flour && pasta.mealName.text != "" && pasta.mealDefinition.text != "" && !boolList[1])
                {
                    boolList[1] = true;
                    quest3[1].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
                if(PastaFeatures.sweet + PastaFeatures.sour + PastaFeatures.bitter + PastaFeatures.salty >=95 && !boolList[3])
                {
                    boolList[3] = true;
                    quest3[3].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
                if (PastaFeatures.smooth + PastaFeatures.crunchs + PastaFeatures.juicy + PastaFeatures.sticky >= 95 && !boolList[2])
                {
                    boolList[2] = true;
                    quest3[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
                if (PastaFeatures.quality + PastaFeatures.effort + PastaFeatures.quality >= 95 && !boolList[4])
                {
                    boolList[4] = true;
                    quest3[4].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
                if(pasta.pointsDict.Count > 0 && !boolList[5])
                {
                    boolList[5] = true;
                    quest3[5].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
                if (pasta.producedMeal)
                {
                    if (pasta.producedMeal.GetComponent<Meal>().price != 99.65f && !boolList[6])
                    {
                        boolList[6] = true;
                        quest3[6].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                        CloseAndOpen();
                    }
                }
                if(boolList[0] && boolList[1] && boolList[2] && boolList[4] && boolList[3] && boolList[5] && boolList[6])
                {
                    ReleaseNextButton();
                }
            }
        }else if(questNo == 4)
        {
            HideNextButton();   
            if (SceneManager.GetActiveScene().name == "mealDevelopment" && GameObject.Find("Purchase")) BlinkAnimation(GameObject.Find("Purchase"), false);
            if (SceneManager.GetActiveScene().name == "List Page" && GameObject.Find("Home Button")) BlinkAnimation(GameObject.Find("Home Button"), false);
            if (SceneManager.GetActiveScene().name == "GameScene" && !boolList[0])
            {
                boolList[0] = true;
                quest4[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            }
            if(SceneManager.GetActiveScene().name == "GameScene")
            {
                bool oliveOil = false;
                bool egg = false;
                bool flour = false;
                foreach (InventoryOfPlayer.InventorySlot item in InventoryOfPlayer.slots)
                {
                    if (item != null)
                    {
                        if (item.typeOfItem != null)
                        {
                            if (item.typeOfItem.name == "Egg" && item.count >= 200) egg = true;
                            if (item.typeOfItem.name == "Olive Oil" && item.count >= 500) oliveOil = true;
                            if (item.typeOfItem.name == "Basic White Flour" && item.count >= 1000) flour = true;
                        }
                    }
                }
                if (oliveOil && !boolList[1])
                {
                    boolList[1] = true;
                    quest4[1].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
                if (egg && !boolList[2])
                {
                    boolList[2] = true;
                    quest4[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
                if (flour && !boolList[3])
                {
                    boolList[3] = true;
                    quest4[3].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
            }
            if (boolList[0] && boolList[1] && boolList[2] && boolList[3])
            {
                ReleaseNextButton();
            }
        }else if(questNo == 5)
        {
            HideNextButton();
            if (PlayerController.current != null)
            {
                if (PlayerController.current.GetComponent<Station>())
                {
                    if (PlayerController.current.GetComponent<Station>().stationType == Station.StationType.DoughKneader && !boolList[0])
                    {
                        boolList[0] = true;
                        quest5[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                        CloseAndOpen();
                    }
                    if (PlayerController.current.GetComponent<Station>().crafting)
                    {
                        if (PlayerController.current.GetComponent<Station>().craftingGO.name == "Pasta Dough White Basic" && PlayerController.current.GetComponent<Station>().craftingCount >= 500 && !boolList[1])
                        {
                            boolList[1] = true;
                            quest5[1].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                            CloseAndOpen();
                        }
                    }
                }
            }
            bool pastaDough = false;
            bool noodles = false;
            bool mealProduced = false;
            foreach (InventoryOfPlayer.InventorySlot item in InventoryOfPlayer.slots)
            {
                if (item != null)
                {
                    if (item.typeOfItem != null)
                    {
                        if (item.typeOfItem.name == "Pasta Dough White Basic" && item.count >= 500) pastaDough = true;
                        if (item.typeOfItem.name == "Noddles White Basic" && item.count >= 500) noodles = true;
                        if (item.typeOfItem.GetComponent<Meal>()) mealProduced = true;
                    }
                }
            }
            if(pastaDough && !boolList[2])
            {
                boolList[2] = true;
                quest5[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
            }
            if (noodles && !boolList[3])
            {
                boolList[3] = true;
                quest5[3].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
            }if(mealProduced && !boolList[4])
            {
                boolList[4] = true;
                quest5[4].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                CloseAndOpen();
            }
            if(boolList[0] && boolList[1] && boolList[2] && boolList[4] && boolList[3] && boolList[5])
            {
                ReleaseNextButton();
            }
        }else if(questNo == 6)
        {
            HideNextButton();
            if(SceneManager.GetActiveScene().name == "GameScene")
            {
                if (orderUI.transform.position.x < 300 && !boolList[0])
                {
                    boolList[0] = true;
                    quest6[0].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
            }
            
            if (PlayerController.current != null)
            {
                if(PlayerController.current.CompareTag("delivery") && !boolList[1])
                {
                    boolList[1] = true;
                    quest6[1].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
            }
            if (SceneManager.GetActiveScene().name == "List Page")
            {
                if (contentUI.transform.position.y > -800 && !boolList[2])
                {
                    boolList[2] = true;
                    quest6[2].transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                    CloseAndOpen();
                }
            }
            if (boolList[0] && boolList[1] && boolList[2])// yarin duzenle burayi tutorial bitti sekmesi ekle
            {
                CloseAndOpen();
            }
        }
    }
    [SerializeField] GameObject orderUI;
    [SerializeField] GameObject contentUI;
    void SetLevelNoText()
    {
        GameObject quest = transform.GetChild(0).gameObject;
        string noText = "Quest No. ";
        noText += (questNo + 1).ToString();
        quest.transform.GetChild(2).GetComponent<Text>().text = noText;
    }

    void RefreshBooleans()
    {
        if (!refreshed)
        {
            boolList = new Dictionary<int, bool>() { { 0, false }, { 1, false }, { 2, false }, { 3, false }, { 4, false }, { 5, false }, { 6, false } };
            refreshed = true;
        }
    }
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject closeButon;
    public void ReleaseNextButton()
    {
        nextButton.SetActive(true);
        closeButon.SetActive(false);
    }

    public void HideNextButton()
    {
        nextButton.SetActive(false);
        closeButon.SetActive(true);
    }

    public void SetNextQuest()
    {
        _quests[questNo].SetActive(false);
        questNo++;
        refreshed = false;
        RefreshBooleans();
        _quests[questNo].SetActive(true);
        HideNextButton();
    }

    void InitializeQuest()
    {
        _quests[questNo].SetActive(true);
    }

    void HardOpen()
    {
        open = false;
        CloseAndOpen();
    }
    float turner = 0.2f;
    bool left = true;
    void BlinkAnimation(GameObject GO, bool inTutorial)
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy || !inTutorial)
        {
            if (!GO.GetComponent<CanvasGroup>()) GO.AddComponent<CanvasGroup>();
            if (left && turner < 1) { turner += 2 * Time.deltaTime; } else { left = false; }
            if (!left && turner > 0) { turner -= 2 * Time.deltaTime; } else { left = true; }
            GO.GetComponent<CanvasGroup>().alpha = turner;
        }
    }

    void SetCanvasOne(GameObject GO)
    {
        if (!GO.GetComponent<CanvasGroup>()) GO.AddComponent<CanvasGroup>();
        GO.GetComponent<CanvasGroup>().alpha = 1;
    }
}
