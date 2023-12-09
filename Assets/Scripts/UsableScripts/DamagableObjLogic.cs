using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObjLogic: MonoBehaviour
{
    public Action<float> OnDamageEvent;
    [SerializeField] private IItem item;
    public float Damage;
   

    private void OnEnable()
    {
        //item = GetComponentInParent<Item>().GetExItem();
    }
    public void UseItem()
    {

    }
}
