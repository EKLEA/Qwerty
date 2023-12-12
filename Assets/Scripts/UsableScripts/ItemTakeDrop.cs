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
        DestroyImmediate(subject);
    }
    public GameObject SpawnItem(IItem item,Vector3 pos)
    {
        
        var gm =Instantiate(item.info.itemGO,pos, Quaternion.identity);
        gm.GetComponent<Item>().SetOperator(item.state);
        
        return gm;
    }
}
