using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField]
    private Canvas canvas;
    public RectTransform rectTransform;
    public CanvasGroup cnvGroup;
    [SerializeField]
    private List<DragDrop> icons;
    public Vector3 position;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cnvGroup = GetComponent<CanvasGroup>();
        ChangePosition();
    }

    private void Start()
    {
        ChangePosition();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cnvGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        foreach(DragDrop element in icons)
        {
            if(element != this)
            {
                element.cnvGroup.alpha = 0.3f;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cnvGroup.blocksRaycasts = true;
        foreach (DragDrop element in icons)
        {
            if (element != this)
            {
                element.cnvGroup.alpha = 1;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(GetComponent<ItemContainerInv>().itemInfo != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragDrop>().position;

                Item it = GetComponent<ItemContainerInv>().itemInfo;
                ChangePosition(eventData.pointerDrag.GetComponent<ItemContainerInv>().itemInfo);
                eventData.pointerDrag.GetComponent<DragDrop>().ChangePosition(it);

                Inventory.instance.OnItemChanged.Invoke();
            }
            else if(GetComponent<ItemContainerInv>().itemInfo == null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragDrop>().position;
                GetComponent<ItemContainerInv>().itemInfo = eventData.pointerDrag.GetComponent<ItemContainerInv>().itemInfo;
                eventData.pointerDrag.GetComponent<ItemContainerInv>().RemoveReference();

                Inventory.instance.OnItemChanged.Invoke();
            }
            else
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragDrop>().position;
            }
        }
    }

    public void ChangePosition()
    {
        position = rectTransform.anchoredPosition;
    }

    public void ChangePosition(Item it)
    {
        GetComponent<ItemContainerInv>().itemInfo = it;
    }
}
