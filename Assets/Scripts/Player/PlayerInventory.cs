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
    public IItem activeItem;
    public int invCapacity
    {
        get { return cashInvCapacity; }
        set 
        {
            cashInvCapacity = value;  
            
            InventoryWithSlots _inventory=new InventoryWithSlots(cashInvCapacity);
            var c = _inventory.GetAllSlots();
            var b = inventory.GetAllSlots();
            for (int i = 0; i < b.Length; i++)
                _inventory.TransitFromSlotToSlot(this, b[i], c[i]);
            inventory = _inventory;
            OnInventoryUpdate?.Invoke(this) ;
        }
    }
    public InventoryWithSlots inventory;
    public int cashInvCapacity=10;
    void Awake()
    {
        cashInvCapacity = invCapacity;
        inventory = new InventoryWithSlots(cashInvCapacity);
        activeItem= null;
    }
}
