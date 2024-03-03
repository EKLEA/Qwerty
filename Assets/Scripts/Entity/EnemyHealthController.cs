using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyHealthController : DamagableObj
{
    
    [SerializeField] protected  float recoilLenght;
    [SerializeField] protected float recoilFactor;
    [SerializeField] protected bool isRecoiling = false;
    protected bool hasTakenDamage = false;
    protected float rT =0;

    [HideInInspector] protected PlayerController playerController=> GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    [SerializeField] protected float speed;// переделать
    [SerializeField] protected int colliderDamage;
    Rigidbody rb=>GetComponent<Rigidbody>();
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
    public override void DamageMoment(int _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        if (hasTakenDamage) return;
        if (defense > 0)
            df -= _damageDone;
        else
            health -= _damageDone;
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
        playerController.playerHealthController.DamageMoment(colliderDamage,Vector2.zero,0);
        playerController.playerHealthController.HitStopTime(0, 5, 0.5f);
    }
}
