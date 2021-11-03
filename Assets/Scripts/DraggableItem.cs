using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform myRectTransform;
    CanvasGroup myCanvasGroup;
    
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();   
        myCanvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        myRectTransform.anchoredPosition += eventData.delta;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {       
        myCanvasGroup.blocksRaycasts = false;
        myCanvasGroup.alpha = .7f;
    }


    public void OnEndDrag(PointerEventData eventData)
    {        
        Debug.Log("OnEndDrag");
        myCanvasGroup.blocksRaycasts = true;
        myCanvasGroup.alpha = 1f;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
