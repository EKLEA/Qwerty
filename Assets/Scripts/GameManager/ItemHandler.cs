using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
   public List<ExampleUsable> list= new List<ExampleUsable>();
    private void Awake()
    {
        foreach(ExampleUsable obj in list )
        {
            if(obj as ItemTakeDrop != null)
            {
                ItemTakeDrop item = obj as ItemTakeDrop;
                if (item.itemInf.itemType == ItemTypes.collectableItems)
                {
                    if (PlayerInventory.Instance.collectableItems.GetAllItems(item.itemInf.id).Length > 0)
                        item.gameObject.SetActive(false);

                }
                else if (item.itemInf.itemType == ItemTypes.UsableItem)
                {
                    if (PlayerInventory.Instance.weaponAndPerks.GetAllItems(item.itemInf.id).Length > 0 || PlayerInventory.Instance.storageItems.GetAllItems(item.itemInf.id).Length > 0)
                        item.gameObject.SetActive(false);

                }
                else
                {
                    if (PlayerInventory.Instance.storageItems.GetAllItems(item.itemInf.id).Length > 0)
                        item.gameObject.SetActive(false);

                }
            }
            else if((obj as UnlockSideCast != null&& PlayerController.Instance.playerLevelList.SideCast==true) 
                || (obj as UnlockDownCast != null && PlayerController.Instance.playerLevelList.DownCast == true) 
                ||(( obj as UnlockUpCast != null) && PlayerController.Instance.playerLevelList.UPCast == true))
            {
                obj.gameObject.SetActive(false);
            }

        }    
    }
}
