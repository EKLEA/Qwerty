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
    
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();

    }
   
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnDraggingEvent?.Invoke(this);
        
        if (eventData.button == PointerEventData.InputButton.Right)
            eventData.pointerDrag = null;
        else
        {
            PlayerController.Instance.playerStateList.isDraging = true;
            if (GetComponentInParent<UIInventorySlot>().slot == null||GetComponentInParent<UIInventorySlot>().slot.item.info.itemType == ItemTypes.CraftComponents|| GetComponentInParent<UIInventorySlot>().slot.isBlock)
            {
                
                eventData.pointerDrag = null;
            }
            else
            {
                var slotTransform = _rectTransform.parent;
                if(slotTransform.GetComponentInParent<GridLayoutGroup>().isActiveAndEnabled==false)
                    slotTransform.SetAsLastSibling();
                slotTransform.parent.SetAsLastSibling();
                slotTransform.parent.parent. SetAsLastSibling();
                _canvasGroup.blocksRaycasts = false;
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
        PlayerController.Instance.playerStateList.isDraging = false;

    }
    
}
