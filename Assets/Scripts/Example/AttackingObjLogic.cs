using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttakingObjLogic : MonoBehaviour
{
    public Action<float,IItem> OnDamageEvent;
    public abstract IItem item { get;  }

    public abstract void Attack();



}
