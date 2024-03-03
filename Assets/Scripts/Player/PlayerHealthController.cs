using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : DamagableObj
{
    private PlayerController pController => GetComponent<PlayerController>();
    
    [SerializeField] private GameObject DamageEffect;
    bool restoreTime;
    float restoreTimeSpeed;
    float healTimer;
    [SerializeField] private float timeToHeal;
    [SerializeField] float energyDrainSpeed;
    [SerializeField] public float energyGain;

    public bool isHeartHas = true;



    public override void DamageMoment(int _damageDone, Vector2 _hitDirection, float _hitForce)
    { 
        health -= _damageDone;
        StartCoroutine(StopTakingDamage());
    }
    IEnumerator StopTakingDamage()
    {
        pController.playerStateList.invincible = true;
        GameObject  _damageEffect = Instantiate(DamageEffect,new Vector2(transform.position.x,transform.position.y+1.5f), Quaternion.identity);
        Destroy(_damageEffect, 1.5f);
        yield return new WaitForSeconds(1f);
        pController.playerStateList.invincible=false;
    }
    public void RestoreTimeScale()
    {
        if (restoreTime)
        {
            if (Time.timeScale < 1)
                Time.timeScale += Time.deltaTime * restoreTimeSpeed;
            else
            {
                Time.timeScale = 1;
                restoreTime = false;
            }
        }
    }
    public void HitStopTime(float _newTimeScale, int _restoreSpeed, float _delay)
    {
        restoreTimeSpeed = _restoreSpeed;
        Time.timeScale = _newTimeScale;
        if (_delay>0)
        {
            StopCoroutine(StartTimeAgain(_delay));
            StartCoroutine(StartTimeAgain(_delay));
        }
        else
        {
            restoreTime = true;
        }
    }
    IEnumerator StartTimeAgain(float _delay)
    {
        restoreTime = true;
        yield return new WaitForSeconds(_delay);
    }

   public void Heal()
    {
        if (Input.GetButton("Healing") && health<maxHealth && energy>0 && !pController.playerStateList.jumping && !pController.playerStateList.dashing)
        {
            pController.playerStateList.healing = true;
            healTimer += Time.deltaTime;
            if (healTimer>=timeToHeal)
            {
                health++;
                healTimer = 0;
            }
            
            energy -= Time.deltaTime * energyDrainSpeed;
            Debug.Log(energy);
        }
        else
        {
            pController.playerStateList.healing=false;
            healTimer = 0;
        }
    }
}
