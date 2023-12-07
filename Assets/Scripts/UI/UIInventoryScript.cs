using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryScript
{
    private UIInventorySlot[] _uiSlots;
    private UIInventorySlot[] _uiActSlots;
    public InventoryWithSlots _inventory { get; }
    public InventoryWithSlots _UIInventory { get; }
    public InventoryWithSlots _actSlotsInventory { get; }


    public UIInventoryScript(UIInventorySlot[] uiSlots, InventoryWithSlots inventory, UIInventorySlot[] uiActSlots, InventoryWithSlots actSlotsInventory)
    {
        _uiActSlots = uiActSlots;
        _UIInventory = actSlotsInventory;
        _uiSlots = uiSlots;
        _inventory = inventory;

        _inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
        _UIInventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
        SetupInvntoryUI(inventory,uiSlots);
        SetupInvntoryUI(actSlotsInventory,uiActSlots);

    }

    private void SetupInvntoryUI(InventoryWithSlots inventory, UIInventorySlot[] slots)
    {
        var allSlots = inventory.GetAllSlots();
        for (int i = 0; i < allSlots.Length; i++)
        {
            var slot = allSlots[i];
            var uiSlot = slots[i];
            if (allSlots.Length > 9)
            {
                slot.requieItem = ItemTypes.NONE;
                slot.slotType = SlotTypes.Inventory;
            }
            else
            {
                slot.requieItem = ItemTypes.UsableItem;
                slot.slotType = SlotTypes.EquippedItems;
            }
                
                uiSlot.SetSlot(slot);
                uiSlot.Refresh();


            }
        
    }

    private void OnInventoryStateChanged(object obj)
    {
        foreach(var slot in _uiSlots)
            slot.Refresh();
        foreach (var slot in _uiActSlots)
            slot.Refresh();
    }
}
