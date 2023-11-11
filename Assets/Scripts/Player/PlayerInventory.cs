using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class PlayerInventory
{
    
    public InventoryWithSlots inventory;
    private ExampleItem item;
    private UIInventorySlot[] _uiSlots;
    private int invCapacity;
    


    public PlayerInventory()
    {
        inventory = new InventoryWithSlots(invCapacity);
        inventory.OnInventoryStateChangedEvent += OnInventoryStateCganged;
        SetupInventoryUI(inventory);
        
    }

    public void SetUiSlots(UIInventorySlot[] uiSlots)
    {
        _uiSlots = uiSlots;
    }
    public void SetInvCapacity(int _invCapacity)
    {
        invCapacity= _invCapacity;
    }
    private InventoryWithSlots SetInventory(int _invCapacity)
    {
        return new InventoryWithSlots(_invCapacity);
    }
    private void SetupInventoryUI(InventoryWithSlots inventory)
    {
        var allSlots= inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;
        for (int i = 0; i < allSlotsCount; i++) { 
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }

    } 
    private IInventorySlot AddItemToSlot(List<IInventorySlot> slots)
    {
        
        var rIndex = UnityEngine.Random.Range(0, slots.Count);
        var rSlot = slots[rIndex];
        inventory.TryAddToSlot(this, rSlot, item);
        return rSlot;
    }
    public void AddItem( ExampleItem _item)
    {
        item=_item;
        var allSlots = inventory.GetAllSlots();
        var aviableslots=new List<IInventorySlot>(allSlots);
        var filledSlot = AddItemToSlot(aviableslots);
        aviableslots.Remove(filledSlot);

    }
    private void OnInventoryStateCganged(object sender)
    {
        foreach (var uiSlot in _uiSlots)
            uiSlot.Refresh();
    }
    
}
