using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "InventoryUsableItemInfo", menuName = "Gameplay/Items/Create new UsableItemInfo")]
public class WeaponItemInfo : InventoryItemInfo
{
    [SerializeField] public float damage;
    [SerializeField] public float cooldown;
    [SerializeField] public float range;
}
