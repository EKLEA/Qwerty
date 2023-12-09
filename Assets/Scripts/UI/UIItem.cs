using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Action<object> OnDraggingEvent;
    private CanvasGroup _canvasGroup;
    private Canvas _mainCanvas;
    private RectTransform _rectTransform;
    public bool isUsed;
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        isUsed = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDraggingEvent?.Invoke(this);
        if (eventData.button == PointerEventData.InputButton.Right)
            eventData.pointerDrag = null;
        else
        {
            if (GetComponentInParent<UIInventorySlot>().slot == null)
            {
                isUsed = true;
                eventData.pointerDrag = null;
            }
            else
            {
                var slotTransform = _rectTransform.parent;
                slotTransform.SetAsLastSibling();
                slotTransform.parent.SetAsLastSibling();
                _canvasGroup.blocksRaycasts = false;
                isUsed = false;
            }
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta/_mainCanvas.scaleFactor;
        
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        isUsed= true;

    }
    public bool GetItemDragState()
    {
        return isUsed;
    }
    
}
