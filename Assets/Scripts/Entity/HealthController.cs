using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtController : MonoBehaviour
{
    public Action<GameObject> OnPlayerDead;
    [SerializeField] private float hp;
    public float health
    {
        get
        {
            return hp;
        }
        set
        {
            hp= value;
            if (health < 0)
                OnPlayerDead?.Invoke(gameObject);
            
        }
    }
    public float defense=1;
    public float engergy=10;
    public bool isHeartHas = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DamagableItem"))
            other.GetComponent<DamagableObjLogic> ().OnDamageEvent += DamageMoment;
        if (other.gameObject.CompareTag("DamagableObj"))
            DamageMoment(1);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DamagableItem"))
            other.GetComponent<DamagableObjLogic> ().OnDamageEvent -= DamageMoment;
        if (other.gameObject.CompareTag("DamagableObj"))
            DamageMoment(1);
    }

    private void DamageMoment(float s)
    {
        if (defense > 0)
            defense -= s;
        else
            health -= s;
    }
}
