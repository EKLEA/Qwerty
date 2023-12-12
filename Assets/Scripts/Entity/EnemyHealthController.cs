using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour, IDamagable
{
    public Action<GameObject> OnDead;
    [SerializeField] private float _maxHp;
    [SerializeField] private float _maxDefense;
    [SerializeField] float recoilLenght;
    [SerializeField] float recoilFactor;
    [SerializeField] bool isRecoiling = false;
    float rT=0;
    float recoilTimer
    {
        get
        {
            if (isRecoiling)
            {
                if (rT < recoilLenght)
                    rT += Time.deltaTime;
                else
                {
                    isRecoiling = false;
                    rT = 0;
                }
            }
            return rT;
        }
    }
    
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hp = maxHealth; df = maxDefense;
    }
    private float hp;
    private float df;
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

        if (defense > 0)
            defense -= _damageDone;
        else
            health -= _damageDone;
        if (!isRecoiling)
        {
            rb.AddForce(_hitDirection * _hitForce * recoilFactor);
        }
    }
}
