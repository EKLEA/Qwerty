using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


public class PlayerInventory: MonoBehaviour
{
    public int invCapavity;
    public InventoryWithSlots inventory;
    void Awake()
    {
        inventory = new InventoryWithSlots(invCapavity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Item"))
        {
            inventory.TryToAdd(this,other.GetComponent<Item>().GetExItem());
        }
    }


}
