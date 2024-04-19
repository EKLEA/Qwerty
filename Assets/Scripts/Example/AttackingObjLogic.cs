using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttakingObjLogic : MonoBehaviour
{
    public Action<float,Item> OnDamageEvent;
    public abstract Item item { get; }

    public abstract void Attack();



}
