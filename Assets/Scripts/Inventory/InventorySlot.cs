using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot 
{
   
    public bool isFull => !isEmpty && count == capacity;

    public bool isEmpty => item==null;
   public InventorySlot(SlotTypes _slotType, InventoryType inventoryType)
    {
        slotType = _slotType;
        this.inventoryType = inventoryType;
    }

    public Item item { get; private set; }


    public int count => isEmpty ? 0 : item.state.count;

    public int capacity {get; private set;}
    public bool isBlock { get; set; }
    public InventoryItemInfo requieItem {  get; set; }
    public ItemTypes requieType{  get;set; }
    public RobotParts requieTypePart { get; set; }
    public SlotTypes slotType { get; set; }
    public InventoryType inventoryType { get; set; }

    public void SetItem(Item item)
    {
        if (!isEmpty)
        {
            return;
        }
        this.item = item;
        this.capacity = item.info.maxItemsInInventortySlot;
    }
    public void CLear()
    {
        if (isEmpty)
            return;
        item = null;
        this.capacity = 0;
    }
}
public enum SlotTypes
{
    StaticSlot,
    DinamicSlot
}
