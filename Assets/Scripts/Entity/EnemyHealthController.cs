using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyHealthController : DamagableObj
{
    
    [SerializeField] protected  float recoilLength;
    [SerializeField] protected float recoilFactor;
    [HideInInspector] public bool isRecoiling = false;
    protected bool hasTakenDamage = false;
    protected float recoilingTime =0;

    [HideInInspector] protected PlayerController playerController=> GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    protected EnemyLogicBase enemyLogic=> GetComponent<EnemyLogicBase>();
    
    [SerializeField] protected int colliderDamage;
    protected Rigidbody rb=>GetComponent<Rigidbody>();
    protected virtual void Update()
    {
        hasTakenDamage = false;
        if (isRecoiling)
        {
            if (recoilingTime < recoilLength)
            {
                recoilingTime += Time.deltaTime;
            }
            else
            {
                isRecoiling = false;
                recoilingTime = 0;
            }
        }
        else
        {
            enemyLogic.UpdateEnemyStates();
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
            rb.velocity= _hitForce * recoilFactor* _hitDirection;
            hasTakenDamage = true;
        }
    }
    protected void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player") && !playerController.playerStateList.invincible && playerController.playerHealthController.health>0)
        {
            ColliderAttack();
        }
    }
    protected virtual void ColliderAttack()
    {
        playerController.playerHealthController.DamageMoment(colliderDamage,Vector2.zero,0);
        if (playerController.playerStateList.alive)
            playerController.playerHealthController.HitStopTime(0, 5, 0.5f);
    }
}
