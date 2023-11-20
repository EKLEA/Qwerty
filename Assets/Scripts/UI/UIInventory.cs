using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    private UIInventoryScript uiScript;
    [SerializeField] public PlayerInventory playerInventory;
    [SerializeField] public PlayerUseMoment playerUseMoment;
    public InventoryWithSlots inventory;
    public GameObject contextMenu;
    public GameObject inventoryUIInterface;
    UIInventorySlot[] uiSlots;
    private RectTransform contextRecTrans;
    private void Start()
    {
        inventory= playerInventory.inventory;
        uiSlots= GetComponentsInChildren<UIInventorySlot>();
        uiScript= new UIInventoryScript(uiSlots,inventory);
        contextRecTrans= contextMenu.GetComponent<RectTransform>();


        playerUseMoment.OnOpenInventoryEvent += OpenInv;
        playerUseMoment.OnOpenContextMenuEvent += OpenContex;
        playerInventory.OnInventoryUpdate += invUpdate;

        inventoryUIInterface.SetActive(false);
        contextMenu.SetActive(false);
        
    }

    private void OpenContex(bool t)
    {
        Vector2 mousePosition = Input.mousePosition;
        contextRecTrans.position = mousePosition;
        contextMenu.SetActive(true);
        
    }

    private void invUpdate(object sender)
    {
        inventory = playerInventory.inventory;
        uiScript = new UIInventoryScript(uiSlots, inventory);
    }

    bool chek(UIInventorySlot[] slots)
    {
        for (int i = 0; i < slots.Length; i++) 
            if (slots[i].GetItemDragState() == false)
                return false; 
        return true;

    }

    private void OpenInv(bool t)
    {
        if (t)
        {
            inventoryUIInterface.SetActive(true);
            playerUseMoment.OnOpenContextMenuEvent += OpenContex;
        }
        else
        {
            if (chek(uiSlots))
                inventoryUIInterface.SetActive(false);
            playerUseMoment.OnOpenContextMenuEvent -= OpenContex;
        }
        
    }
    
   
}
