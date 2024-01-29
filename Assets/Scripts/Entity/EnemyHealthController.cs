using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyHealthController : MonoBehaviour, IDamagable
{
    
    public Action<GameObject> OnDead;
    
    [SerializeField] protected float _maxHp;
    [SerializeField] protected  float recoilLenght;
    [SerializeField] protected float recoilFactor;
    [SerializeField] protected bool isRecoiling = false;
    protected bool hasTakenDamage = false;
    protected float rT =0;

    [HideInInspector] protected PlayerController playerController=> GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    [SerializeField] protected float speed;
    [SerializeField] protected float colliderDamage;
    protected virtual void Update()
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
    protected Rigidbody rb=> GetComponent<Rigidbody>();
    protected virtual void Awake()
    {
        hp = maxHealth;
    }
    [HideInInspector] public float hp;// потом изменить для хелтх бара
    [HideInInspector] public float df;
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
    public float defenseK
    {
        get { return df; }
        set
        {
            if (value < 1f && value > 2f)
                return;
            else
                df = value;
        }
    }

    public float maxHealth => _maxHp;
    public virtual void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        if (hasTakenDamage) return;
        health-= _damageDone * (1 / defenseK);
        if (!isRecoiling)
        {
            rb.AddForce(-_hitForce * recoilFactor* _hitDirection);
            hasTakenDamage = true;
        }
    }
    protected void OnTriggerStay(Collider other) 
    {
        if (other.CompareTag("Player")&&!playerController.playerStateList.invincible)
        {
            ColliderAttack();
        }
    }
    protected virtual void ColliderAttack()
    {
        playerController.playerHealthController.TakeDamage(colliderDamage);
        playerController.playerHealthController.HitStopTime(0, 5, 0.5f);
    }
}
