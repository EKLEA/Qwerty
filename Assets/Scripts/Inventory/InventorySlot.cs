using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : IInventorySlot
{
    public bool isFull => count == capacity;

    public bool isEmpty => item==null;

    public IItem item { get; private set; }

    public Type itemType => item.itemType;

    public int count => isEmpty ? 0 : item.state.count;

    public int capacity {get; private set;}
    public void SetItem(IItem item)
    {
        if (!isEmpty)
        {
            return;
        }
        this.item = item; ;
        this.capacity = item.info.maxItemsInInventortySlot;
    }
    public void CLear()
    {
        if (isEmpty)
            return;
        item.state.count = 0;
        item=null;
    }

   
}
