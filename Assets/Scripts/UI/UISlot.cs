using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler,IPointerClickHandler
{
    public event Action<UISlot,bool> OnSlotContextClickedEvent;
    public virtual void OnDrop(PointerEventData eventData)
    {
        var otherItemTransform = eventData.pointerDrag.transform;
        otherItemTransform.SetParent(transform);
        otherItemTransform.localPosition = Vector3.zero;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            OnSlotContextClickedEvent?.Invoke(this,true);
        if (eventData.button == PointerEventData.InputButton.Left)
            OnSlotContextClickedEvent?.Invoke(this, false);
    }
}
