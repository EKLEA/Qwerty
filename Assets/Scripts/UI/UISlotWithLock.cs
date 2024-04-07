using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlotWithLock : UIInventorySlot
{
    [SerializeField] protected GameObject locker;
    protected void OnEnable()
    {
        SetLock();
    }
    protected virtual void SetLock()
    {

    }

    public override void Refresh()
    {
        base.Refresh();
        SetLock();
    }
}
