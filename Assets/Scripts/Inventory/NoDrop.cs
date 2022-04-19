using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.GetComponent<RectTransform>() != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragDrop>().position;
            }
        }  
    }
}
