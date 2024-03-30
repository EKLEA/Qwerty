using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
public enum InventoryType
{
    Equippement,
    Storage,
}
public class InventoryWithSlots
{
    public event Action<object, Item, int> OnInventoryItemAddedEvent;
    public event Action<object, Item, int> OnInventoryItemRemovedEvent;
    public event Action<object> OnInventoryStateChangedEvent;
    public event Action<bool, Item> OnEquippedEvent;
    
    public InventoryType invType;
    
    public int invCapacity { get; set; }
    public bool isBlock=false;

    public bool isFull => _slots.All(slot => slot.isFull);

    private List<InventorySlot> _slots;

    public InventoryWithSlots(int capacity,SlotTypes slotType,InventoryType invType)
    {
        this.invCapacity = capacity;
        _slots=new List<InventorySlot>(capacity);
        for (int i = 0; i < capacity; i++)
            _slots.Add(new InventorySlot(slotType,invType));
        this.invType = invType;
    }
     public Item GetItem(string ItemID)
     {
           return _slots.Find(slot => slot.item.info.id == ItemID).item;
     }

    public Item[] GetAllItems()
    {
        var allItems = new List<Item>();
        foreach (var slot in _slots)
        {
            if (!slot.isEmpty)
                allItems.Add(slot.item);
        }
        return allItems.ToArray();
    }

    public Item[] GetAllItems(string ItemID)
    {
        var allItemsOfType = new List<Item>();
        var slotOfType = _slots.
            FindAll(slot=> !slot.isEmpty && slot.item.info.id ==ItemID);
        foreach (var slot in slotOfType)
            allItemsOfType.Add(slot.item);
        return allItemsOfType.ToArray();
    }

    public Item[] GetEquippedItems()
    {
        var requiredSlots = _slots.
            FindAll(slot => slot.item.state.IsEquipped);
        var equippedItems = new List<Item>();
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
    

    public bool TryToAdd(object sender, Item item)
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
    public bool TryAddToSlot (object sender, InventorySlot slot,Item item)
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
    public void SetBlockInventory(bool b)
    {
        isBlock = b;
    }

    public void TransitFromSlotToSlot(object sender, InventorySlot fromSlot, InventorySlot toSlot)
    {
        
        if (fromSlot.slotType == SlotTypes.StaticSlot|| fromSlot.item.info.itemType==ItemTypes.CraftComponents)
            return;
        if (toSlot == null)
            return;

        if (fromSlot.isEmpty)
            return;
        if (this.isBlock == true)
            return;
        if (toSlot.slotType == SlotTypes.DinamicSlot)
        {
            if (toSlot.inventoryType != fromSlot.inventoryType)
            {
                if (fromSlot.inventoryType == InventoryType.Storage && toSlot.inventoryType == InventoryType.Equippement)
                    PlayerInventory.Instance.EquippedMoment(true, fromSlot.item);
                if (fromSlot.inventoryType == InventoryType.Equippement && toSlot.inventoryType == InventoryType.Storage)
                    PlayerInventory.Instance.EquippedMoment(false, fromSlot.item);
            }
                if ((!toSlot.isEmpty || toSlot.isFull) && fromSlot.item.info.id != toSlot.item.info.id)
                {
                    Item a = fromSlot.item;
                    Item b = toSlot.item;
                    toSlot.CLear();
                    toSlot.SetItem(a);
                    toSlot.item.state.count = a.state.count;
                    fromSlot.CLear();
                    fromSlot.SetItem(b);
                    fromSlot.item.state.count = b.state.count;

                    OnInventoryStateChangedEvent?.Invoke(sender);
                }
                if (toSlot.isEmpty)
                {
                    toSlot.SetItem(fromSlot.item);
                    toSlot.item.state.count = fromSlot.item.state.count;
                    fromSlot.CLear();
                    OnInventoryStateChangedEvent?.Invoke(sender);

                }
                if (fromSlot == toSlot) return;
                if ((!toSlot.isEmpty && !fromSlot.isEmpty) && (fromSlot.item.info.id == toSlot.item.info.id))
                {
                    var slotCapacity = toSlot.capacity;
                    var fits = fromSlot.count + toSlot.count <= slotCapacity;
                    var amountToAdd = fits ? fromSlot.count : slotCapacity - toSlot.count;
                    var amountLeft = fromSlot.count - amountToAdd;
                    toSlot.item.state.count += amountToAdd;
                    if (fits)
                        fromSlot.CLear();
                    else
                        fromSlot.item.state.count = amountLeft;
                    OnInventoryStateChangedEvent?.Invoke(sender);
                }
            
        }







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
                if (slot.count <= 0&& slot.requieType!=ItemTypes.CraftComponents)
                    slot.CLear();
                   
                Debug.Log($"item removed from inventort. ItemId: {ItemID}, amount {amountToRemove}");
                OnInventoryItemRemovedEvent?.Invoke(sender, slot.item, amountToRemove);
                OnInventoryStateChangedEvent?.Invoke(sender);

                break;
            }
            var amountRemoved = slot.count;
            amountToRemove-= slot.count;
            if (slot.requieType != ItemTypes.CraftComponents)
                slot.CLear();
            Debug.Log($"item removed from inventort. ItemId: {ItemID}, amount {amountRemoved}");
            OnInventoryItemRemovedEvent?.Invoke(sender, slot.item, amountRemoved);
            OnInventoryStateChangedEvent?.Invoke(sender);
        }
    }
   
    public bool HasItem(string ItemID, out Item item)
    {
        item = GetItem(ItemID);
        return item != null;
    }
    public InventorySlot[] GetAllSlots(string ItemID)
    {
        return _slots.
            FindAll(slot => !slot.isEmpty
                            && slot.item.info.id == ItemID).ToArray();
    }
    public InventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }

}
