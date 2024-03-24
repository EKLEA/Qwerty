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
    [SerializeField] private UsableItemInfo _usInfo;
    [SerializeField] private int _maxItemsInInventortySlot;
    [SerializeField] private Sprite _spriteIcon;
    [SerializeField] private ItemTypes _itemType;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] int _requielevel;





    public string id => _id;

    public string title => _title;

    public string discription => _description;

    public int maxItemsInInventortySlot => _maxItemsInInventortySlot;

    public Sprite spriteIcon => _spriteIcon;

    public ItemTypes itemType => _itemType;

    public GameObject itemGO=> _gameObject;

    public IUsableItemInfo usableItemInfo => _usInfo;

    public int requielevel => _requielevel;

    public InventoryWithSlots requieCraftComonents => throw new System.NotImplementedException();
}
