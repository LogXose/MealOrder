// Copyright (C) 2015, 2016 ricimi - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms.

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


/// <summary>
/// Basic button class used throughout the demo.
/// </summary>
public class BasicButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public float fadeTime = 0.2f;
    public float onHoverAlpha;
    public float onClickAlpha;
    public bool close = false;
    [SerializeField] bool pickerItem = false;
    public bool hasActivated = false;
    public bool donePrecondition = false;
    public bool techItem = false;

    [Serializable] 
    public class ButtonClickedEvent : UnityEvent { }

    [SerializeField]
    private ButtonClickedEvent onClicked = new ButtonClickedEvent();

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = gameObject.AddComponent<CanvasGroup>();

    }
    private void Start()
    {
        canvasGroup.alpha = 1f;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || close)
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(Utils.FadeOut(canvasGroup, onHoverAlpha, fadeTime));
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || close)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(Utils.FadeIn(canvasGroup, 1.0f, fadeTime));
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || close)
        {
            return;
        }

        canvasGroup.alpha = onClickAlpha;

        onClicked.Invoke();

        if (pickerItem)
        {
            GameObject pickerPanel = GameObject.FindGameObjectWithTag("pickerPanel");
            pickerPanel.SendMessage("pickedOne", transform.GetChild(2).gameObject);
        }

    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || close)
        {
            return;
        }

        canvasGroup.alpha = onHoverAlpha;
    }
    bool changeState = false;
    void Update()
    {
        if (techItem)
        {

            if (!donePrecondition)
            {
                canvasGroup.alpha = 0.5f;
                onHoverAlpha = 0.5f;
                onClickAlpha = 0.5f;
                gameObject.GetComponent<Tooltip>().enable = false;
            }
            else
            {
                if (!changeState) { canvasGroup.alpha = 1; changeState = true; }
                gameObject.GetComponent<Tooltip>().enable = true;
                if (hasActivated)
                {
                    onHoverAlpha = 1f;
                    onClickAlpha = 1f;
                }
                else
                {
                    onHoverAlpha = 0.9f;
                    onClickAlpha = 0.6f;
                }
            }
        }
    }
}

