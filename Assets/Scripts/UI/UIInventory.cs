using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
   public InventoryWithSlots inventory {  get; private set; }
    private void Awake()
    {
        inventory = new InventoryWithSlots(15);
    }
}
