using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    PlayerInventory playerInventory=> PlayerController.Instance.GetComponent<PlayerInventory>();
    [HideInInspector] public InventoryWithSlots inventory;
    [HideInInspector] UIInventorySlot[] slots=> GetComponentsInChildren<UIInventorySlot>(includeInactive:true);
    public void SetupInvntoryUI(InventoryWithSlots inventory)
    {
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
