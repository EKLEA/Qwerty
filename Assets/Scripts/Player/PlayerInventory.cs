using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
using static UnityEditor.Progress;


public class PlayerInventory: MonoBehaviour
{
    public InventoryWithSlots equippedItems = new InventoryWithSlots(7, SlotTypes.DinamicSlot);
    public InventoryWithSlots collectableItems = new InventoryWithSlots(12, SlotTypes.StaticSlot);
    public InventoryWithSlots craftComponents= new InventoryWithSlots(3, SlotTypes.StaticSlot);
    public InventoryWithSlots storageItems = new InventoryWithSlots(20, SlotTypes.DinamicSlot);
    public InventoryWithSlots craftableItems = new InventoryWithSlots(15, SlotTypes.StaticSlot);
    public InventoryWithSlots weaponAndPerks = new InventoryWithSlots(4, SlotTypes.StaticSlot);
    public int levelTier= 0;
    public static PlayerInventory Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {

        storageItems.SetBlockInventory(true);
        AddItem( "Bolts", 0);
        AddItem( "Fluid", 0);
        AddItem( "Electronics", 0);

    }
    public static void AddItem(string id,int count)
    {
        if (!ItemBase.ItemsInfo.ContainsKey(id))
            return;
        if (ItemBase.ItemsInfo[id].itemType == ItemTypes.CraftComponents)
            Instance.craftComponents.TryToAdd(null, new Item(ItemBase.ItemsInfo[id],count));
        else if (ItemBase.ItemsInfo[id].itemType == ItemTypes.collectableItems)
            Instance.collectableItems.TryToAdd(null, new Item(ItemBase.ItemsInfo[id], count));
        else
            Instance.storageItems.TryToAdd(null, new Item(ItemBase.ItemsInfo[id], count));

    }
    public static void RemoveItem(string id, int count)
    {
        if (ItemBase.ItemsInfo[id].itemType == ItemTypes.CraftComponents)
            Instance.craftComponents.Remove(null,id, count);
        else if (ItemBase.ItemsInfo[id].itemType == ItemTypes.collectableItems)
            Instance.collectableItems.Remove(null, id, count);
        else
            Instance.storageItems.Remove(null, id, count);
    }

}
