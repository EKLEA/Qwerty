using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIInventorySlot : UISlot, IPointerEnterHandler, IPointerExitHandler

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

        InventorySlot otherSlot= otherSlotUI.slot;
        var inventory = eventData.pointerDrag.GetComponentInParent<UIInventory>().inventory;

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

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Opisanie.Instance.gameObject.activeInHierarchy)
        {
            Opisanie.Instance.transform.position = Vector3.zero;
            Opisanie.Instance.gameObject.SetActive(false);
            Opisanie.Instance.SetText("");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var opSlotUI = eventData.pointerEnter.GetComponentInParent<UIInventorySlot>();
        
        if (!opSlotUI.slot.isEmpty)
        {
            if (opSlotUI.slot.isBlock)
                return;
            Opisanie.Instance.gameObject.SetActive(true);
            Opisanie.Instance.obj.transform.SetAsLastSibling();
            Opisanie.Instance.SetText(opSlotUI.slot.item.info.title);
        }
    }
}
