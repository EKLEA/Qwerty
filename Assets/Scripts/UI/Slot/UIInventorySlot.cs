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
            
            if ( (otherSlot.item.info.itemType==ItemTypes.UsableItem && (slot.requieType != otherSlot.item.info.itemType )) || 
                (otherSlot.item.info.itemType==ItemTypes.RobortParts &&(slot.requieTypePart != (otherSlot.item.info as RobotPartInfo).robotParts)) )
                return;
            else
                inventory.TransitFromSlotToSlot(this, otherSlot, slot);
                
        }
        else
            inventory.TransitFromSlotToSlot(this, otherSlot, slot);





        Refresh();
        otherSlotUI.Refresh();
        PlayerController.Instance.playerStateList.isDraging = false;
    }
    public virtual void Refresh()
    {

        if (slot != null)
        {
            if(!slot.isEmpty&& slot.requieType==ItemTypes.RobortParts)
                gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(191/255f, 234 / 255f, 255/255f);
            _uiInventoryItem.Refresh(slot);
        }
    }
}
