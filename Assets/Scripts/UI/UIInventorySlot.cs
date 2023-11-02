using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIInventorySlot : UISlot

{
    [SerializeField] private UIInventoryItem _uiInventoryItem;
    private UIInventory _uiInventory;
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
        var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI.slot;
        var inventory = _uiInventory.inventory;

        inventory.TransitFromSlotToSlot(this, otherSlot, slot);

        Refresh();
        otherSlotUI.Refresh();
    }
    private void Refresh()
    {
        if (slot != null)
        {
            _uiInventoryItem.Refresh(slot);
        }
    }
}
