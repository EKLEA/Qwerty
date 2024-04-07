using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName="InventoryItemInfo",menuName= "Gameplay/Items/Create new ItemInfo")]
public class InventoryItemInfo : ScriptableObject
{
    [SerializeField] public string id;
    [SerializeField] public string title;
    [SerializeField] public string description;
    [SerializeField] public int maxItemsInInventortySlot;
    [SerializeField] public Sprite spriteIcon;
    [SerializeField] public ItemTypes itemType;
    [SerializeField] public GameObject itemGO;
    [SerializeField] public int requielevel;
    [SerializeField] public bool isCraftable;

}
public enum ItemTypes
{ 
    Any,
    CraftComponents,
    collectableItems,
    UsableItem,
    Perks,
    RobortParts,
    Ability

    
}
public enum RobotParts
{
    Legs,
    Arm,
    Body,
}
