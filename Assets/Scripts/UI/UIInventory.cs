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
    public GameObject inventoryGrid; 
    public GameObject EquippedGrid;
    private UIInventorySlot[] uiSlots;
    private UIInventorySlot[] uiActSlots;
    private void Start()
    {
        inventory= playerInventory.inventory;
        ctMenu=contextMenu.GetComponent<ContextMenu>();
        uiSlots= inventoryGrid.GetComponentsInChildren<UIInventorySlot>();
        uiActSlots = EquippedGrid.GetComponentsInChildren<UIInventorySlot>();
        for (int i = 0; i < uiSlots.Length; i++)
            uiSlots[i].OnSlotContextClickedEvent += OpenContextMenu;
        uiScript= new UIInventoryScript(uiSlots,inventory, uiActSlots, playerInventory.actItems);

        playerUseMoment.OnOpenInventoryEvent += OpenInv;
        playerInventory.OnInventoryUpdate += invUpdate;

        inventoryUIInterface.SetActive(false);
        contextMenu.SetActive(false);
    }

    private void OpenContextMenu(UISlot slot,bool b)
    {
        contextMenu.SetActive(false);
        if (b)
        {
            var c = slot;
            if (slot.GetComponentInParent<UIInventorySlot>().slot.item == null)
                return;
            else
            {
                ctMenu.OnActiveBTClickedEvent += ActiveBTClicked;
                ctMenu.OnEquipBTClickedEvent += EquipBTClicked;
                ctMenu.OnDropBTClickedEvent += DropBTClicked;
                ctMenu.slot = c;
                ctMenu.SetButtons();
                contextMenu.SetActive(true);
                var pos = new Vector3(slot.transform.position.x + 15, slot.transform.position.y - 15, slot.transform.position.z);
                contextMenu.transform.position = pos;
            }
        }
        else
            contextMenu.SetActive(false);
    }

    private void DropBTClicked(UISlot slot)
    {
        
        var slotD = slot.GetComponentInParent<UIInventorySlot>();
        slotD.slot.item.info.itemGO.GetComponent<ItemTakeDrop>().SpawnItem(slotD.slot.item);
        playerInventory.inventory.Remove(this, slotD.slot.item.info.id, slotD.slot.count);
        ctMenu.OnActiveBTClickedEvent -= ActiveBTClicked;
        ctMenu.OnEquipBTClickedEvent -= EquipBTClicked;
        ctMenu.OnDropBTClickedEvent -= DropBTClicked;
        
    }

    bool TryToAddToEquippedInv(IInventorySlot slot,bool b)
    {
        var slots = playerInventory.actItems.GetAllSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].requieItem == slot.item.info.itemType)
            {
                if (b == true)
                    playerInventory.actItems.TransitFromSlotToSlot(this, slot, slots[i]);
                
                else
                    playerInventory.actItems.TransitFromSlotToSlot(this, slots[i], slot);

                return true;
            }

        }
        return false;
    }
    private void EquipBTClicked(UISlot slot,bool b)
    {
            var slotE = slot.GetComponentInParent<UIInventorySlot>();
            TryToAddToEquippedInv(slotE.slot,b);
            ctMenu.OnActiveBTClickedEvent -= ActiveBTClicked;
            ctMenu.OnEquipBTClickedEvent -= EquipBTClicked;
            ctMenu.OnDropBTClickedEvent -= DropBTClicked;
    }

    private void ActiveBTClicked(UISlot slot)
    {

    }

    private void invUpdate(object sender)
    {
        inventory = playerInventory.inventory;
        uiScript = new UIInventoryScript(uiSlots, inventory, uiActSlots, playerInventory.actItems);
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
