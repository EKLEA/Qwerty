using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private  PlayerInventory playerInv;
    public InventoryWithSlots inventory => playerInv.inventory;

    private void Start()
    {
        var uiSlots= GetComponentsInChildren<UIInventorySlot>();
        playerInv = new PlayerInventory();
        playerInv.SetUiSlots(uiSlots);
    }



}
