using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : IInventorySlot
{
    public bool isFull => !isEmpty && count == capacity;

    public bool isEmpty => item==null;
   public InventorySlot(SlotTypes _slotType)
    {
        slotType = _slotType;
    }

    public IItem item { get; private set; }


    public int count => isEmpty ? 0 : item.state.count;

    public int capacity {get; private set;}
    public bool isBlock { get; set; }
    public ItemTypes requieItem {  get; set; }
    public SlotTypes slotType { get; set; }

    public void SetItem(IItem item)
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
