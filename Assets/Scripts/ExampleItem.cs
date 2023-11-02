using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : MonoBehaviour, IItem
{

    public IItemInfo info {get; }

public IItemState state { get; }

    public Type itemType => GetType();

    

    public ExampleItem(IItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public IItem Clone()
    {
        var clonedExampleItem = new ExampleItem(info);
        clonedExampleItem.state.count = state.count;
        return clonedExampleItem;
    }
}
