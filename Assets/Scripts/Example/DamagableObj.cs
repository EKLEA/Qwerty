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

    public delegate void OnDeadDelegate();
    [HideInInspector] public OnDeadDelegate OnDeadCallBack;

    public float maxHealth;
    
    protected void Start()
    {
        hp = maxHealth;

    }
    protected float hp;
    protected float df;
    
    public float health
    {
        get
        {
            return hp;
        }
        set
        {
                hp = value;
                OnHealthChangedCallBack?.Invoke();
                if (hp <= 0)
                {
                    OnDeadCallBack?.Invoke();
                    Destroy(gameObject);
                }

        }
    }
    public virtual void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce) { }
}

