using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class InventoryItemState 
{
     int itemAmount;
    bool isItemEquipped;
    public GameObject isItemOperator;

    public int count { get => itemAmount; set => itemAmount = value; }   
    public bool IsEquipped { get => isItemEquipped; set => isItemEquipped = value; }
    public GameObject itemOperator { get=> isItemOperator; set => isItemOperator = value; }

    public InventoryItemState()
    {
        itemAmount = 0;
        isItemEquipped = false; 
        isItemOperator= null;
    }
}
