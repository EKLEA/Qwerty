using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagableObj : MonoBehaviour
{
    public Action<GameObject> OnDamageDone;
    public delegate void OnHealthChangedDelegate();
    [HideInInspector] public OnHealthChangedDelegate OnHealthChangedCallBack;

    public delegate void OnEnergyChangedDelegate();
    [HideInInspector] public OnEnergyChangedDelegate OnEnergyChangedCallBack;

    public delegate void OnDeadDelegate();
    [HideInInspector] public OnDeadDelegate OnDeadCallBack;

    public int maxHealth;
    public float maxEnergy;
    private void OnEnable()
    {
        hp = maxHealth;
        en = maxEnergy;

    }
    protected int hp;
    protected int df;
    protected float en;
    public int health
    {
        get
        {
            return hp;
        }
        set
        {
            if (defense > 0)
                defense -= value;
            else
                hp = value;
                OnHealthChangedCallBack?.Invoke();
                if (health <= 0)
                {
                OnDeadCallBack?.Invoke();
                    Destroy(gameObject);
                }

        }
    }
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
    public int defense
    {
        get { return df; }
        set
        {
            df = value;
        }
    }
    public virtual void DamageMoment(int _damageDone, Vector2 _hitDirection, float _hitForce) { }
}

