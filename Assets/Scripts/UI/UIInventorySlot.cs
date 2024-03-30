using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIInventorySlot : UISlot

{

    [SerializeField] protected UIInventoryItem _uiInventoryItem;
    public bool GetItemDragState()
    {
        return _uiInventoryItem.GetItemDragState();
    }

    public InventorySlot slot { get; private set; }

    public void SetSlot(InventorySlot newSlot)
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

        var inventory = eventData.pointerDrag.GetComponentInParent<UIInventory>().inventory;


        if (slot.requieType != ItemTypes.Any)
        {
            
            if (slot.requieType != otherSlot.item.info.itemType)
                return;
            else
                inventory.TransitFromSlotToSlot(this, otherSlot, slot);
                
        }
        else
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
