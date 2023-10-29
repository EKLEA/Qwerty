using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryWithSlots : IInventory
{
    public event Action<object, IItem, int> OnInventoryItemAddedEvent;
    public event Action<object, Type, int> OnInventoryItemRemovedEvent;
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
     public IItem GetItem(Type itemType)
     {
        return _slots.Find(slot => slot.itemType == itemType).item;
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

    public IItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IItem>();
        var slotOfType = _slots.
            FindAll(slot=> !slot.isEmpty && slot.itemType==itemType);
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
    public int GetItemCount(Type itemtype)
    {
        var count = 0;
        var allItemsSlost = _slots.
            FindAll(slot => slot.itemType == itemtype);
        foreach (var itemSlot in allItemsSlost)
            count += itemSlot.count;
        return count;
    }

    public bool TryToAdd(object sender, IItem item)
    {
        var slotWithSameItemButNotEmpty = _slots.
            Find(slot => !slot.isEmpty
                    && slot.itemType == item.itemType && !slot.isFull);

        if (slotWithSameItemButNotEmpty!=null)
            return TryAddToSlot(sender, slotWithSameItemButNotEmpty,item);

        var emptySlot= _slots.Find(slot => slot.isEmpty);

        if (emptySlot!=null)
            return TryAddToSlot(sender, emptySlot,item);

        Debug.Log($"Cannot add item ({item.itemType}), count: {item.state.count}, " +
            $"because there is no place for that.");
        return false;
    }
    private bool TryAddToSlot (object sender, IInventorySlot slot,IItem item)
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

        Debug.Log($"item added to inventort. ItemType: {item.itemType}, amount {amountToAdd}");
        OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);

        if (amountLeft <= 0)
            return true;

        item.state.count= amountLeft;
        return TryToAdd(sender, item);

    }

    public void Remove(object sender, Type itemType, int count = 1)
    {
       var slotsWithItem = GetAllSlots(itemType);
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
                Debug.Log($"item removed from inventort. ItemType: {itemType}, amount {amountToRemove}");
                OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);

                break;
            }
            var amountRemoved = slot.count;
            amountToRemove-= slot.count;
            slot.CLear();
            Debug.Log($"item removed from inventort. ItemType: {itemType}, amount {amountRemoved}");
            OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountRemoved);
        }
    }
    public bool HasItem(Type itemtype, out IItem item)
    {
        item = GetItem(itemtype);
        return item != null;
    }
    public IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.
            FindAll(slot => !slot.isEmpty
                            && slot.itemType == itemType).ToArray();
    }
    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }

}
