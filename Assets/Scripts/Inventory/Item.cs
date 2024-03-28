using System;
using UnityEngine;

[System.Serializable]

public class Item 
{
    public InventoryItemInfo info { get; set; }

    public InventoryItemState state { get; set; }
    public Item(InventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public Item(InventoryItemInfo info , int count)
    {
        this.info = info;
        state = new InventoryItemState();
        state.count = count;
    }
    public Item Clone()
    {
        var clonedExampleItem = new Item(info);
        clonedExampleItem.state.count = state.count;
        return clonedExampleItem;
    }
}
