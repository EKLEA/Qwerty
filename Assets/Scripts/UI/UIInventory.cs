using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [HideInInspector] public InventoryWithSlots inventory;
    [HideInInspector] public UIInventorySlot[] slots=> GetComponentsInChildren<UIInventorySlot>(includeInactive:true);
    public void SetupInvntoryUI(InventoryWithSlots inventory)
        
    {
        this.inventory = inventory;
        var allSlots = inventory.GetAllSlots();
        
        for (int i = 0; i < allSlots.Length; i++)
        {
            var slot = allSlots[i];
            var uiSlot = slots[i];
            uiSlot.SetSlot(slot); 
            uiSlot.Refresh();
        }
        inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
    }

    private void OnInventoryStateChanged(object obj)
    {
        
        foreach (var slot in slots)
            slot.Refresh();
    }
}
