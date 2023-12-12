using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public interface IUsableItemInfo
{
    public float damage { get; }
    public float coolDown { get; }
    public float range { get; }

}

public interface IItem
{
    IItemInfo info { get; set; }
    IItemState state { get; set; }
   
    IItem Clone();
    
}
public interface IDamagable
{
    public float health { get; set; }
    public float maxHealth { get;  }
    public float defense { get; set; }
    public float maxDefense { get; }
    public void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce);
}


public enum ItemTypes
{
    Consumables,
    CraftComponentss,
    UsableItem,
    Upgrade,
    RobotPart,
    Ability,
    NONE
}
public enum SlotTypes
{
    Inventory,
    EquippedItems
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
    string discription { get; }
    int maxItemsInInventortySlot { get; }
    IUsableItemInfo usableItemInfo { get; }
    Sprite spriteIcon { get; }
    ItemTypes itemType { get; }
    GameObject itemGO { get; }
}


    public interface IInventorySlot
{
    ItemTypes requieItem { get; set; }
    SlotTypes slotType { get; set; }
    bool isFull { get; }
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

public interface IMoveHandler
{
    public Rigidbody rb { get; }
    public float speed { get;   }
    public float jumpHeight { get; }
    public void Move(float xAxis);
    public void JumpMoment(bool down);
    public void SetValues(float _speed,float _jumpHeight);
    public bool Grounded();


}



