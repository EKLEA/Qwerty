using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftableItemInfo : InventoryItemInfo
{
    [Space(5)]
    [Header("CraftComponents")]
    public int requeBolts;
    public int requeElectronics;
    public int requeFluid;
}
