using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo inf;
    public ExampleItem item;
    public int _count;

    public ExampleItem GetExItem()
    {
        var item = new ExampleItem(inf);
        item.state.count = _count;
        return item;
    }
    
}
