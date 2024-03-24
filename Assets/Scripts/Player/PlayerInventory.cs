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
    public InventoryWithSlots storageItems = new InventoryWithSlots(40, SlotTypes.DinamicSlot);
    public InventoryWithSlots craftableItems = new InventoryWithSlots(15, SlotTypes.StaticSlot);
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
        craftComponents.TryToCheatAdd(ItemBase.ItemsInfo["Bolti"], 0);

    }
}
