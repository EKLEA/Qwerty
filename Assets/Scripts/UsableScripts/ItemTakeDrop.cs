using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemTakeDrop : ExampleUsable
{
    
    public override void UseMoment()
    {
        
        playerUseMoment.gameObject.GetComponent<PlayerInventory>().inventory.TryToAdd(this, subject.GetComponent<Item>().GetExItem());
        playerUseMoment.OnUsedEvent -= Cheker;
        Destroy(subject);
    }
    public GameObject SpawnItem(IItem item,Vector3 pos)
    {
        var gm =Instantiate(item.info.itemGO,pos, Quaternion.identity);
        gm.GetComponent<Item>().GetExItem().info = item.info;
        gm.GetComponent<Item>().GetExItem().state=item.state;
        return gm;
    }
}
