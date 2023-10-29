using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class InventoryItemState : IItemState
{
    public int itemAmount;
    public bool isItemEquipped;

    public int count { get => itemAmount; set => itemAmount = value; }   
    public bool IsEquipped { get => isItemEquipped; set => isItemEquipped = value; }

    public InventoryItemState()
    {
        itemAmount = 0;
        isItemEquipped = false; 
    }
}
