using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName="InventoryItemInfo",menuName= "Gameplay/Items/Create new ItemInfo")]
public class InventoryItemInfo : ScriptableObject, IItemInfo
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private int _maxItemsInInventortySlot;
    [SerializeField] private Sprite _spriteIcon;
    [SerializeField] private ItemTypes _itemType;





    public string id => _id;

    public string title => _title;

    public string discription => _description;

    public int maxItemsInInventortySlot => _maxItemsInInventortySlot;

    public Sprite spriteIcon => _spriteIcon;

    public ItemTypes itemType => _itemType;
}
