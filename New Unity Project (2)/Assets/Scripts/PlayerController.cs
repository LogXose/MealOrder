﻿using System.Collections;
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
    bool inventoryOpen = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = Body.GetComponent<Animator>();
    }

    void Update()
    {
        Body.transform.localPosition = Vector3.zero;
        if (Input.GetMouseButtonDown(0))
        {
            if (!inventoryOpen)
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
                if (results.Count > 0)
                {
                    foreach (var item in results)
                    {
                        //menu station show
                    }
                }
                else
                {
                    inventoryOpen = false;
                    inventory.SetActive(false);
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