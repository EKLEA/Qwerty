using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealtController : MonoBehaviour, IDamagable
{
    public Action<GameObject> OnDead;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _maxDefense;
    [SerializeField] private float hp;
    [SerializeField] private float df;
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
            {
                OnDead?.Invoke(gameObject);
                DestroyImmediate(gameObject);
            }   
        }
    }
    public float defense
    {
        get { return df; }
        set { df = value; }
    }

    private void OnEnable()
    {
        hp=maxHealth; df=maxDefense;
    }
    public float maxHealth =>_maxHp;
    public float maxDefense => _maxDefense;
    public bool isHeartHas = true;



    public void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        
        if (defense > 0)
            defense -= _damageDone;
        else
            health -= _damageDone;
    }
}
