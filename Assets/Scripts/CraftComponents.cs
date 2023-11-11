using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class —raft—omponents:  IItem
{
    public IItemInfo info { get; }

    public IItemState state { get; }
    
    public —raft—omponents(IItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();   
    }
    public IItem Clone()
    {
        var clonedItem = new —raft—omponents(info);
        clonedItem.state.count = state.count;
        return clonedItem;
    }
}
