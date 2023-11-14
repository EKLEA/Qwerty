using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private UIInventoryScript uiScript;
    [SerializeField] public PlayerInventory playerInventory;
    [SerializeField] public PlayerUseMoment playerUseMoment;
    public InventoryWithSlots inventory => playerInventory.inventory;
    UIInventorySlot[] uiSlots;
    private void Start()
    {
        uiSlots= GetComponentsInChildren<UIInventorySlot>();
        uiScript= new UIInventoryScript(uiSlots,inventory);
        playerUseMoment.OnOpenInventoryEvent += OpenInv;
        playerUseMoment.OnCloseInventoryEvent += CloseInf;
        gameObject.SetActive(false);
        
    }
    bool chek(UIInventorySlot[] slots)
    {
        for (int i = 0; i < slots.Length; i++) 
            if (slots[i].GetItemDragState() == false)
                return false; 
        return true;

    }

    

    private void CloseInf(object obj)
    { 
        if (chek(uiSlots))
            gameObject.SetActive(false);
    }

    private void OpenInv(object obj)
    {
        gameObject.SetActive(true);
    }
}
