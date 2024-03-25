using System;
using UnityEngine;

[System.Serializable]

public class Item : IItem
{
    public IItemInfo info { get; set; }

    public IItemState state { get; set; }
    public Item(IItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public Item(IItemInfo info , int count)
    {
        this.info = info;
        state = new InventoryItemState();
        state.count = count;
    }
    public IItem Clone()
    {
        var clonedExampleItem = new Item(info);
        clonedExampleItem.state.count = state.count;
        return clonedExampleItem;
    }
}
