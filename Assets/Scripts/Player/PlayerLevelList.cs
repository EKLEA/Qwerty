using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelList : MonoBehaviour
{
    [Header("Health")]
    public float addEnergy;
    public float addHealth;
    [Space(5)]
    [Header("Move")]
    public float movekf;
    [Space(5)]
    [Header("Inventory")]
    public int levelTier = 0;
    [Space(5)]
    [Header("Ability")]
    
    public bool SideCast = false;
    public bool DownCast = false;
    public bool UPCast = false;



}
