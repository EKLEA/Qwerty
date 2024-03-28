using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            if (GetComponentInParent<UIInventorySlot>().slot == null||GetComponentInParent<UIInventorySlot>().slot.item.info.itemType == ItemTypes.CraftComponents)
            {
                isUsed = true;
                eventData.pointerDrag = null;
            }
            else
            {
                var slotTransform = _rectTransform.parent;
                slotTransform.SetAsLastSibling();
                slotTransform.parent.SetAsLastSibling();
                slotTransform.parent.parent. SetAsLastSibling();
                _canvasGroup.blocksRaycasts = false;
                isUsed = false;
            }
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    { 
       if (GetComponentInParent<UIInventorySlot>().slot.slotType == SlotTypes.StaticSlot)
          return;
        if (GetComponentInParent<UIInventorySlot>().slot.item.info.itemType==ItemTypes.CraftComponents)
            return;
        if (transform.parent.GetComponentInParent<UIInventory>().inventory.isBlock==true)
            return;

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
