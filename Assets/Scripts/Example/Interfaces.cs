using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IWeapon
{
    IItem item { get; }
    public float damage { get;  }
}

public interface IItem
{
    IItemInfo info { get; }
    IItemState state { get;  }
   
    IItem Clone();
    
}
public enum ItemTypes
{
    Consumables,
    CraftComponentss,
    UsableItem
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
    ItemTypes itemType { get; }
    GameObject gameObject { get; }
}

    public interface IInventorySlot
{
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
    public CharacterController controller { get; }
    public float speed { get;   }
    public float jumpHeight { get; }
    public float gravityValue { get;  }
    public void Move(Vector2 vec);
    public void JumpMoment();
    public void SetValues(float _speed,float _gravity,float _jumpHeight, CharacterController _controller);
    public void SetVelocity(Vector2 vec);
}



