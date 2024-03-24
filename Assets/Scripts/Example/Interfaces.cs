using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public interface IUsableItemInfo
{
    public int damage { get; }
    public float coolDown { get; }
    public float range { get; }

}

public interface IItem
{
    IItemInfo info { get; set; }
    IItemState state { get; set; }
   
    IItem Clone();
    
}


public enum ItemTypes
{
    CraftComponents,
    collectableItems,
    UsableItem,
    Perks,
    RobotParts
}
public enum RobotParts
{
    Legs,
    Arm,
    Body,
}
public enum SlotTypes
{
    StaticSlot,
    DinamicSlot
}
public interface IItemState
{
    bool IsEquipped { get; set; }
    int count { get; set; }
    GameObject itemOperator { get; set; }

}
public interface IItemInfo
{
    string id { get; }
    string title { get; }
    public InventoryWithSlots requieCraftComonents { get; }//ss
    string discription { get; }
    int maxItemsInInventortySlot { get; }
    int requielevel {  get;  }
    IUsableItemInfo usableItemInfo { get; }
    Sprite spriteIcon { get; }
    ItemTypes itemType { get; }
    GameObject itemGO { get; }
}


 public interface IInventorySlot
{
    ItemTypes requieType { get; set; }
    IItemInfo requieItem{ get; set; }
    SlotTypes slotType { get; set; }
    bool isFull { get; }
    bool isBlock { get;set; }
    bool isEmpty { get; }
    IItem item{ get; }
   
    int count { get; }
    int capacity { get; }

    void SetItem(IItem item);
    void CLear();
}
public interface IInventory
{
    int invCapacity { get; set; }
    bool isFull { get; }
    IItem GetItem(string ItemID);
    IItem[] GetAllItems();
    IItem[] GetAllItems(string ItemID);
    IItem[] GetEquippedItems();
    int GetItemCount(string ItemID);
    bool TryToAdd(object sender, IItem item);
    void Remove(object sender, string ItemID, int count = 1);
    bool HasItem(string ItemID, out IItem item);
}




