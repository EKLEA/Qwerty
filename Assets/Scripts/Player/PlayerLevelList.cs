using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLevelList : MonoBehaviour
{
    public event Action<object> OnShardAdded;

    [Header("Health")]
    public float addEnergy;
    public float addHealth;
    float taHP;
    float taEN;
    public float tempAddHP
    {
        get { return taHP; }
        set
        {
            if (value % 4 == 0 && value != 0)
            {
                taHP = 0;
                PlayerController.Instance.playerHealthController.IncreaseMaxHealth(1);
            }
            else if (value > 4)
            {
                taHP = value - 4;
                PlayerController.Instance.playerHealthController.IncreaseMaxHealth(1);
            }
            else
                taHP = value;
            OnShardAdded?.Invoke(this);
        }
    }
    public float tempAddEN
    {
        get { return taEN; }
        set
        {
            if (value % 4 == 0 && value != 0)
            {
                taEN = 0;
                PlayerController.Instance.playerHealthController.IncreaseMaxEnergy((int)(value/4));
            }
            else if (value > 4)
            {
                taEN= value - 4;
                PlayerController.Instance.playerHealthController.IncreaseMaxEnergy(1);
            }
            else
                taEN = value;
            OnShardAdded?.Invoke(this);
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
