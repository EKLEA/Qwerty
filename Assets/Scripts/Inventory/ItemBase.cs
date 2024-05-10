using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

public struct ItemBase
{
    public static ItemBase Instance;
    public static Dictionary<string, InventoryItemInfo> ItemsInfo = new Dictionary<string, InventoryItemInfo>();
    public void Initialize()
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
