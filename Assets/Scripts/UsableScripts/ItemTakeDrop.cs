using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemTake : ExampleUsable
{
    [SerializeField] InventoryItemInfo itemInf;
    Item item;
    private void Start()
    {
        Item item = new Item(itemInf);
    }
    public override void UseMoment()
    {
        if (itemInf.itemType == ItemTypes.UsableItem)
        {
            gameObject.transform.parent = playerUseMoment.Hand.transform;
            transform.localPosition = new Vector3(-0.67f, -0.036f, -0.013f);
            transform.localRotation = Quaternion.Euler(0, -90, 90);
            playerUseMoment.OnUsedEvent -= Cheker;
        }
        else if (itemInf.itemType== ItemTypes.CraftComponents)
        {
            PlayerInventory.Instance.craftComponents.TryToAdd(null,item);
        }
        else if(itemInf.itemType==ItemTypes.collectableItems)
        {
            PlayerInventory.Instance.collectableItems.TryToAdd(null, item);
        }
    }
}
