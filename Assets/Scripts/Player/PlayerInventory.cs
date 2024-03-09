using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
using static UnityEditor.Progress;


public class PlayerInventory: MonoBehaviour
{
    public event Action<object> OnInventoryUpdate;
    public InventoryWithSlots equippedItems = new InventoryWithSlots(7, SlotTypes.DinamicSlot);

    public InventoryWithSlots collectableItems = new InventoryWithSlots(12, SlotTypes.StaticSlot);
    public InventoryWithSlots craftComponents= new InventoryWithSlots(3, SlotTypes.StaticSlot);
}
