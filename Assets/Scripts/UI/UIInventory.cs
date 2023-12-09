using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        foreach (UIInventorySlot c in uiSlots)
            c.OnSlotContextClickedEvent += OpenContextMenu;
        foreach (UIInventorySlot f in uiActSlots)
            f.OnSlotContextClickedEvent += OpenContextMenu;
        uiScript = new UIInventoryScript(uiSlots,inventory, uiActSlots, playerInventory.actItems);

        playerUseMoment.OnOpenInventoryEvent += OpenInv;
        playerInventory.OnInventoryUpdate += invUpdate;

        inventoryUIInterface.SetActive(false);
        contextMenu.SetActive(false);
    }

    private void OpenContextMenu(UISlot slot,bool b)
    {
        if (b)
        {
            ctMenu.OnActiveBTClickedEvent -= ActiveBTClicked;
            ctMenu.OnEquipBTClickedEvent -= EquipBTClicked;
            ctMenu.OnDropBTClickedEvent -= DropBTClicked;
            ctMenu.Clear();
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
        {
            ctMenu.Clear ();
            contextMenu.SetActive(false);
        }
    }

    private void DropBTClicked(UISlot slot)
    {
        
        var slotD = slot.GetComponentInParent<UIInventorySlot>();
        slotD.slot.item.info.itemGO.GetComponent<ItemTakeDrop>().SpawnItem(slotD.slot.item,playerInventory.gameObject.transform.position);
        playerInventory.inventory.Remove(this, slotD.slot.item.info.id, slotD.slot.count);
        ctMenu.OnActiveBTClickedEvent -= ActiveBTClicked;
        ctMenu.OnEquipBTClickedEvent -= EquipBTClicked;
        ctMenu.OnDropBTClickedEvent -= DropBTClicked;
        
    }

    bool TryToAddToEquippedInv(UIInventorySlot slotUI,bool b)
    {
        var slot = slotUI.slot;
        var slots = playerInventory.actItems.GetAllSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].requieItem == slot.item.info.itemType)
            {
                if (b == true)
                    playerInventory.actItems.TransitFromSlotToSlot(this, slot, slots[i]);

                else
                    if(slot.slotType==SlotTypes.Inventory)
                        playerInventory.actItems.TransitFromSlotToSlot(this, slots[i], slot);
                    else
                        playerInventory.actItems.TransitFromSlotToSlot(this, slots[i], playerInventory.inventory.GetAllSlots()[0]);

                return true;
            }

        }
        return false;
    }
    private void EquipBTClicked(UISlot slot,bool b)
    {
            var slotE = slot.GetComponentInParent<UIInventorySlot>();
            TryToAddToEquippedInv(slotE, b);
            ctMenu.OnActiveBTClickedEvent -= ActiveBTClicked;
            ctMenu.OnEquipBTClickedEvent -= EquipBTClicked;
            ctMenu.OnDropBTClickedEvent -= DropBTClicked;
        if (b)
        {
            var gm = slotE.slot.item.info.itemGO.GetComponent<ItemTakeDrop>().SpawnItem(slotE.slot.item, playerUseMoment.Hand.transform.position);
            Destroy(gm.GetComponent<ItemTakeDrop>());
            gm.transform.SetParent(playerUseMoment.Hand.transform);
            gm.transform.localPosition = Vector3.zero;
            gm.transform.localEulerAngles = new Vector3(13f, 0, 90f);
        }
        else
            Destroy(playerUseMoment.Hand.transform.GetChild(0).gameObject);
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
