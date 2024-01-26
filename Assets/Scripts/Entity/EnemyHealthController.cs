using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour, IDamagable
{
    public Action<GameObject> OnDead;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _maxDefense;
    [SerializeField] float recoilLenght;
    [SerializeField] float recoilFactor;
    [SerializeField] bool isRecoiling = false;
    protected bool hasTakenDamage = false;
    float rT=0;
  
    void Update()
    {
        hasTakenDamage = false;
        if (isRecoiling)
        {
            if (rT < recoilLenght)
            {
                rT += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                rT = 0;
            }
        }
    }
    Rigidbody rb=> GetComponent<Rigidbody>();
    void Awake()
    {
        hp = maxHealth; df = maxDefense;
    }
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
            hp = value;
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

    public float maxHealth => _maxHp;
    public float maxDefense => _maxDefense;
    public bool isHeartHas = true;
    public void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        if (hasTakenDamage) return;
        if (defense > 0)
            defense -= _damageDone;
        else
            health -= _damageDone;
        if (!isRecoiling)
        {
            rb.AddForce(-_hitForce * recoilFactor* _hitDirection);
            hasTakenDamage = true;
        }
    }
}
