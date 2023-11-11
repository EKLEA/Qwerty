using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryWithSlots : IInventory
{
    public event Action<object, IItem, int> OnInventoryItemAddedEvent;
    public event Action<object, IItem, int> OnInventoryItemRemovedEvent;
    public event Action<object> OnInventoryStateChangedEvent;
    public int invCapacity { get; set; }

    public bool isFull => _slots.All(slot => slot.isFull);

    private List<InventorySlot> _slots;

    public InventoryWithSlots(int capacity)
    {
        this.invCapacity = capacity;
        _slots=new List<InventorySlot>(capacity);
        for (int i = 0; i < capacity; i++)
            _slots.Add(new InventorySlot());
    }
     public IItem GetItem(string ItemID)
     {
        return _slots.Find(slot => slot.item.info.id == ItemID).item;
     }

    public IItem[] GetAllItems()
    {
        var allItems = new List<IItem>();
        foreach (var slot in _slots)
        {
            if (!slot.isEmpty)
                allItems.Add(slot.item);
        }
        return allItems.ToArray();
    }

    public IItem[] GetAllItems(string ItemID)
    {
        var allItemsOfType = new List<IItem>();
        var slotOfType = _slots.
            FindAll(slot=> !slot.isEmpty && slot.item.info.id ==ItemID);
        foreach (var slot in slotOfType)
            allItemsOfType.Add(slot.item);
        return allItemsOfType.ToArray();
    }

    public IItem[] GetEquippedItems()
    {
        var requiredSlots = _slots.
            FindAll(slot => slot.item.state.IsEquipped);
        var equippedItems = new List<IItem>();
        foreach(var slot in requiredSlots)
            equippedItems.Add(slot.item);
        return equippedItems.ToArray();
    }
    public int GetItemCount(string ItemID)
    {
        var count = 0;
        var allItemsSlost = _slots.
            FindAll(slot => slot.item.info.id == ItemID);
        foreach (var itemSlot in allItemsSlost)
            count += itemSlot.count;
        return count;
    }

    public bool TryToAdd(object sender, IItem item)
    {
        var slotWithSameItemButNotEmpty = _slots.
            Find(slot => !slot.isEmpty
                    && slot.item.info.id == item.info.id && !slot.isFull);

        if (slotWithSameItemButNotEmpty!=null)
            return TryAddToSlot(sender, slotWithSameItemButNotEmpty,item);

        var emptySlot= _slots.Find(slot => slot.isEmpty);

        if (emptySlot!=null)
            return TryAddToSlot(sender, emptySlot,item);

        Debug.Log($"Cannot add item ({item.info.id}), count: {item.state.count}, " +
            $"because there is no place for that.");
        return false;
    }
    public bool TryAddToSlot (object sender, IInventorySlot slot,IItem item)
    {
        var fits = slot.count + item.state.count <= item.info.maxItemsInInventortySlot;
        var amountToAdd = fits
            ? item.state.count
            :item.info.maxItemsInInventortySlot-slot.count;
        var amountLeft = item.state.count - amountToAdd;
        var clonedItem = item.Clone();
        clonedItem.state.count = amountToAdd;

        if (slot.isEmpty)
            slot.SetItem(clonedItem);
        else
            slot.item.state.count += amountToAdd;

        Debug.Log($"item added to inventort. ItemId: {item.info.id}, amount {amountToAdd}");
        OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);
        OnInventoryStateChangedEvent?.Invoke(sender);

        if (amountLeft <= 0)
            return true;

        item.state.count= amountLeft;
        return TryToAdd(sender, item);

    }

    public void TransitFromSlotToSlot(object sender,IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot.isEmpty)
            return;
        if (toSlot.isEmpty)
            return;

        if (toSlot.isEmpty && fromSlot.item.info.id != toSlot.item.info.id)
            return;

        var slotCapacity = fromSlot.capacity;
        var fits = fromSlot.count+ toSlot.count<=slotCapacity;
        var amountToAdd = fits ? fromSlot.count : slotCapacity - toSlot.count;
        var amountLeft = fromSlot.count - amountToAdd;

        if (toSlot.isEmpty)
        {
            toSlot.SetItem(fromSlot.item);
            fromSlot.CLear();
            OnInventoryStateChangedEvent?.Invoke(sender);
        }

        toSlot.item.state.count += amountToAdd;
        if(fits)
            fromSlot.CLear();
        else
            fromSlot.item.state.count= amountLeft;
        OnInventoryStateChangedEvent?.Invoke(sender);
    }

 public void Remove(object sender, string ItemID, int count = 1)
    {
       var slotsWithItem = GetAllSlots(ItemID);
        if (slotsWithItem.Length == 0)
            return;

        var amountToRemove = count;
        var k = slotsWithItem.Length;
        
        for (int i = k-1; i >= 0; i--)
        {
            var slot= slotsWithItem[i];
            if (slot.count >= amountToRemove)
            {
                slot.item.state.count -= amountToRemove;
                if (slot.count <= 0)
                    slot.CLear();
                Debug.Log($"item removed from inventort. ItemId: {ItemID}, amount {amountToRemove}");
                OnInventoryItemRemovedEvent?.Invoke(sender, slot.item, amountToRemove);
                OnInventoryStateChangedEvent?.Invoke(sender);

                break;
            }
            var amountRemoved = slot.count;
            amountToRemove-= slot.count;
            slot.CLear();
            Debug.Log($"item removed from inventort. ItemId: {ItemID}, amount {amountRemoved}");
            OnInventoryItemRemovedEvent?.Invoke(sender, slot.item, amountRemoved);
            OnInventoryStateChangedEvent?.Invoke(sender);
        }
    }
    public bool HasItem(string ItemID, out IItem item)
    {
        item = GetItem(ItemID);
        return item != null;
    }
    public IInventorySlot[] GetAllSlots(string ItemID)
    {
        return _slots.
            FindAll(slot => !slot.isEmpty
                            && slot.item.info.id == ItemID).ToArray();
    }
    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }

}
