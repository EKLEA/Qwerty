using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IsUsable
{
    public GameObject Operator { get; }
    public GameObject Subject {get; }
    void UseMoment();

}

public interface IItem
{
    IItemInfo info { get; }
    IItemState state { get; }
    Type itemType { get; }
    IItem Clone();
    
}
public interface IItemState
{
    bool IsEquipped { get; set; }
    int count { get; set; }

}
public interface IItemInfo
{
    string id { get; }
    string title { get; }
    string discription { get; }
    int maxItemsInInventortySlot { get; }
    Sprite spriteIcon { get; }
}

    public interface IInventorySlot
{
    bool isFull { get; }
    bool isEmpty { get; }
    IItem item{ get; }
    Type itemType { get; }
    int count { get; }
    int capacity { get; }

    void SetItem(IItem item);
    void CLear();
}
public interface IInventory
{
    int invCapacity { get; set; }
    bool isFull { get; }
    IItem GetItem(Type itemType);
    IItem[] GetAllItems();
    IItem[] GetAllItems(Type itemType);
    IItem[] GetEquippedItems();
    int GetItemCount(Type itemtype);
    bool TryToAdd(object sender, IItem item);
    void Remove(object sender, Type itemType, int count = 1);
    bool HasItem(Type itemtype, out IItem item);
}

public interface IsDamagable
{
    public float hp { get; }
    public GameObject _subject { get; }
    void DamageMoment();
}
public enum ItemTypes
{
    consumables = 0,
    craftComponents = 1,
    itemsToUse = 2
}
