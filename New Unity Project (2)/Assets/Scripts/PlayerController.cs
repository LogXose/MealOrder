using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public GameObject Body;
    float InitialTouch = 0;
    bool faster = false;
    public float SPEED = 12.5f;
    public GameObject inventory;
    public GameObject RecipeBook;
    public GameObject charInventory;
    public static GameObject current;
    public bool inventoryOpen = false;
    public GameObject marketInventory;
    public bool marketInventoryOpen = false;
    public GameObject Recipe;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = Body.GetComponent<Animator>();
        inventory.SetActive(false);
        marketInventory.SetActive(false);
        PlayerSlot.playerInventory = charInventory;
    }

    void Update()
    {
        Body.transform.localPosition = Vector3.zero;
        if (Input.GetMouseButtonDown(0))
        {
            if (!inventoryOpen && !marketInventoryOpen && !OrdersMenuController.open)
            {
                if (Time.time < InitialTouch + 0.5f)
                {
                    agent.speed = SPEED * 2;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    faster = true;
                    if (Physics.Raycast(ray, out hit))
                    {
                        agent.SetDestination(hit.point);
                    }

                }
                else
                {
                    agent.speed = SPEED;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    faster = false;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.CompareTag("station"))
                        {
                            if (Vector3.Magnitude(hit.point - transform.position) < 10)
                            {
                                inventoryOpen = true;
                                inventory.SetActive(true);
                                inventory.transform.position = Input.mousePosition;
                                current = hit.transform.gameObject;
                                RecipeController.recipes = hit.transform.GetComponent<Station>().Outputs;
                                foreach (GameObject item in hit.transform.GetComponent<Station>().Outputs)
                                {
                                    GameObject recipe = Instantiate(Recipe, RecipeBook.transform.GetChild(1).GetChild(0));
                                    Image image = recipe.transform.GetChild(0).GetComponent<Image>();
                                    Text text = recipe.transform.GetChild(1).GetComponent<Text>();
                                    text.text = item.name.ToUpper();
                                    if (item.GetComponent<Material>())
                                    {
                                        image.sprite = item.GetComponent<Material>().image;
                                    }
                                    else if(item.GetComponent<Meal>())
                                    {
                                        image.sprite = item.GetComponent<Meal>().image;
                                    }
                                }
                                GameObject.FindObjectOfType<RecipeController>().GetToggles();
                            }
                        }
                        else if (hit.transform.parent.CompareTag("station"))
                        {
                            if (Vector3.Magnitude(hit.point - transform.position) < 10)
                            {
                                inventoryOpen = true;
                                inventory.SetActive(true);
                                inventory.transform.position = Input.mousePosition;
                                current = hit.transform.parent.gameObject;
                                RecipeController.recipes = hit.transform.parent.GetComponent<Station>().Outputs;
                                foreach (GameObject item in hit.transform.parent.GetComponent<Station>().Outputs)
                                {
                                    GameObject recipe = Instantiate(Recipe, RecipeBook.transform.GetChild(1).GetChild(0));
                                    Image image = recipe.transform.GetChild(0).GetComponent<Image>();
                                    Text text = recipe.transform.GetChild(1).GetComponent<Text>();
                                    text.text = item.name.ToUpper();
                                    if (item.GetComponent<Material>())
                                    {
                                        image.sprite = item.GetComponent<Material>().image;
                                    }
                                    else if (item.GetComponent<Meal>())
                                    {
                                        image.sprite = item.GetComponent<Meal>().image;
                                    }
                                }
                                GameObject.FindObjectOfType<RecipeController>().GetToggles();
                            }
                        }
                        else if (hit.transform.CompareTag("market"))
                        {
                            if (Vector3.Magnitude(hit.point - transform.position) < 30)
                            {
                                marketInventoryOpen = true;
                                marketInventory.SetActive(true);
                                marketInventory.transform.position = 
                                    new Vector3(Screen.width/2-marketInventory.GetComponent<RectTransform>().rect.width
                                    ,Screen.height/2- marketInventory.GetComponent<RectTransform>().rect.y);
                                current = hit.transform.gameObject;
                            }
                        }
                        else if (hit.transform.CompareTag("delivery"))
                        {
                            if (Vector3.Magnitude(hit.point - transform.position) < 30)
                            {
                                OrdersMenuController.open = true;
                                OrdersMenuController.atOrderStation = true;
                            }
                        }
                        agent.SetDestination(hit.point);
                    }
                }
                InitialTouch = Time.time;
            }
            else
            {
                GraphicRaycaster gr = inventory.GetComponent<GraphicRaycaster>();
                PointerEventData ped = new PointerEventData(null);
                ped.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                gr.Raycast(ped, results);
                GraphicRaycaster gr2 = marketInventory.GetComponent<GraphicRaycaster>();
                PointerEventData ped2 = new PointerEventData(null);
                ped2.position = Input.mousePosition;
                List<RaycastResult> results2 = new List<RaycastResult>();
                gr2.Raycast(ped2, results2);
                GraphicRaycaster gr3 = charInventory.GetComponent<GraphicRaycaster>();
                PointerEventData ped3 = new PointerEventData(null);
                ped3.position = Input.mousePosition;
                List<RaycastResult> results3 = new List<RaycastResult>();
                gr3.Raycast(ped3, results3);
                Debug.Log("at player=" + results3.Count);

                if (results.Count > 0)
                {
                    foreach (var item in results)
                    {
                        //menu station show

                    }
                }
                else if (results2.Count > 0)
                {

                }
                else if (results3.Count > 0)
                {

                }
                else
                {
                    if (inventoryOpen)
                    {
                        ImageCreator.Closure();
                    }
                    RecipeBook.transform.GetChild(1).GetChild(0).GetComponent<RecipeController>().clean();
                    inventoryOpen = false;
                    marketInventoryOpen = false;
                    inventory.SetActive(false);
                    marketInventory.SetActive(false);
                    /*OrdersMenuController.open = false;
                    OrdersMenuController.atStation = false;*/
                }
            }
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            if (faster) animator.SetFloat("Speed_f", 30);
            else animator.SetFloat("Speed_f", 3);
        }
        else
        {
            animator.SetFloat("Speed_f", 0);
        }   
    }

}
