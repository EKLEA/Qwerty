using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObjWithLogic : DamagableObj
{


    public delegate void OnEnergyChangedDelegate();
    [HideInInspector] public OnEnergyChangedDelegate OnEnergyChangedCallBack;
    protected float en;
    public float maxEnergy;
    public float energy
    {
        get
        {
            return en;
        }
        set
        {
            OnEnergyChangedCallBack?.Invoke();
            en = value;

        }
    }
    protected new void Start()
    {
        base.Start();
        en = maxEnergy;

    }

}
