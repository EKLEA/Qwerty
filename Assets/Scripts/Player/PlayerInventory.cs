using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
using static UnityEditor.Progress;


public class PlayerInventory: MonoBehaviour
{

    public InventoryWithSlots collectableItems = new InventoryWithSlots(12, SlotTypes.StaticSlot, InventoryType.Equippement);
    public InventoryWithSlots craftableItems;
    public InventoryWithSlots craftComponents= new InventoryWithSlots(3, SlotTypes.StaticSlot, InventoryType.Storage);

    public InventoryWithSlots abilities= new InventoryWithSlots(3, SlotTypes.StaticSlot, InventoryType.Equippement);


    public InventoryWithSlots storageItems = new InventoryWithSlots(20, SlotTypes.DinamicSlot, InventoryType.Storage);
    public InventoryWithSlots equippedItems = new InventoryWithSlots(3, SlotTypes.DinamicSlot, InventoryType.Equippement);
    public InventoryWithSlots weaponAndPerks = new InventoryWithSlots(4, SlotTypes.DinamicSlot, InventoryType.Equippement);
   
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
    }
    private void Start()
    {
        InventorySlot[] slots = craftComponents.GetAllSlots();

        foreach(InventorySlot slot in slots)
            slot.requieType = ItemTypes.CraftComponents;
        slots[0].requieItem = ItemBase.ItemsInfo["Bolts"];
        slots[1].requieItem = ItemBase.ItemsInfo["Fluid"];
        slots[2].requieItem = ItemBase.ItemsInfo["Electronics"];

        AddItem("Bolts", 1);
        AddItem("Fluid", 1);
        AddItem("Electronics", 1);


        slots = weaponAndPerks.GetAllSlots();
        slots[0].requieType = ItemTypes.UsableItem;
        for (int i = 1; i < slots.Length; i++)
            slots[i].requieType = ItemTypes.Perks;

        slots = storageItems.GetAllSlots();
        for (int i = 0; i < slots.Length; i++)
            slots[i].requieType = ItemTypes.Any;

        slots = equippedItems.GetAllSlots();
        for (int i = 0; i < slots.Length; i++)
            slots[i].requieType = ItemTypes.RobortParts;

        slots[0].requieTypePart = RobotParts.Body;
        slots[1].requieTypePart = RobotParts.Arm;
        slots[2].requieTypePart = RobotParts.Legs;



        Instance.equippedItems.TryToAdd(null, new Item(ItemBase.ItemsInfo["body0"], 1));
        Instance.equippedItems.TryToAdd(null, new Item(ItemBase.ItemsInfo["arm0"], 1));
        Instance.equippedItems.TryToAdd(null, new Item(ItemBase.ItemsInfo["legs0"], 1));

        BlockPlayerInv();
        List<InventoryItemInfo> list = new List<InventoryItemInfo>();
        foreach( InventoryItemInfo itemInf in ItemBase.ItemsInfo.Values)
        {
            if(itemInf.isCraftable==true)
                list.Add(itemInf);
        }
        list = list.OrderBy(item=> item.requielevel).ThenBy(item=> item.id).ToList();
        craftableItems = new InventoryWithSlots(list.Count, SlotTypes.StaticSlot, InventoryType.Storage);
        foreach( InventoryItemInfo itemInf in list)
            craftableItems.TryToAdd(null, new Item(itemInf, 1));
    }
    public void SetupPlayerVariables()
    {
        foreach (Item rp in equippedItems.GetAllItems())
        {
            if (equippedItems.GetAllItems().Length != 0 && rp != null)
            {
                PlayerController.Instance.playerLevelList.movekf += (rp.info as RobotPartInfo).partMoveKf;
                PlayerController.Instance.playerLevelList.baseDamage += (rp.info as RobotPartInfo).partBaseDamage;
                PlayerController.Instance.playerLevelList.baseRange += (rp.info as RobotPartInfo).partbaseRange;
                PlayerController.Instance.playerLevelList.baseCooldown += (rp.info as RobotPartInfo).partBaseCooldown;
            }
        }

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
