using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : IItem
{
    public IItemInfo info { get; }

    public IItemState state { get; }

    public Type itemType => GetType();
    
    public Apple(IItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();   
    }
    public IItem Clone()
    {
        var clonedApple = new Apple(info);
        clonedApple.state.count = state.count;
        return clonedApple;
    }
}
