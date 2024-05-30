using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
   public List<ItemTakeDrop> list= new List<ItemTakeDrop>();
    private void Awake()
    {
        foreach( ItemTakeDrop item in list )
        {
            if(item.itemInf.itemType==ItemTypes.collectableItems)
            {
                if (PlayerInventory.Instance.collectableItems.GetAllItems(item.itemInf.id).Length == 1) 
                    item.gameObject.SetActive(false);

            }
            else if(item.itemInf.itemType == ItemTypes.UsableItem)
            {
                if (PlayerInventory.Instance.weaponAndPerks.GetAllItems(item.itemInf.id).Length == 1)
                    item.gameObject.SetActive(false);

            }
            else
            {
                if (PlayerInventory.Instance.storageItems.GetAllItems(item.itemInf.id).Length == 1)
                    item.gameObject.SetActive(false);

            }
        }    
    }
}
