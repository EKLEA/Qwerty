using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class UIInventory : MonoBehaviour
{
    private UIInventoryScript uiScript;
    private ContextMenu ctMenu;
    [SerializeField] public PlayerInventory playerInventory;
    [SerializeField] public PlayerUseMoment playerUseMoment;
    public InventoryWithSlots inventory;
    public GameObject contextMenu;
    public GameObject inventoryUIInterface;
    UIInventorySlot[] uiSlots;
    private void Start()
    {
        inventory= playerInventory.inventory;
        ctMenu=contextMenu.GetComponent<ContextMenu>();
        uiSlots= GetComponentsInChildren<UIInventorySlot>();
        for (int i = 0; i < uiSlots.Length; i++)
            uiSlots[i].OnSlotContextClickedEvent += OpenContextMenu;
        uiScript= new UIInventoryScript(uiSlots,inventory);

        playerUseMoment.OnOpenInventoryEvent += OpenInv;
        playerInventory.OnInventoryUpdate += invUpdate;

        inventoryUIInterface.SetActive(false);
        contextMenu.SetActive(false);
    }

    private void OpenContextMenu(UISlot slot,bool b)
    {
        if (b)
        {
            var c = slot.GetComponentInParent<UIInventorySlot>().slot;
            if (c.item == null)
                return;
            else
            {
                ctMenu.OnActiveBTClickedEvent += ActiveBTClicked;
                ctMenu.OnEquipBTClickedEvent += EquipBTClicked;
                ctMenu.OnDropBTClickedEvent += DropBTClicked;
                ctMenu.SetButtons(c.item);
                contextMenu.SetActive(true);
                var pos = new Vector3(slot.transform.position.x + 15, slot.transform.position.y - 15, slot.transform.position.z);
                contextMenu.transform.position = pos;
                if (contextMenu.activeSelf==false)
                {
                    ctMenu.OnActiveBTClickedEvent -= ActiveBTClicked;
                    ctMenu.OnEquipBTClickedEvent -= EquipBTClicked;
                    ctMenu.OnDropBTClickedEvent -= DropBTClicked;
                }
            }
        }
        else
            contextMenu.SetActive(false);
    }

    private void DropBTClicked(UISlot slot)
    {
        throw new NotImplementedException();
    }

    private void EquipBTClicked(UISlot slot)
    {
        throw new NotImplementedException();
    }

    private void ActiveBTClicked(UISlot slot)
    {
        throw new NotImplementedException();
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
            inventoryUIInterface.SetActive(true);
        else
            if (chek(uiSlots))
                inventoryUIInterface.SetActive(false);
        
    }
    
   
}
