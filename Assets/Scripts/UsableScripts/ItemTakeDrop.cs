using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;




public class ItemTakeDrop : ExampleUsable
{
    public InventoryItemInfo itemInf;
    public int count;

    public override void UseMoment()
    {
       // if (gameObject.layer==LayerMask.NameToLayer("Weapon"))
       // {
       //     gameObject.transform.parent = playerUseMoment.Hand.transform;
       //     transform.localPosition = new Vector3(-0.67f, -0.036f, -0.013f);
       //     transform.localRotation = Quaternion.Euler(0, -90, 90);
        //    playerUseMoment.OnUsedEvent -= Cheker;
       // }
        //else
            PlayerInventory.AddItem(itemInf.id, count);
        playerUseMoment.OnUsedEvent -= Cheker;
        DestroyImmediate (gameObject);
    }
    public static void SpawnItem(string id,int count)
    {
        GameObject gm= Instantiate(ItemBase.ItemsInfo[id].itemGO);
        gm.GetComponent<ItemTakeDrop>().count = count;
    }
}
