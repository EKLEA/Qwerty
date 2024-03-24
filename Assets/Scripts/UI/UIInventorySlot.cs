using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIInventorySlot : UISlot

{
    
    [SerializeField] protected UIInventoryItem _uiInventoryItem;
    private UIInventory _uiInventory;
    public bool GetItemDragState()
    {
        return _uiInventoryItem.GetItemDragState();
    }

    private void Awake()
    {
        _uiInventory = GetComponentInParent<UIInventory>();
        
    }
    public IInventorySlot slot { get; private set; }

    public void SetSlot(IInventorySlot newSlot)
    {
        slot = newSlot;
    }
    public override void OnDrop(PointerEventData eventData)
    {
        if (slot.slotType == SlotTypes.StaticSlot)
            return;
        var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>(); 
        var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI.slot;
        var inventory = eventData.pointerDrag.GetComponent<UIInventory>().inventory;
        inventory.TransitFromSlotToSlot(this, otherSlot, slot);
            Refresh();
            otherSlotUI.Refresh();
    }
    public void Refresh()
    {
        if (slot != null)
        {
            _uiInventoryItem.Refresh(slot);
        }
    }
}
