using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelList : MonoBehaviour
{
    [Header("Health")]
    public float addEnergy;
    public float addHealth;
    private float taHP;
    private float taEN;
    public float tempAddHP
    {
        get { return taHP; }
        set
        {
            if (value % 3 == 0)
            {
                taHP = 0;
                PlayerController.Instance.playerHealthController.IncreaseMaxHealth(1);
            }
            else
                taHP = value;
        }
    }
    public float tempAddEN
    {
        get { return taEN; }
        set
        {
            if (value % 3 == 0)
            {
                taEN = 0;
                PlayerController.Instance.playerHealthController.IncreaseMaxEnergy(1);
            }
            else
                taEN = value;
        }
    }
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
    public bool canDash = false;
    public bool canDoubleWallJump=false;



}
