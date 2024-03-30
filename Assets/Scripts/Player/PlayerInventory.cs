using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
using static UnityEditor.Progress;


public class PlayerInventory: MonoBehaviour
{

    public InventoryWithSlots collectableItems = new InventoryWithSlots(12, SlotTypes.StaticSlot, InventoryType.Equippement);
    public InventoryWithSlots craftableItems = new InventoryWithSlots(15, SlotTypes.StaticSlot, InventoryType.Storage);
    public InventoryWithSlots craftComponents= new InventoryWithSlots(3, SlotTypes.StaticSlot, InventoryType.Storage);

    public InventoryWithSlots abilities= new InventoryWithSlots(3, SlotTypes.StaticSlot, InventoryType.Equippement);


    public InventoryWithSlots storageItems = new InventoryWithSlots(20, SlotTypes.DinamicSlot, InventoryType.Storage);
    public InventoryWithSlots equippedItems = new InventoryWithSlots(4, SlotTypes.DinamicSlot, InventoryType.Equippement);
    public InventoryWithSlots weaponAndPerks = new InventoryWithSlots(4, SlotTypes.DinamicSlot, InventoryType.Equippement);
    public int levelTier= 0;
    public bool blockInv = true;
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
        InventorySlot[] slots = weaponAndPerks.GetAllSlots();
        slots[0].requieType = ItemTypes.UsableItem;
        for (int i = 1; i < slots.Length; i++)
            slots[i].requieType = ItemTypes.Perks;

        slots = storageItems.GetAllSlots();
        for (int i = 0; i < slots.Length; i++)
            slots[i].requieType = ItemTypes.Any;


    }
    private void Start()
    {

        BlockPlayerInv();
        InventorySlot[] slots = craftableItems.GetAllSlots();
        slots[0].requieItem = ItemBase.ItemsInfo["Bolts"];
        slots[0].requieItem = ItemBase.ItemsInfo["Fluid"];
        slots[0].requieItem = ItemBase.ItemsInfo["Electronics"];
        AddItem("Bolts", 0);
        AddItem("Fluid", 0);
        AddItem("Electronics", 0);
    }
    public void BlockPlayerInv()
    {
        storageItems.SetBlockInventory(blockInv);
        equippedItems.SetBlockInventory(blockInv);
        weaponAndPerks.SetBlockInventory(blockInv);
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
    public void EquippedMoment( bool b,Item item)
    {
        if (item.info.itemType == ItemTypes.UsableItem)
        {
            GameObject itemPrefab= Instantiate(item.info.itemGO);
            if (b)
            {
                itemPrefab.transform.parent= PlayerController.Instance.GetComponent<PlayerUseMoment>().Hand.transform;
                itemPrefab.transform.localPosition = new Vector3(-0.67f, -0.036f, -0.013f);
                itemPrefab.transform.localRotation = Quaternion.Euler(0, -90, 90);
            }
            else
            {
                if(PlayerController.Instance.GetComponent<PlayerUseMoment>().Hand.transform.childCount > 0)
                    DestroyImmediate(PlayerController.Instance.GetComponent<PlayerUseMoment>().Hand.transform.GetChild(0).gameObject);
            }
                
        }
    }
}
