using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private UIInventoryScript uiScript;
    [SerializeField] public PlayerInventory playerInventory;
    public InventoryWithSlots inventory => playerInventory.inventory;

    private void Start()
    {
        var uiSlots= GetComponentsInChildren<UIInventorySlot>();
        uiScript= new UIInventoryScript(uiSlots,inventory);
    }



}
