using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private InventoryItemInfo inf;
    private ExampleItem item;
    private GameObject itemOperator;
    public int _count;
    public ExampleItem GetExItem()
    {
        item = new ExampleItem(inf);
        item.state.count = _count;
        item.state.itemOperator = itemOperator;
        return item;
    }
    public void SetOperator(IItemState state)
    {
        itemOperator = state.itemOperator;
    }

}
