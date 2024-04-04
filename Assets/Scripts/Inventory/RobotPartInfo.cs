using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "InventoryRobotPartInfo", menuName = "Gameplay/Items/Create new InventoryRobotPartInfo")]
public class RobotPartInfo : CraftableItemInfo
{
    public RobotParts robotParts;
    public float partMoveKf;
    public float partBaseDamage;
    public float partbaseRange;
    public float partBaseCooldown;
}
