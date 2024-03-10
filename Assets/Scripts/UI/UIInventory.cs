using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [HideInInspector] public InventoryWithSlots inventory;
    [HideInInspector] public UIInventorySlot[] slots;
   

    public void SetupInvntoryUI(InventoryWithSlots inventory, UIInventorySlot[] slots)
    {
        var allSlots = inventory.GetAllSlots();
        
        for (int i = 0; i < allSlots.Length; i++)
        {
            var slot = allSlots[i];
            var uiSlot = slots[i];
            uiSlot.SetSlot(slot); 
            uiSlot.Refresh();
        }
    }
}
