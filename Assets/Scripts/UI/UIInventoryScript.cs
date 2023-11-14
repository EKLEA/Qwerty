using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryScript
{
    private UIInventorySlot[] _uiSlots;
    public InventoryWithSlots _inventory { get; }


    public UIInventoryScript(UIInventorySlot[] uiSlots,InventoryWithSlots inventory)
    {
        _uiSlots = uiSlots;
        _inventory = inventory;
        _inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
        SetupInvntoryUI(inventory);
    }

    private void SetupInvntoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        for (int i = 0; i < allSlots.Length; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }

    private void OnInventoryStateChanged(object obj)
    {
        foreach(var slot in _uiSlots)
            slot.Refresh();
    }
}
