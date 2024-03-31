using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelList : MonoBehaviour
{
    [Header("Damage")]

    public float baseDamage;
    public float baseRange;
    public float baseCooldown;
    [Space(5)]
    [Header("Move")]
    public float movekf;
    [Space(5)]
    [Header("Inventory")]
    public int levelTier = 0;
}
