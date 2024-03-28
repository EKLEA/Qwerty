using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct Items
{
    public InventoryItemInfo itemInf;
    [HideInInspector] public int count;
}

public class ItemsDropper : DamagableObj
{

    public Items[] items;
    public override void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damageDone;
        foreach (var itemInf in items)
            ItemTakeDrop.SpawnItem(itemInf.itemInf.id, UnityEngine. Random.Range(1, 5));
    }
}
