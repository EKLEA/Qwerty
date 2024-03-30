using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public class ItemBase :MonoBehaviour
{
    public static Dictionary<string, InventoryItemInfo> ItemsInfo = new Dictionary<string, InventoryItemInfo>();
    void Start()
    {

        Object[] assets = Resources.LoadAll<InventoryItemInfo>("");
        foreach (object obj in assets)
        {
            InventoryItemInfo so = obj as InventoryItemInfo;
            if (!ItemsInfo.ContainsValue(so))
                ItemsInfo.Add(so.id, so);
        }
       
    }

}
